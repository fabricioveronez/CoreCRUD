using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoreCRUD.Infrastructure.Entity;
using CoreCRUD.Infrastructure.Repository;
using CoreCRUD.Infrastructure.Collections;

namespace CoreCRUD.Application.Interfaces.Services.Base
{
    /// <summary>
    /// Classe base para as classes de serviço
    /// </summary>
    /// <typeparam name="T">Entidade do serviço</typeparam>
    public class ServiceBase<T> : IBaseService<T> where T : BaseEntity
    {
        private IBaseRepository<T> Repository { get; set; }

        public ServiceBase(IBaseRepository<T> repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Método para saber quantos itens obedecem o filtro de busca na base de dados
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <returns>Quantidade de itens que obedecem o fltro</returns>
        public long Count(Expression<Func<T, bool>> filter)
        {
            return this.Repository.Count(filter);
        }

        /// <summary>
        /// Método para excluir uma instância na base de dados
        /// </summary>
        /// <param name="id">Id da instância a ser excluida</param>
        public void Delete(string id)
        {
            this.Repository.Delete(id);
        }

        /// <summary>
        /// Método para verificar se existe uma instância com o id passado
        /// </summary>
        /// <param name="id">Id a ser verificado</param>
        /// <returns>Resposta se possui ou não</returns>
        public bool Exists(string id)
        {
            return this.Repository.Exists(id);
        }

        /// <summary>
        /// Método para obter por id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Instância que possui esse id</returns>
        public T Get(string id)
        {
            return this.Repository.Get(id);
        }

        /// <summary>
        /// Método para obter instâncias por um determinado filtro
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <returns>Intâncias na base de dados que correspondem ao filtro</returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return this.Repository.Get(filter);
        }

        /// <summary>
        /// Método obter todos
        /// </summary>
        /// <returns>Intâncias da busca</returns>
        public IEnumerable<T> GetAll()
        {
            return this.Repository.GetAll();
        }

        /// <summary>
        /// Método para salvar uma instância na base de dados
        /// </summary>
        /// <param name="entity">Instância que será salva</param>
        public void Save(T entity)
        {
            this.Repository.Save(entity);
        }

        /// <summary>
        /// Método obter todos de forma paginada
        /// </summary>
        /// <param name="pageNumber">Número da página a ser exibida</param>
        /// <param name="itensPerPage">Itens por página</param>
        /// <returns>Lista da página solicitada e dados da página</returns>
        public PagedList<T> PagedGetAll(int pageNumber, int itensPerPage)
        {
            return this.Repository.PagedGetAll(pageNumber, itensPerPage);
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
            return this.Repository.PagedGet(filter, pageNumber, itensPerPage);
        }
    }
}
