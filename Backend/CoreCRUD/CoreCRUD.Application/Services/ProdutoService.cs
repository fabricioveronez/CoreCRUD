using CoreCRUD.Application.Interfaces.Services;
using CoreCRUD.Application.Interfaces.Services.Base;
using CoreCRUD.Domain.Entities;
using CoreCRUD.Domain.Repositories;

namespace CoreCRUD.Application.Services
{
    /// <summary>
    /// Classe de serviço da entidade produto
    /// </summary>
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        public ProdutoService(IProdutoRepository repository) : base(repository)
        {
        }
    }
}
