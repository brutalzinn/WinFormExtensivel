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
        public string id { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public string @default { get; set; }
    }

    public class Action
    {
        public string id { get; set; }
        public string name { get; set; }
        public string prefix { get; set; }
        public string type { get; set; }
        public bool tryInline { get; set; }
        public string format { get; set; }
        public List<Datum> data { get; set; }
        public bool isForm { get; set; }


        public Control ObterControle()
        {
            switch (this.type)
            {
                case "button":
                    return new Button()
                    {
                        Name = this.id,
                        Text = this.name,
                        AutoSize = true
                    };
                case "text":
                    return new TextBox()
                    {
                        Name = this.id,
                        Text = this.name,
                        AutoSize = true

                    };
                default:
                    return null;
            }
        }
    }

    public class State
    {
        public string id { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public string @default { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Action> actions { get; set; }
        public List<State> states { get; set; }
    }
    public class PluginEntryPoint
    {
        public string version { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string plugin_start_cmd { get; set; }
        public List<Category> categories { get; set; }


    }



    
}
