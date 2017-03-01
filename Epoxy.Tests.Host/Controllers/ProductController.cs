using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Epoxy.Tests.Host.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        [HttpPost, Route("{productId}/variants")]
        public IHttpActionResult AddProductsVariants([ModelBinder]AddProductVariantRequest addProductVariantRequest)
        {
            if (addProductVariantRequest.ProductId > 0)
            {
                return Ok();
            }
            
            return BadRequest();
        }
    }
}