using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Epoxy.Tests.Host.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        [HttpPost, Route("{productId}/variants")]
        public AddProductVariantResponse AddProductsVariants([ModelBinder]AddProductVariantRequest addProductVariantRequest)
        {
            return addProductVariantRequest;
        }
    }
}