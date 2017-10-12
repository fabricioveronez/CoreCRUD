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


        public ProdutoControllerTests()
        {
            // Crio um mock para o AutoMapper
            mockMapper = new Mock<IMapper>();

            // Configuro o mapeamento de ProdutoViewModel para Produto
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

            // Configuro o mapeamento de Produto para ProdutoViewModel
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
        }

        [Fact]
        public void ListarTodos()
        {
            // Crio um mock para IProdutoService
            var produtoServiceMock = new Mock<IProdutoService>();
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

            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void ListarPorId()
        {
            string id = ObjectId.GenerateNewId().ToString();

            // Crio um mock para IProdutoService
            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock.Setup(service => service.Get(id)).Returns(() =>
            {
                return new Produto()
                {
                    Id = id,
                    Nome = "Geladeira B W600",
                    Categorias = new string[] { "Geladeira" },
                    Descricao = "Descrição detalhada da Geladeira B W600",
                    Preco = 2500
                };
            });

            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void InserirProdutoEntidadeValida()
        {
            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock.Setup(service => service.Save(new Produto())).Verifiable();

            string id = ObjectId.GenerateNewId().ToString();
            produtoServiceMock.Setup(service => service.Get(id)).Returns(() =>
            {
                return new Produto()
                {
                    Id = id,
                    Nome = "Geladeira B W600",
                    Categorias = new string[] { "Geladeira" },
                    Descricao = "Descrição detalhada da Geladeira B W600",
                    Preco = 2500
                };
            });

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
            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock.Setup(service => service.Save(new Produto())).Verifiable();

            string id = ObjectId.GenerateNewId().ToString();
            produtoServiceMock.Setup(service => service.Get(id)).Returns(() =>
            {
                return new Produto()
                {
                    Id = id,
                    Nome = "Geladeira B W600",
                    Categorias = new string[] { "Geladeira" },
                    Descricao = "Descrição detalhada da Geladeira B W600",
                    Preco = 2500
                };
            });

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
            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock.Setup(service => service.Save(new Produto())).Verifiable();

            string id = ObjectId.GenerateNewId().ToString();
            produtoServiceMock.Setup(service => service.Get(id)).Returns(() =>
            {
                return new Produto()
                {
                    Id = id,
                    Nome = "Geladeira B W600",
                    Categorias = new string[] { "Geladeira" },
                    Descricao = "Descrição detalhada da Geladeira B W600",
                    Preco = 2500
                };
            });

            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Update(id, new ProdutoViewModel()
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
            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock.Setup(service => service.Save(new Produto())).Verifiable();

            string idAleatorio = ObjectId.GenerateNewId().ToString();
            string idModelo = ObjectId.GenerateNewId().ToString();
            produtoServiceMock.Setup(service => service.Get(idModelo)).Returns(() =>
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

            var controller = new ProdutoController(mockMapper.Object, produtoServiceMock.Object);

            IActionResult result = controller.Update(idAleatorio, new ProdutoViewModel()
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

    }
}
