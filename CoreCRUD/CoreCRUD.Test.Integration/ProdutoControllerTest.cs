using CoreCRUD.Api.ViewModel;
using CoreCRUD.Infrastructure.Collections;
using CoreCRUD.Infrastructure.Test;
using CoreCRUD.Test.Integration.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoreCRUD.Test.Integration
{
    public class ProdutoControllerTest : BaseIntegrationTest
    {

        public ProdutoControllerTest() : base()
        {
            DataHelper.StartDatabase();
        }

        [Fact]
        public async Task ListaTodos()
        {
            var response = await Client.GetAsync("/api/Produto");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ListaPaginada()
        {
            var response = await Client.GetAsync(string.Format("/api/Produto/pagina/{0}/itensporpagina/{1}", 1, 3));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            PagedList<ProdutoViewModel> listaProdutos = JsonConvert.DeserializeObject<PagedList<ProdutoViewModel>>(responseString);       
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ListarPorIdValido()
        {
            var response = await Client.GetAsync(string.Format("/api/Produto/{0}", DataHelper.GetValidId()));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();                    
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ListarPorIdInValido()
        {
            var response = await Client.GetAsync(string.Format("/api/Produto/{0}", DataHelper.GetRamdonId()));
            var responseString = await response.Content.ReadAsStringAsync();           
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("Geladeira", "Essa é a descrição da geladeira", new string[] { "Geladeiras" }, 2500)]
        public async Task InserirProdutoEntidadeValida(string nome, string descricao, string[] categorias, double preco)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel()
            {
                Nome = nome,
                Descricao = descricao,
                Categorias = categorias,
                Preco = preco
            };

            string entidade = JsonConvert.SerializeObject(viewModel);
            var response = await Client.PostAsync("/api/Produto", new StringContent(entidade, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("", "Essa é a descrição da geladeira W600", new string[] { "Geladeiras" }, 2500)]
        [InlineData("Geladeira W600", "", new string[] { "Geladeiras" }, 2500)]
        [InlineData("Geladeira W600", "Essa é a descrição da geladeira W600", new string[] { }, 2400)]
        [InlineData("Geladeira W600", "Essa é a descrição da geladeira W600", new string[] { "Geladeiras" }, 0)]
        public async Task InserirProdutoEntidadeInvalida(string nome, string descricao, string[] categorias, double preco)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel()
            {
                Nome = nome,
                Descricao = descricao,
                Categorias = categorias,
                Preco = preco
            };

            string entidade = JsonConvert.SerializeObject(viewModel);
            var response = await Client.PostAsync("/api/Produto", new StringContent(entidade, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("Geladeira W600", "Essa é a descrição da geladeira W600", new string[] { "Geladeiras" }, 2500)]
        public async Task EditarProdutoEntidadeValida(string nome, string descricao, string[] categorias, double preco)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel()
            {
                Id = DataHelper.GetValidId(),
                Nome = nome,
                Descricao = descricao,
                Categorias = categorias,
                Preco = preco
            };

            string entidade = JsonConvert.SerializeObject(viewModel);
            var response = await Client.PutAsync(string.Format("/api/Produto/{0}", DataHelper.GetValidId()), new StringContent(entidade, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("59dc51286e57114d98098e99", "Geladeira W600", "Essa é a descrição da geladeira W600", new string[] { "Geladeiras" }, 1000)]
        public async Task EditarProdutoEntidadeIdInValido(string id, string nome, string descricao, string[] categorias, double preco)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel()
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Categorias = categorias,
                Preco = preco
            };

            string entidade = JsonConvert.SerializeObject(viewModel);
            var response = await Client.PutAsync(string.Format("/api/Produto/{0}", id), new StringContent(entidade, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeletarProdutoIdInValido()
        {
            var response = await Client.DeleteAsync(string.Format("/api/Produto/{0}", DataHelper.GetRamdonId()));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeletarProdutoIdValido()
        {
            var response = await Client.DeleteAsync(string.Format("/api/Produto/{0}", DataHelper.GetValidId()));
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
