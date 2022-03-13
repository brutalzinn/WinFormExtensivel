using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormExtensivel.Plugins.Models
{
    public static class PluginListConfig
    {
        private static List<PluginConfig> Config { get; set; } = new List<PluginConfig>();
        private const string SAVE_FILE = "database.json";

        public static PluginConfig ObterConfig(string Id)
        {
            return Config.FirstOrDefault(e => e.Id.Equals(Id));
        }

        public static void AdicionarConfig(PluginConfig _config)
        {
            var config = Config.FirstOrDefault(e => e.Id == _config.Id);
            if(config != null)
            {
                config.Configs = _config.Configs;
                SalvarConfig();
                return;
            }
            Config.Add(_config);
            SalvarConfig();
        }

        public static void CarregarConfig()
        {
                string content = File.ReadAllText(SAVE_FILE);
                Config = JsonSerializer.Deserialize<List<PluginConfig>>(content);
         
        }
        public static void SalvarConfig()
        {
            File.WriteAllText(SAVE_FILE, JsonSerializer.Serialize(Config));
        }

    }
}
