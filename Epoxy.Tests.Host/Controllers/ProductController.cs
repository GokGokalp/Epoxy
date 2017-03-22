using System.Web.Http;
using System.Web.Http.ModelBinding;
using Epoxy.Tests.Host.Request;
using Epoxy.Tests.Host.Response;

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

        [HttpGet, Route("{Id}")]
        public GetResponse Get([ModelBinder]GetRequest request)
        {
            return new GetResponse() { Id = request.Id };
        }
    }
}