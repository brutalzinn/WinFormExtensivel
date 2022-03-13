using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormExtensivel.Plugins;
using WinFormExtensivel.Plugins.Models;

namespace WinFormExtensivel
{
    public partial class PluginConfigForm : Form
    {
        private PluginTag Plugin { get; set; }

        public PluginConfigForm(PluginTag plugin)
        {
            InitializeComponent();
            if (plugin != null)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                foreach (var item in plugin.Action.Data)
                {
                    //sistema de configuração de plugins.
                    var control = item.ObterControle();
                    var config = PluginListConfig.ObterConfig(plugin.Info.Id);
                    var valor = config?.ObterValor(item.Id).ToString();
                    if (control.Tag is Datum _data && _data.Type == "text")
                    {
                        var _control_filho = control.Controls.Find(item.Id, true)[0];
                        _control_filho.Text = valor;
                    }

                    tableLayoutPanel1.Controls.Add(control);
                }
                this.Plugin = plugin;

            }
        }

        private void PluginConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //List<Datum> data = new List<Datum>();
            var config = new PluginConfig(Plugin);
            config.Configs = new();
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control.Tag is Datum _data && _data.Type == "text")
                {
                        var _control =  control.Controls.Find(_data.Id, true)[0];
                        config.Configs.Add(_data.Id, _control.Text);
                }
            }

            PluginListConfig.AdicionarConfig(config);
            

            //File.WriteAllText(@"database.json", JsonSerializer.Serialize(config));

        }
    }
}
