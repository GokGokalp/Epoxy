using System;
using System.Collections.Generic;

namespace Epoxy.Tests.Host
{
    public class AddProductVariantRequest
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? SpecialPrice { get; set; }
        public int? SpecialPriceDuration { get; set; }
        public DateTime? SpecialPriceStartDate { get; set; }
        public DateTime CreatedOn { get; set; }

        public Category DefaultCategory { get; set; }
        public List<Category> AdditionalCategories { get; set; }
    }
}