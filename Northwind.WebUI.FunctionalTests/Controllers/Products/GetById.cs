using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Application.Products.Queries.GetProduct;
using Northwind.WebUI.FunctionalTests.Common;
using Xunit;

namespace Northwind.WebUI.FunctionalTests.Controllers.Products
{
    public class GetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetById(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenValidId_ReturnsProductViewModel()
        {
            var id = 67;

            var response = await _client.GetAsync($"/api/products/get/{id}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<ProductViewModel>(response);

            Assert.Equal(id, result.ProductId);
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsProductViewModel()
        {
            var invalidId = 0;

            var response = await _client.GetAsync($"/api/products/get/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}