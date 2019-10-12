using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using CoreCRUD.Infrastructure.Entity;
using CoreCRUD.Infrastructure.Collections;

namespace CoreCRUD.Infrastructure.Repository
{
    /// <summary>
    /// Classe base para as classes de repositório
    /// </summary>
    /// <typeparam name="T">Entidade do repositório</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private IDbContext DbContext { get; set; }
        private IMongoCollection<T> Collection { get; set; }

        public BaseRepository(IDbContext context)
        {
            this.DbContext = context;
            this.Collection = context.Context.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// Método para obter por id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Instância que possui esse id</returns>
        public T Get(string id)
        {
            return this.Collection.Find(Builders<T>.Filter.Eq(p => p.Id, id)).FirstOrDefault();
        }

        /// <summary>
        /// Método obter todos
        /// </summary>
        /// <returns>Intâncias na base de dados</returns>
        public IEnumerable<T> GetAll()
        {
            var result = this.Collection.Find(FilterDefinition<T>.Empty);

            if (result == null)
                return new List<T>();

            return result.ToList();
        }

        /// <summary>
        /// Método para obter instâncias por um determinado filtro
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <returns>Intâncias na base de dados que correspondem ao filtro</returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            var result = this.Collection.FindSync(filter);

            if (result == null)
                return new List<T>();

            return result.ToList();
        }

        /// <summary>
        /// Método para verificar se existe uma instância com o id passado
        /// </summary>
        /// <param name="id">Id a ser verificado</param>
        /// <returns>Resposta se possui ou não</returns>
        public bool Exists(string id)
        {
            return this.Get(id) != null;
        }

        /// <summary>
        /// Método para salvar uma instância na base de dados
        /// </summary>
        /// <param name="entity">Instância que será salva</param>
        public void Save(T entity)
        {
            if (entity.Id == null || entity.Id.Equals(""))
            {
                this.Collection.InsertOne(entity);
            }
            else
            {
                this.Collection.FindOneAndReplace(obj => obj.Id.Equals(entity.Id), entity);
            }
        }

        /// <summary>
        /// Método para excluir uma instância na base de dados
        /// </summary>
        /// <param name="id">Id da instância a ser excluida</param>
        public void Delete(string id)
        {
            this.Collection.FindOneAndDelete(obj => obj.Id.Equals(id));
        }

        /// <summary>
        /// Método para saber quantos itens obedecem o filtro de busca na base de dados
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <returns>Quantidade de itens que obedecem o fltro</returns>
        public long Count(Expression<Func<T, bool>> filter)
        {
            return this.Collection.Count(filter);
        }


        /// <summary>
        /// Método obter todos de forma paginada
        /// </summary>
        /// <param name="pageNumber">Número da página a ser exibida</param>
        /// <param name="itensPerPage">Itens por página</param>
        /// <returns>Lista da página solicitada e dados da página</returns>
        public PagedList<T> PagedGetAll(int pageNumber, int itensPerPage)
        {
            PagedList<T> pagedList = new PagedList<T>
            {
                ItensPerPages = itensPerPage,
                PageNumber = pageNumber
            };

            var result = this.Collection.Find(FilterDefinition<T>.Empty);

            if (result == null)
            {
                pagedList.Itens = new List<T>();
                pagedList.TotalPages = 0;
            }
            else
            {
                pagedList.TotalPages = result.Count() / itensPerPage;
                pagedList.Itens = result.Skip(itensPerPage * (pageNumber - 1)).Limit(itensPerPage).ToList();
            }

            return pagedList;
        }

        /// <summary>
        /// Método para obter instâncias por um determinado filtro de forma paginada
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <param name="pageNumber">Número da página a ser exibida</param>
        /// <param name="itensPerPage">Itens por página</param>
        /// <returns>Lista da página solicitada e dados da página</returns>
        public PagedList<T> PagedGet(Expression<Func<T, bool>> filter, int pageNumber, int itensPerPage)
        {
            PagedList<T> pagedList = new PagedList<T>
            {
                ItensPerPages = itensPerPage,
                PageNumber = pageNumber
            };

            var result = this.Collection.Find(filter);

            if (result == null)
            {
                pagedList.Itens = new List<T>();
                pagedList.TotalPages = 0;
            }
            else
            {
                pagedList.TotalPages = result.Count();
                pagedList.Itens = result.Skip(itensPerPage * (pageNumber - 1)).Limit(itensPerPage).ToList();
            }

            return pagedList;
        }
    }
}
