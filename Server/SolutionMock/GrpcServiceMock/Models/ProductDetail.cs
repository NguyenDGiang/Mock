using System;
using System.Collections.Generic;

namespace GrpcServiceMock.Models
{
    public partial class ProductDetail : BaseEntity
    {
        public string? Price { get; set; }
        public string? Color { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? MadeBy { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
