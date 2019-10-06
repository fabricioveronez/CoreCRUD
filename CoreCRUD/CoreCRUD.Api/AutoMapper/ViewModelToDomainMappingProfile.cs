using AutoMapper;
using CoreCRUD.Api.ViewModel;
using CoreCRUD.Infrastructure.Collections;
using CoreCRUD.Domain.Entities;

namespace CoreCRUD.Api.AutoMapper
{
    /// <summary>
    /// Classe que mapeia os dados do view model para o domain
    /// </summary>
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<PagedList<ProdutoViewModel>, PagedList<Produto>>();
        }
    }
}
