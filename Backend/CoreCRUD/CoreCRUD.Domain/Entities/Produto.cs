using CoreCRUD.Infrastructure.Entity;
using System.Collections.Generic;

namespace CoreCRUD.Domain.Entities
{
    /// <summary>
    /// Classe produto
    /// </summary>
    public class Produto : BaseEntity
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Categoria { get; set; }
        public string  Descricao { get; set; }
    }
}
