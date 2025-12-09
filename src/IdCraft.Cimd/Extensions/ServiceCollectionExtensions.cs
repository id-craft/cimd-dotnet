using IdCraft.Cimd.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdCraft.Cimd.Extensions
{
    /// <summary>
    /// Extension methods for configuring CIMD services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the client identifier validator to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <remarks>
        /// This method registers <see cref="IClientIdentifierValidator"/> as a singleton service
        /// because the validator is stateless and thread-safe.
        /// </remarks>
        public static IServiceCollection AddClientIdentifierValidator(this IServiceCollection services)
        {
            services.TryAddSingleton<IClientIdentifierValidator, ClientIdentifierValidator>();
            return services;
        }
    }
}
