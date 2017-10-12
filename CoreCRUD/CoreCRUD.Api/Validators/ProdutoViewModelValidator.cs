using CoreCRUD.Api.ViewModel;
using FluentValidation;
using System.Linq;

namespace CoreCRUD.Api.Validators
{
    /// <summary>
    /// Validados para a classe produto view model
    /// </summary>
    public class ProdutoViewModelValidator : AbstractValidator<ProdutoViewModel>
    {
        /// <summary>
        /// Monto a validação do produto
        /// </summary>
        public ProdutoViewModelValidator()
        {
            // Valida que o campo nome deve ser preenchido
            RuleFor(produto => produto.Nome).NotNull().NotEmpty().WithMessage("Nome do produto é obrigatório.");
            // Valida que o preço deve ser maior que 0
            RuleFor(produto => produto.Preco).GreaterThan(0).WithMessage("Preço deve ser maior que zero.");
            // Valida que a categoria deve ter pelo 1 item.
            RuleFor(produto => produto.Categorias).Must(obj => obj.Count() > 0).WithMessage("Produto deve ter pelo menos uma categoria.");
            // Valida a descrição 
            RuleFor(produto => produto.Descricao).NotNull().NotEmpty().MinimumLength(20).WithMessage("A descrição deve ter no mínimo 20 caracteres.");
        }
    }
}
