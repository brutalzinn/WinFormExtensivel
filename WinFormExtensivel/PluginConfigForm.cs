using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormExtensivel.Plugins;

namespace WinFormExtensivel
{
    public partial class PluginConfigForm : Form
    {
        public PluginConfigForm(Plugins.Action action)
        {
            InitializeComponent();
            if (action != null)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                this.Controls.Add(tableLayoutPanel1);

                foreach (var item in action.data)
                {
                    tableLayoutPanel1.Controls.Add(item.ObterControle());
                }

            }
        }

        private void PluginConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                if (control is TextBox txtbox && txtbox.Tag is Datum _data)
                {
                    
                }
            }
        }
    }
}
