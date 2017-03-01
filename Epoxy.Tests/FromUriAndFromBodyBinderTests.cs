using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using Epoxy.Tests.Host;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epoxy.Tests
{
    [TestClass]
    public class FromUriAndFromBodyBinderTests
    {
        private string _variantAddUrl;

        [TestInitialize]
        public void Initialize()
        {
            _variantAddUrl = "http://localhost:8080/api/products/{0}/variants";
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
    }
}