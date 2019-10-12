using AutoMapper;

namespace Equinox.Application.AutoMapper
{
    /// <summary>
    /// Classe configuradora do AutoMapper
    /// </summary>
    public class AutoMapperConfig
    {               
        /// <summary>
        /// Registra os mapeadores
        /// </summary>
        /// <returns>Config de mapeamento</returns>
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
