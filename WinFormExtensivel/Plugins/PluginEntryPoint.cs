using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormExtensivel.Plugins
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("default")]
        public string Default { get; set; }

        public Control ObterControle()
        {
            switch (this.Type)
            {
                case "button":
                    return new Button()
                    {
                        Name = this.Id,
                        Text = this.Label,
                        AutoSize = true
                    };
                case "text":
                    var tlp = new TableLayoutPanel();
                    tlp.Tag = this;
                    tlp.Controls.Add(new Label() { Text = this.Label, AutoSize = true });
                    tlp.Controls.Add(new TextBox() { Name= this.Id, Dock = DockStyle.Fill, Text = this.Default });
                    return tlp;
                default:
                    return null;
            }
        }

    }

    public class Actions
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("tryInline")]
        public bool TryInline { get; set; }

        [JsonPropertyName("isForm")]
        public bool IsForm { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("data")]
        public List<Datum> Data { get; set; }


        public Control ObterControle()
        {
            switch (this.Type)
            {
                case "button":
                    return new Button()
                    {
                        Name = this.Id,
                        Text = this.Name,
                        AutoSize = true,
                        Dock = DockStyle.Fill
                    };
                case "text":
                    var tlp = new TableLayoutPanel();
                    tlp.Controls.Add(new Label() { Text = this.Name, AutoSize = true });
                    tlp.Controls.Add(new TextBox() { Dock = DockStyle.Fill });
                    return tlp;
                default:
                    return null;
            }
        }
    }

    public class State
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("default")]
        public string Default { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("actions")]
        public List<Actions> Actions { get; set; }

        [JsonPropertyName("states")]
        public List<State> States { get; set; }
    }
    public class PluginEntryPoint
    {
 
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("plugin_start_cmd")]
        public string PluginStartCmd { get; set; }

        [JsonPropertyName("categories")]
        public List<Category> Categories { get; set; }


    }



    
}
