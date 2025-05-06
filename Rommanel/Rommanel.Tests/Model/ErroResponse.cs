using Newtonsoft.Json;

namespace Rommanel.Tests.Model
{
    public class ErroResponse
    {
        [JsonProperty("erro")]
        public string Erro { get; set; }
    }
}
