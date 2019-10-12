using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CoreCRUD.Api.ViewModel;
using CoreCRUD.Application.Interfaces.Services;
using CoreCRUD.Domain.Entities;
using CoreCRUD.Infrastructure.Collections;
using MongoDB.Bson;

namespace CoreCRUD.Api.Controllers
{
    /// <summary>
    /// Controller de produto
    /// </summary>
    [Produces("application/json")]
    [Route("api/Produto")]
    public class ProdutoController : Controller
    {
        /// <summary>
        /// Serviço de produto
        /// </summary>
        public IProdutoService Service { get; set; }

        /// <summary>
        /// Auto mapper
        /// </summary>
        public IMapper AutoMapper { get; set; }

        /// <summary>
        /// Construtor do controller
        /// </summary>
        /// <param name="produtoService">Servico de produto</param>
        /// <param name="mapper">AutoMapper</param>
        public ProdutoController(IMapper mapper, IProdutoService produtoService)
        {
            this.Service = produtoService;
            this.AutoMapper = mapper;
        }

        // GET: api/Produto
        /// <summary>
        /// Lista todos os produtos da base de dados
        /// </summary>
        /// <returns>Lista de produtos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Produto> listaProdutos = this.Service.GetAll();
                IEnumerable<ProdutoViewModel> retorno = this.AutoMapper.Map<IEnumerable<ProdutoViewModel>>(listaProdutos);

                return new OkObjectResult(retorno);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os itens paginadamente
        /// </summary>
        /// <param name="pageNumber">número da página</param>
        /// <param name="itensPerPage">Itens por página</param>
        /// <returns></returns>        
        [HttpGet("pagina/{pageNumber}/itensporpagina/{itensPerPage}")]
        public IActionResult PagedGet(int pageNumber, int itensPerPage)
        {
            try
            {
                PagedList<Produto> listaProdutos = this.Service.PagedGetAll(pageNumber, itensPerPage);
                PagedList<ProdutoViewModel> retorno = this.AutoMapper.Map<PagedList<ProdutoViewModel>>(listaProdutos);

                return new OkObjectResult(retorno);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        // GET: api/Produto/5
        /// <summary>
        /// Obtém um produto pelo Id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Produto encontrado</returns>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            try
            {
                ObjectId objID;
                if (!ObjectId.TryParse(id, out objID))
                {
                    return new BadRequestObjectResult("Id inválido.");
                }

                Produto umProduto = this.Service.Get(id);
                if (umProduto == null)
                {
                    return NotFound();
                }

                ProdutoViewModel retorno = this.AutoMapper.Map<ProdutoViewModel>(umProduto);
                return new OkObjectResult(retorno);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        // POST: api/Produto
        /// <summary>
        /// Cadastra um novo produto
        /// </summary>
        /// <param name="produto">Produto para cadastrar</param>
        [HttpPost]
        public IActionResult Post([FromBody]ProdutoViewModel produto)
        {
            try
            {

                if (produto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                Produto umProduto = this.AutoMapper.Map<Produto>(produto);
                this.Service.Save(umProduto);
                produto = AutoMapper.Map<ProdutoViewModel>(umProduto);

                return CreatedAtRoute("Get", new { id = umProduto.Id }, produto);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }

        }

        // PUT: api/Produto/5
        /// <summary>
        /// Atualiza o produto
        /// </summary>
        /// <param name="id">Id do produto para atualizar</param>
        /// <param name="produto">Dados do produto para alterar</param>
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody]ProdutoViewModel produto)
        {
            try
            {

                ObjectId objID;
                if (!ObjectId.TryParse(id, out objID))
                {
                    return new BadRequestObjectResult("Id inválido.");
                }

                if (produto == null || produto.Id.ToString() != id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                if (this.Service.Get(produto.Id.ToString()) == null)
                {
                    return NotFound();
                }

                produto.Id = id;
                Produto umProduto = this.AutoMapper.Map<Produto>(produto);

                this.Service.Save(umProduto);
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }

        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Exclui o produto
        /// </summary>
        /// <param name="id">Id do produto que será excluido</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                ObjectId objID;
                if (!ObjectId.TryParse(id, out objID))
                {
                    return new BadRequestObjectResult("Id inválido.");
                }

                Produto umProduto = this.Service.Get(id);
                if (umProduto == null)
                {
                    return NotFound();
                }

                this.Service.Delete(id);
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
