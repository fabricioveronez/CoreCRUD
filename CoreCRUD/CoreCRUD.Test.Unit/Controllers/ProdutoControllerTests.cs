using Moq;
using CoreCRUD.Application.Interfaces.Services;
using CoreCRUD.Api.Controllers;
using AutoMapper;
using CoreCRUD.Domain.Entities;
using CoreCRUD.Api.ViewModel;
using System.Collections.Generic;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CoreCRUD.Test.Unit
{
    public class ProdutoControllerTests
    {

        Mock<IMapper> mockMapper;
        Mock<IProdutoService> produtoServiceMock;


        public ProdutoControllerTests()
        {        
            mockMapper = new Mock<IMapper>();
           
            mockMapper.Setup(x => x.Map<Produto>(It.IsAny<ProdutoViewModel>()))
                .Returns((ProdutoViewModel source) =>
                {
                    return new Produto()
                    {
                        Id = source.Id,
                        Nome = source.Nome,
                        Categorias = source.Categorias,
                        Preco = source.Preco,
                        Descricao = source.Descricao
                    };
                });
         
            mockMapper.Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns((Produto source) =>
                {
                    return new ProdutoViewModel()
                    {
                        Id = source.Id,
                        Nome = source.Nome,
                        Categorias = source.Categorias,
                        Preco = source.Preco,
                        Descricao = source.Descricao
                    };
                });

            mockMapper.Setup(x => x.Map<IEnumerable<ProdutoViewModel>>(It.IsAny<IEnumerable<Produto>>()))
              .Returns((IEnumerable<Produto> listSource) =>
              {
                  List<ProdutoViewModel> retorno = new List<ProdutoViewModel>();

                  foreach (var source in listSource)
                  {
                      retorno.Add(new ProdutoViewModel()
                      {
                          Id = source.Id,
                          Nome = source.Nome,
                          Categorias = source.Categorias,
                          Preco = source.Preco,
                          Descricao = source.Descricao
                      });
                  }

                  return retorno;

              });

            mockMapper.Setup(x => x.Map<IEnumerable<Produto>>(It.IsAny<IEnumerable<ProdutoViewModel>>()))
              .Returns((IEnumerable<ProdutoViewModel> listSource) =>
              {
                  List<Produto> retorno = new List<Produto>();

                  foreach (var source in listSource)
                  {
                      retorno.Add(new Produto()
                      {
                          Id = source.Id,
                          Nome = source.Nome,
                          Categorias = source.Categorias,
                          Preco = source.Preco,
                          Descricao = source.Descricao
                      });
                  }

                  return retorno;

              });


            // Crio um mock para IProdutoService
            produtoServiceMock = new Mock<IProdutoService>();

            produtoServiceMock.Setup(service => service.GetAll()).Returns(() =>
            {
                return new List<Produto>()
                {
                    new Produto()
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            Nome = "Geladeira B W600",
                            Categorias = new string[] {"Geladeira" },
                            Descricao = "Descrição detalhada da Geladeira B W600" ,
                            Preco = 2500
                        }
                };
            });

            string idModelo = ObjectId.GenerateNewId().ToString();
            produtoServiceMock.Setup(service => service.Get(It.IsAny<string>())).Returns(() =>
            {
                return new Produto()
                {
                    Id = idModelo,
                    Nome = "Geladeira B W600",
                    Categorias = new string[] { "Geladeira" },
                    Descricao = "Descrição detalhada da Geladeira B W600",
                    Preco = 2500
                };
            });

            produtoServiceMock.Setup(service => service.Save(It.IsAny<Produto>())).Verifiable();
        }

        [Fact]
        public void ListarTodos()
        {
            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void ListarPorId()
        {
            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void InserirProdutoEntidadeValida()
        {
            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Post(new ProdutoViewModel()
            {
                Nome = "Geladeira B W600",
                Categorias = new string[] { "Geladeira" },
                Descricao = "Descrição detalhada da Geladeira B W600",
                Preco = 2500
            });

            ObjectResult okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(201, okResult.StatusCode);
        }

        [Fact]
        public void InserirProdutoEntidadeInvalida()
        {
            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);
            controller.ModelState.AddModelError("Descricao", "Descrição vazia");

            IActionResult result = controller.Post(new ProdutoViewModel()
            {
                Nome = "Geladeira B W600",
                Categorias = new string[] { "Geladeira" },
                Descricao = "",
                Preco = 2500
            });

            ObjectResult resultado = result as ObjectResult;

            Assert.NotNull(resultado);
            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void EditarProdutoEntidadeIdValido()
        {
            string id = ObjectId.GenerateNewId().ToString();

            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Put(id, new ProdutoViewModel()
            {
                Id = id,
                Nome = "Geladeira B W600",
                Categorias = new string[] { "Geladeira" },
                Descricao = "",
                Preco = 2500
            });

            NoContentResult okResult = result as NoContentResult;
            Assert.NotNull(okResult);
            Assert.Equal(204, okResult.StatusCode);
        }

        [Fact]
        public void EditarProdutoEntidadeIdInValido()
        {
            string idAleatorio = ObjectId.GenerateNewId().ToString();
            string idModelo = ObjectId.GenerateNewId().ToString();

            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Put(idAleatorio, new ProdutoViewModel()
            {
                Id = idModelo,
                Nome = "Geladeira B W600",
                Categorias = new string[] { "Geladeira" },
                Descricao = "",
                Preco = 2500
            });

            BadRequestResult resultado = result as BadRequestResult;
            Assert.NotNull(resultado);
            Assert.Equal(400, resultado.StatusCode);
        }

        [Fact]
        public void ListaPaginada()
        {
            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.PagedGet(1, 5);
            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeletarProduto()
        {
            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Delete(ObjectId.GenerateNewId().ToString());
            NoContentResult okResult = result as NoContentResult;
            Assert.NotNull(okResult);
            Assert.Equal(204, okResult.StatusCode);
        }
    }
}
