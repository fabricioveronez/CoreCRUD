using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCRUD.Api.ViewModel
{
    /// <summary>
    /// Produto View Model
    /// </summary>
    public class ProdutoViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public IEnumerable<string> Categorias { get; set; }
        [MinLength(20)]
        public string Descricao { get; set; }
    }
}
