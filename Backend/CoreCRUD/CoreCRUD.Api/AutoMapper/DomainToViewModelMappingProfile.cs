using AutoMapper;
using CoreCRUD.Api.ViewModel;
using CoreCRUD.Infrastructure.Collections;
using CoreCRUD.Domain.Entities;

namespace Equinox.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// Classe que mapeia os dados do domain para o view model
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>();
            CreateMap<PagedList<Produto>, PagedList<ProdutoViewModel>>();
        }
    }
}
