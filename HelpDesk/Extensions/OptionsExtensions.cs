using HelpDesk.Common.Options;

namespace HelpDesk.Extensions;

public static class OptionsExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Registers options with the DI container
        ///     using the key from the <see cref="IKeyableOptions" /> interface.
        ///     The options are validated on startup.
        /// </summary>
        /// <typeparam name="T">The type of the options to register.</typeparam>
        public void AddKeyableOptions<T>() where T : class, IKeyableOptions
        {
            services.AddOptions<T>()
                .BindConfiguration(T.Key)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
    }
}
