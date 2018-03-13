using Model.Dao;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BatmanShop.Areas.Admin.Controllers
{
    public class InventoryController : BaseController
    {
        ////Display, create, edit, delete Inventory
        //Index page of Inventory management
        public ActionResult ImportIndex(string searchString, string docCode = "IM", int page = 1, int pageSize = 10)
        {
            var dao = new AccDocDao();
            var model = dao.ListAllPaging(docCode, searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            ViewBag.PageSize = pageSize;
            return View(model);
        }

        //Import Inventory
        [HttpGet]
        public ActionResult ImportCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportCreate(AccDoc accDoc)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccDocDao();
                long id = dao.Insert(accDoc);
                accDoc.DocNo = dao.UpdateDocNo(accDoc.DocCode, id);
                if (accDoc.ID > 0)
                {
                    SetAlert("Create a new AccDoc successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Create a new AccDoc failed.");
                    return RedirectToAction("ImportCreate", "Inventory");
                }
            }
            else
            {
                SetAlert("Please verify all fields", "error");
            }
            ViewBag.DocNo = accDoc.DocNo;
            return View(accDoc);
        }

        public JsonResult ImportDetail(string list)
        {
            var json = new JavaScriptSerializer().Deserialize<List<Inventory>>(list);
            var accDocDetailDao = new AccDocDetailDao();
            AccDocDetail accDocDetail = new AccDocDetail();
            var inventoryDao = new InventoryDao();
            Inventory inventory = new Inventory();

            if (json.Count() > 0)
            {
                accDocDetailDao.RemoveDetail(json[0].DocNo);
                inventoryDao.RemoveDetail(json[0].DocNo);
                foreach (var item in json)
                {
                    //Insert detail vào bảng AccDocDetail
                    accDocDetail.DocCode = item.DocCode;
                    accDocDetail.DocNo = item.DocNo;
                    accDocDetail.WarehouseID = item.WarehouseID;
                    accDocDetail.ProductID = item.ProductID;
                    accDocDetail.Quantity = item.Quantity;
                    if(accDocDetailDao.CheckExist(accDocDetail.DocCode, accDocDetail.DocNo, accDocDetail.WarehouseID, accDocDetail.ProductID) == true)
                    {
                        accDocDetailDao.UpdateQuantity(accDocDetail.DocCode, accDocDetail.DocNo, accDocDetail.WarehouseID, accDocDetail.ProductID, (int)accDocDetail.Quantity);
                    }
                    else
                    {
                        accDocDetailDao.Insert(accDocDetail);
                    }

                    //Insert detail vào bảng Inventory
                    if(inventoryDao.CheckExist(accDocDetail.DocCode, accDocDetail.DocNo, accDocDetail.WarehouseID, accDocDetail.ProductID) == true)
                    {
                        inventoryDao.UpdateQuantity(accDocDetail.DocCode, accDocDetail.DocNo, accDocDetail.WarehouseID, accDocDetail.ProductID, (int)item.Quantity);
                    }
                    else
                    {
                        inventoryDao.Insert(item);
                    }
                    
                }
            }

            return Json(new
            {
                status = true
            });
        }

        //Edit an Import Inventory 
        [HttpGet]
        public ActionResult ImportEdit(long id)
        {
            var accDoc = new AccDocDao().GetByID(id);
            ViewBag.ListDetail = new AccDocDetailDao().ListDetail(accDoc.DocCode, accDoc.DocNo);
            return View(accDoc);
        }

        [HttpPost]
        public ActionResult ImportEdit(AccDoc accDoc)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccDocDao();
                bool result = dao.Update(accDoc);
                accDoc.DocNo = dao.UpdateDocNo(accDoc.DocCode, accDoc.ID);
                if (result)
                {
                    SetAlert("Update the AccDoc successfully.", "success");
                }
                else
                {
                    ModelState.AddModelError("", "Update the AccDoc failed.");
                    return RedirectToAction("ImportEdit", "Inventory");
                }
            }
            else
            {
                SetAlert("Please verify all fields", "error");
            }
            ViewBag.ListDetail = new AccDocDetailDao().ListDetail(accDoc.DocCode, accDoc.DocNo);
            return View(accDoc);
        }

        //Delete an Import Inventory
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var accDoc = new AccDocDao().GetByID(id);
            new AccDocDetailDao().RemoveDetail(accDoc.DocNo);
            new InventoryDao().RemoveDetail(accDoc.DocNo);
            new AccDocDao().Delete(id);
            return RedirectToAction("ImportIndex");
        }










    }
}