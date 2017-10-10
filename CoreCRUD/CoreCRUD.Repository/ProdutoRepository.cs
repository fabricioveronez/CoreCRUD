using CoreCRUD.Domain.Entities;
using CoreCRUD.Domain.Repositories;
using CoreCRUD.Infrastructure.Repository;

namespace CoreCRUD.Repository
{
    /// <summary>
    /// Classe do repositório de produto
    /// </summary>
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {

        private IDbContext DbContext { get; set; }

        public ProdutoRepository(IDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
