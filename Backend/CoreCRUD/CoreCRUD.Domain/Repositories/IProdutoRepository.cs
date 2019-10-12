using CoreCRUD.Domain.Entities;
using CoreCRUD.Infrastructure.Repository;

namespace CoreCRUD.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de produto
    /// </summary>
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
    }
}
