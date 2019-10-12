using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCRUD.Infrastructure.Collections
{
    /// <summary>
    /// Classe genérica para dados de paginação
    /// </summary>
    /// <typeparam name="T">Tipo da classe</typeparam>
    public class PagedList<T>
    {
        public int PageNumber { get; set; }
        public long TotalPages { get; set; }
        public int ItensPerPages { get; set; }
        public IEnumerable<T> Itens { get; set; }
    }
}
