using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ContentViewModel
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public long? ContentCategoryID { get; set; }

        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
        
        public string Tags { get; set; }
        
        public string MetaKeywords { get; set; }
        
        public string MetaDescriptions { get; set; }

        public DateTime? CreatedDate { get; set; }
        
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        public string Language { get; set; }


    }
}
