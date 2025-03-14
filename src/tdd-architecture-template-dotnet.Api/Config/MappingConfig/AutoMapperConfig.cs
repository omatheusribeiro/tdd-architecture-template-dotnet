using tdd_architecture_template_dotnet.Application.Mappings;

namespace tdd_architecture_template_dotnet.Config.MappingConfig
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile),
                    typeof(ViewModelToDomainMappingProfile));
        }
    }
}
