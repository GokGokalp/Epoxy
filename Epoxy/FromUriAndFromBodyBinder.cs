using System;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using Newtonsoft.Json;

namespace Epoxy
{
    public class FromUriAndFromBodyBinder : IModelBinder
    {
        private readonly JsonMediaTypeFormatter _formatter;

        public FromUriAndFromBodyBinder(JsonMediaTypeFormatter formatter)
        {
            _formatter = formatter;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            MethodInfo deserializeMethod = GetType().GetMethod("DeserializeModel")
                             .MakeGenericMethod(bindingContext.ModelType);
            var model = deserializeMethod.Invoke(this, new object[] { actionContext });

            foreach (var prop in model.GetType().GetProperties())
            {
                ValueProviderResult uriVal = bindingContext.ValueProvider.GetValue(prop.Name.ToLowerInvariant());
                if (uriVal != null)
                {
                    prop.SetValue(model, Convert.ChangeType(uriVal.RawValue, prop.PropertyType), null);
                }
            }

            bindingContext.Model = model;
            bindingContext.ValidationNode.ValidateAllProperties = true;
            bindingContext.ValidationNode.Validate(actionContext);

            return true;
        }

        public T DeserializeModel<T>(HttpActionContext actionContext) where T : class
        {
            string query = actionContext.Request.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<T>(query, _formatter.SerializerSettings);

            return model;
        }
    }
}