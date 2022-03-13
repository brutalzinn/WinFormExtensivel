using Newtonsoft.Json.Linq;
using SocketIOSharp.Common;
using SocketIOSharp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormExtensivel
{
    public partial class Form1 : Form
    {
        private SocketIOServer server { get; set; } = null;

        //TODO: remover isso depois.
        public class PluginCliente
        {
            public string SocketId { get; private set; }
            public string PluginId { get; private set; }

            public PluginCliente(string socketId, string pluginId)
            {
                this.SocketId = socketId;
                this.PluginId = pluginId;
            }

            public override bool Equals(object obj)
            { 
                if (obj == null)
                {
                return false;
                }
            
                if (!(obj is PluginCliente))
                {
                return false;
                }
                var other = (PluginCliente)obj;
                return this.SocketId.Equals(other.SocketId);
            }
        }

        public List<PluginCliente> pluginClientes = new List<PluginCliente>();

        private void DesconectarPlugin(string socketId)
        {
            var plugin = pluginClientes.First(e => e.SocketId.Equals(socketId));
            pluginClientes.Remove(plugin);
           
        }
        private void ConectarPlugin(string socketId, string pluginId)
        {
            if (pluginClientes.Count(e => e.SocketId.Equals(socketId)) == 0)
            {
                pluginClientes.Add(new PluginCliente(socketId, pluginId));
            }

        }
       //TODO: Esse invoqued required tá muito bruto e jogando memória fora. 
       //dar uma olhada depois.
        private void AtualizarPlugins()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => AtualizarPlugins()));
                return;
            }
            listBox1.Items.Clear();
            pluginClientes.ForEach(e => EscreverPlugin(listBox1,e.PluginId));
        }
        public Form1()
        {

            InitializeComponent();
            using (server = new SocketIOServer(new SocketIOServerOption(5555)))
            {
                EscreverLog(richTextBox1, "Servidor em " + server.Option.Port);

                server.OnConnection((socket) =>
                {
                   
                    EscreverLog(richTextBox1, "Cliente conectado.");

                    socket.On("register", (data) =>
                    {
                        foreach (JToken token in data)
                        {
                            ConectarPlugin(socket.Socket.SID, token.ToString());
                            AtualizarPlugins();

                            // RegistrarPlugin(token + "" + socket.Socket.SID);
                        }
                    });

                    socket.On("input", (data) =>
                    {
                        foreach (JToken token in data)
                        {
                            EscreverLog(richTextBox1, token + " ");
                        }

                        socket.Emit("echo", data);
                    });

                

                    socket.On(SocketIOEvent.DISCONNECT, () =>
                    {
                            EscreverLog(richTextBox1, "Cliente " + socket.Socket.SID + "Desconectado.");
                            DesconectarPlugin(socket.Socket.SID);
                            AtualizarPlugins();
                    });

                    //emitindo um handshake basicao
                    socket.Emit("connection", new byte[] { 0, 1, 2, 3, 4, 5 });
                    
                });
            }
            server.Start();
        }
        void EscreverLog(RichTextBox richText, string output)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => EscreverLog(richText, output)));
                return;
            }
                if (String.IsNullOrEmpty(richText.Text))
                richText.AppendText(output);
            else
                richText.AppendText(Environment.NewLine + output);

            richText.ScrollToCaret();
        }

        void EscreverPlugin(ListBox listBox, string pluginId)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => EscreverPlugin(listBox, pluginId)));
                return;
            }
            listBox.Items.Add(pluginId);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
         

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EscreverLog(richTextBox1, $"Enviado:{richTextBox2.Text}");

            server.Emit("echo", richTextBox2.Text);
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
