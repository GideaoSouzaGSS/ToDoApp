namespace TodoApp.Api.Configuration
{

    public class SecretsConfigurationProvider : ConfigurationProvider
    {

        private readonly IEnumerable<string> _secretKeys;

        public SecretsConfigurationProvider(IEnumerable<string> secretKeys)
        {
            _secretKeys = secretKeys;
        }

        public override void Load()
        {
            foreach (var key in _secretKeys)
            {
                // Lê a variável de ambiente diretamente com a chave original (ex: "ConnectionStrings__Aplicacao")
                var secretPath = Environment.GetEnvironmentVariable(key);

                // Verifique se o valor existe e se o arquivo no caminho existe
                if (!string.IsNullOrEmpty(secretPath) && File.Exists(secretPath))
                {
                    Data[key] = File.ReadAllText(secretPath).Trim();
                }
            }
        }
    }

    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDockerSecrets(this IConfigurationBuilder builder, params string[] secretKeys)
        {
            return builder.Add(new SecretsConfigurationSource(secretKeys));
        }
    }

    public class SecretsConfigurationSource : IConfigurationSource
    {
        private readonly IEnumerable<string> _secretKeys;

        public SecretsConfigurationSource(IEnumerable<string> secretKeys)
        {
            _secretKeys = secretKeys;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SecretsConfigurationProvider(_secretKeys);
        }

    }

}
