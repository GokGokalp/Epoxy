#   **Epoxy**
------------------------------

![alt tag](https://raw.githubusercontent.com/GokGokalp/Epoxy/master/misc/epoxy-logo.png)

Simple both _fromuri_ and _frombody_ model binder for Asp.NET Web API

### NuGet Packages
``` 
PM> Install-Package Epoxy 
```

####Features:
- Binds both **fromuri** and **frombody** data to model
- Supports model validations
- Currently only support JSON data for body

Usage:
-----

Firstly insert _EpoxyFromUriAndFromBodyBinderProvider_ to HttpConfiguration.Services:

```cs
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.Services.Insert(typeof(ModelBinderProvider), 0, new EpoxyFromUriAndFromBodyBinderProvider());

        // ...
    }
}
```

after service insert step, you should use [ModelBinder] attribute. There are two ways:

First way is: in Controller. For example:
```cs
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
```

after this, Epoxy will bind uri parameters(eg: productId) and payload data to _AddProductVariantRequest_ object.

Second way is: in request object. For example:
```cs
[ModelBinder]
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
```