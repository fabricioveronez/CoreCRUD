using CoreCRUD.Infrastructure.Entity;
using CoreCRUD.Infrastructure.Repository;
using CoreCRUD.Infrastructure.Collections;
using CoreCRUD.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreCRUD.Application.Interfaces.Services.Base
{
    /// <summary>
    /// Interface base para as interfaces de serviço
    /// </summary>
    /// <typeparam name="T">Entidade do serviço</typeparam>
    public interface IBaseService<T> where T : BaseEntity
    {

        /// <summary>
        /// Método para obter por id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Instância que possui esse id</returns>
        T Get(string id);

        /// <summary>
        /// Método obter todos
        /// </summary>
        /// <returns>Intâncias da busca</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Método para obter instâncias por um determinado filtro
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <returns>Intâncias na base de dados que correspondem ao filtro</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Método obter todos de forma paginada
        /// </summary>
        /// <param name="pageNumber">Número da página a ser exibida</param>
        /// <param name="itensPerPage">Itens por página</param>
        /// <returns>Lista da página solicitada e dados da página</returns>
        PagedList<T> PagedGetAll(int pageNumber, int itensPerPage);

        /// <summary>
        /// Método para obter instâncias por um determinado filtro de forma paginada
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <param name="pageNumber">Número da página a ser exibida</param>
        /// <param name="itensPerPage">Itens por página</param>
        /// <returns>Lista da página solicitada e dados da página</returns>
        PagedList<T> PagedGet(Expression<Func<T, bool>> filter, int pageNumber, int itensPerPage);

        /// <summary>
        /// Método para verificar se existe uma instância com o id passado
        /// </summary>
        /// <param name="id">Id a ser verificado</param>
        /// <returns>Resposta se possui ou não</returns>
        bool Exists(string id);

        /// <summary>
        /// Método para salvar uma instância na base de dados
        /// </summary>
        /// <param name="entity">Instância que será salva</param>
        void Save(T entity);

        /// <summary>
        /// Método para excluir uma instância na base de dados
        /// </summary>
        /// <param name="id">Id da instância a ser excluida</param>
        void Delete(string id);

        /// <summary>
        /// Método para saber quantos itens obedecem o filtro de busca na base de dados
        /// </summary>
        /// <param name="filter">Filtro da busca</param>
        /// <returns>Quantidade de itens que obedecem o fltro</returns>
        long Count(Expression<Func<T, bool>> filter);
    }
}
