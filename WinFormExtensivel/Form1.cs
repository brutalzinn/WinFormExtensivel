using NetCoreServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormExtensivel.Plugins;
using WinFormExtensivel.Plugins.Models;

namespace WinFormExtensivel
{
    public partial class Form1 : Form
    {
        private TcpChatServer server { get; set; } = null;

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
        }

        public List<PluginCliente> pluginClientes = new List<PluginCliente>();

        public List<PluginEntryPoint> pluginsCarregados = new List<PluginEntryPoint>();


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

        public static Form1 _form { get; set; }
        public Form1()
        {

            InitializeComponent();
            _form = this;
    

            using (server = new TcpChatServer(IPAddress.Any, 5555))
            {
                EscreverLog(richTextBox1, "Servidor em " + server.Port);
                EscreverLog(richTextBox1, "Iniciando servidor..");


         
                ////// parte antiga ------
                //server.OnConnection((socket) =>
                //{

                //    EscreverLog(richTextBox1, "Cliente conectado.");

                //    socket.On("register", (data) =>
                //    {
                //        foreach (JToken token in data)
                //        {
                //            ConectarPlugin(socket.Socket.SID, token.ToString());
                //            AtualizarPlugins();

                //            // RegistrarPlugin(token + "" + socket.Socket.SID);
                //        }
                //    });

                //    socket.On("input", (data) =>
                //    {
                //        foreach (JToken token in data)
                //        {
                //            EscreverLog(richTextBox1, token + " ");
                //        }

                //        socket.Emit("echo", data);
                //    });



                //    socket.On(SocketIOEvent.DISCONNECT, () =>
                //    {
                //            EscreverLog(richTextBox1, "Cliente " + socket.Socket.SID + "Desconectado.");
                //            DesconectarPlugin(socket.Socket.SID);
                //            AtualizarPlugins();
                //    });

                //    //emitindo um handshake basicao
                //    socket.Emit("connection", new byte[] { 0, 1, 2, 3, 4, 5 });

                //});
                ////// parte antiga ------

            }
           
            server.Start();
        }

        private void EventoTeste_ThresholdReached(object? sender, EventArgs e)
        {        
            var teste = (MessageChannel)e;
            Debug.WriteLine("Chave:" + teste.Channel + " " + "Valor:" + teste.Value);
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

    
        private void GerarControle(Actions action)
        {
            //var control = action.ObterControle();
            //control.Text = action.name;
            //control.Name = action.id;

     
            //var tlp = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 6 };
            //tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            //tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            //tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            //tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            //tlp.Controls.Add(control);

            //panel1.Controls.Add(tlp);
            //foreach (var item in fields)
            //{
            //     tlp.Controls.Add(new Label() { Text = item, AutoSize = true });
          //  }
            //foreach (var item in fields)
            //{
            //    tlp.Controls.Add(new Label() { Text = item, AutoSize = true });
            //    tlp.Controls.Add(new TextBox() { Dock = DockStyle.Fill });
            //}
        }

        private Control PegarControlador(Category categoria)
        {
            switch (categoria.Id.ToLower())
            {
                case "main":
                    return Main;
                case "config":
                    return Config;
                default:
                    return null;
            }
        }
        

        
        private void Form1_Load(object sender, EventArgs e)
        {

            EscreverLog(richTextBox1, $"Procurando plugins..");
            List<string> dir_plugins = new List<string>();

            Directory.GetDirectories($@"plugins\").ToList().ForEach(x =>
            {
                EscreverLog(richTextBox1, $"Carregando plugins..");
                dir_plugins.Add(x);
            });

            foreach (string file in dir_plugins)
            {
                var config = Directory.GetFiles(file,@"entry.json")[0];
                string content = File.ReadAllText(config);
                var json = JsonSerializer.Deserialize<PluginEntryPoint>(content);
                EscreverLog(richTextBox1, $"{json.Name} Carregado..");
                pluginsCarregados.Add(json);
            }
            PluginListConfig.CarregarConfig();

            foreach (var plugin in pluginsCarregados)
            {
                plugin.Categories.ForEach(categoria => 
                {
                    var tlp = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 4 };
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    categoria.Actions.ForEach(action =>
                    {
                        var control = action.ObterControle();
                        if (control is Button button)
                        {
                            button.Click += Button_Click;
                            button.Tag = new PluginTag(plugin, action);
                        }
                        tlp.Controls.Add(control);
                    });
                    PegarControlador(categoria).Controls.Add(tlp);
                });
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            var button =  (Button)sender;
            if(button.Tag is PluginTag _pluginTag && _pluginTag.Action.IsForm)
            {      
                    var form = new PluginConfigForm(_pluginTag);
                    form.ShowDialog();
                    return;              
            }
            ////// parte antiga ------

            //server.Emit("onAction", JsonSerializer.Serialize(new PluginAction()
            //{
            //    ActionId = button.Name,
            //    Teste = "MAracutaia da boa."
            //}));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EscreverLog(richTextBox1, $"Enviado:{richTextBox2.Text}");
            ////// parte antiga ------

            //server.Emit("echo", richTextBox2.Text);
          

        }
    }
}
