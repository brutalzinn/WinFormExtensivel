using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormExtensivel.Plugins
{
    public class PluginTag
    {

        public PluginEntryPoint Info { get; set; }
        
        public Actions Action { get;  set; }

        public PluginTag(PluginEntryPoint info, Actions action)
        {
            Action = action;
            Info = info;
        }

        public PluginTag()
        {

        }
    }
}
