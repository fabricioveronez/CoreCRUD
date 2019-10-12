using CoreCRUD.Api.Validators;
using CoreCRUD.Api.ViewModel;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreCRUD.Test.Unit.Validators
{
    public class ProdutoValidatorTests
    {

        ProdutoViewModelValidator validador = new ProdutoViewModelValidator();

        [Theory]
        [InlineData("Geladeira W600",
            "Essa é a descrição da geladeira W600",
            new string[] { "Geladeiras" },
            2500)]
        public void EntidadesValidas(string nome, string descricao, string[] categorias, double preco)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel()
            {
                Categorias = categorias,
                Descricao = descricao,
                Nome = nome,
                Preco = preco
            };

            ValidationResult resultado = validador.Validate(viewModel);

            Assert.True(resultado.IsValid);
        }

        [Theory]
        [InlineData("", "Essa é a descrição da geladeira W600", new string[] { "Geladeiras" }, 2500)]
        [InlineData("Geladeira W600", "", new string[] { "Geladeiras" }, 2500)]
        [InlineData("Geladeira W600", "", new string[] { }, 2400)]
        [InlineData("Geladeira W600", "", new string[] { "Geladeiras" }, 0)]
        public void EntidadesInValidas(string nome, string descricao, string[] categorias, double preco)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel()
            {
                Categorias = categorias,
                Descricao = descricao,
                Nome = nome,
                Preco = preco
            };

            ValidationResult resultado = validador.Validate(viewModel);

            Assert.False(resultado.IsValid);
        } 
    }
}
