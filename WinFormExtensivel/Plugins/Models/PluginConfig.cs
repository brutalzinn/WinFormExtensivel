using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinFormExtensivel.Plugins
{
    public class PluginConfig
    {

        [JsonPropertyName("configs")]
        public Dictionary<string, string> Configs { get; set; } = new();

        [JsonPropertyName("id")]
        public string Id { get; set; }

        public PluginConfig(PluginTag _pluginTag)
        {
            Id = _pluginTag.Info.Id;
        }
        public PluginConfig(PluginEntryPoint _pluginTag)
        {
            Id = _pluginTag.Id;
        }
        public PluginConfig()
        {

        }
        public object ObterValor(string chave)
        {
            Configs.TryGetValue(chave, out var val);
            return val ?? string.Empty;
        }

    }
}
