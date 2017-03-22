using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using Epoxy.Tests.Host;
using Epoxy.Tests.Host.Response;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epoxy.Tests
{
    [TestClass]
    public class FromUriAndFromBodyBinderTests
    {
        private string _variantAddUrl;
        private string _apiUrl;

        [TestInitialize]
        public void Initialize()
        {
            _variantAddUrl = "http://localhost:8080/api/products/{0}/variants";
            _apiUrl = "http://localhost:8080/api/products/";
        }

        [TestMethod]
        public void FromUriAndFromBodyBinder_BindBothProductIdParameterAndPayloadObject_ReturnBindedObject()
        {
            //Arrange
            int productId = 5;
            _variantAddUrl = string.Format(_variantAddUrl, productId);
            AddProductVariantRequest addProductVariantRequest = new AddProductVariantRequest()
            {
                Name = "Black",
                Price = 550,
                SpecialPrice = 400,
                SpecialPriceDuration = 5,
                SpecialPriceStartDate = DateTime.Today.AddDays(5),
                CreatedOn = DateTime.Today,
                DefaultCategory = new Category() { CreatedOn = DateTime.Today, Id = 5, Name = "Watches" }
            };
            AddProductVariantResponse addProductVariantResponse;

            //Act
            using (var server = TestServer.Create<Startup>())
            {
                HttpResponseMessage response = server.CreateRequest(_variantAddUrl)
                                              .And(request => request.Content = new ObjectContent(typeof(AddProductVariantRequest), addProductVariantRequest, new JsonMediaTypeFormatter()))
                                              .PostAsync().Result;

                addProductVariantResponse = response.Content.ReadAsAsync<AddProductVariantResponse>().Result;
            }

            //Assert
            Assert.AreEqual(productId, addProductVariantResponse.ProductId);
            Assert.AreEqual(addProductVariantRequest.Name, addProductVariantResponse.Name);
            Assert.AreEqual(addProductVariantRequest.Price, addProductVariantResponse.Price);
            Assert.AreEqual(addProductVariantRequest.SpecialPrice, addProductVariantResponse.SpecialPrice);
            Assert.AreEqual(addProductVariantRequest.SpecialPriceDuration, addProductVariantResponse.SpecialPriceDuration);
            Assert.AreEqual(addProductVariantRequest.SpecialPriceStartDate, addProductVariantResponse.SpecialPriceStartDate);
            Assert.AreEqual(addProductVariantRequest.CreatedOn, addProductVariantResponse.CreatedOn);
            Assert.IsNotNull(addProductVariantResponse.DefaultCategory);
        }

        [TestMethod]
        public void FromUriAndFromBodyBinder_BindIdParameter_ReturnBindedObject()
        {
            //Arrange
            int id = 5;
            string getUrl = $"{_apiUrl}{id}";
             
            GetResponse getResponse;

            //Act
            using (var server = TestServer.Create<Startup>())
            {
                HttpResponseMessage response = server.CreateRequest(getUrl)
                                              .GetAsync().Result;

                getResponse= response.Content.ReadAsAsync<GetResponse>().Result;
            }

            //Assert
            Assert.AreEqual(id, getResponse.Id);
        }
    }
}