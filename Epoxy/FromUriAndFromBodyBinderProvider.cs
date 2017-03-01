using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Epoxy
{
    public class EpoxyFromUriAndFromBodyBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new FromUriAndFromBodyBinder(configuration.Formatters.JsonFormatter);
        }
    }
}