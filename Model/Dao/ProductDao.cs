using Model.EntityFramework;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        BatmanShopDbContext db = null;

        public ProductDao()
        {
            db = new BatmanShopDbContext();
        }

        /// <summary>
        /// Get Product when having ID - Lấy ra Product khi có ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetByID(long id)
        {
            return db.Products.Find(id);
        }

        /// <summary>
        /// Insert one Product to database -  Thêm mới một Product vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Insert(Product entity)
        {
            entity.CreatedDate = entity.ModifiedDate = DateTime.Now;
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        /// <summary>
        /// Update one Product in the database -  Cập nhật một Product trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.MoreImages = entity.MoreImages;
                product.Price = entity.Price;
                product.PromotionPrice = entity.PromotionPrice;
                product.IncludedVAT = entity.IncludedVAT;
                product.Quantity = entity.Quantity;
                product.ProductCategoryID = entity.ProductCategoryID;
                product.Detail = entity.Detail;
                product.MetaKeywords = entity.MetaKeywords;
                product.MetaDescriptions = entity.MetaDescriptions;
                product.ModifiedDate = DateTime.Now;
                product.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete one Product in the database - Xóa một Product khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// List Product into a table with search string - Liệt kê danh sách Product có thể sử dụng tìm kiếm search
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Code.Contains(searchString) || x.Name.Contains(searchString)
                || x.Description.Contains(searchString) || x.Detail.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get Product list when having ProductCategoryID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListByCategoryId(long productCategoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 3)
        {
            totalRecord = db.Products.Where(x => x.ProductCategoryID == productCategoryID).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.ProductCategoryID equals b.ID
                         where a.ProductCategoryID == productCategoryID
                         select new
                         {
                             CategoryMetaTitle = b.MetaTitle,
                             CategoryName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CategoryMetaTitle = x.MetaTitle,
                             CategoryName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Image,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model = model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        /// <summary>
        /// Get new related Products when having productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Product> ListNewRelatedProduct(long productId, int top)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != product.ID && x.ProductCategoryID == product.ProductCategoryID).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// Get top new Products
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListTopNewProduct(int top)
        {
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.ProductCategoryID equals b.ID
                         where a.Status == true
                         select new
                         {
                             CategoryMetaTitle = b.MetaTitle,
                             CategoryName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CategoryMetaTitle = x.MetaTitle,
                             CategoryName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Image,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            return model.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// Get feature Products
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot >= DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// Get hot related Products when having productId
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListHotRelatedProduct(long productId, int top)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ProductCategoryID == product.ProductCategoryID && x.TopHot != null && x.TopHot >= DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// Get Product list when having a keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Search Products and paged list as well
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="totalRecord"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.ProductCategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CategoryMetaTitle = b.MetaTitle,
                             CategoryName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CategoryMetaTitle = x.MetaTitle,
                             CategoryName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Image = x.Image,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        /// <summary>
        /// Update MoreImages
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="images"></param>
        public void UpdateImages(long productId, string images)
        {
            var product = db.Products.Find(productId);
            product.MoreImages = images;
            db.SaveChanges();
        }

        /// <summary>
        /// Update ViewCount of the Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public void UpdateViewCount(long productId)
        {
            var product = db.Products.Find(productId);
            if(product.ViewCount != null)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
            db.SaveChanges();
        }




    }
}
