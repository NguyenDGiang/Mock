using System;
using System.Collections.Generic;

namespace GrpcServiceMock.Models
{
    public partial class Category: BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string? Name { get; set; }
        public string? TagName { get; set; }
        public bool? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
