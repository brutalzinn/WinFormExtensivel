using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormExtensivel
{
    public class MessageChannel : EventArgs
    {
        public string Channel { get; set; }
        public object Value { get; set; }
    }
    public class GlobalEventsHandle
    {
 
        public event EventHandler nova_mensagem;
        // Define the delegate collection.
        public void OnThresholdReached(MessageChannel e)
        {
            EventHandler handler = nova_mensagem;
            handler?.Invoke(this, e);
        }

        // Define the MouseUp event property.

    }
}
