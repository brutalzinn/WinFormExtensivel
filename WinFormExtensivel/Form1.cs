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
                            RegistrarPlugin(token + "");
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
                        EscreverLog(richTextBox1, "Client disconnected!");
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

        void EscreverPlugin(ListBox listBox, string output)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => EscreverPlugin(listBox, output)));
                return;
            }
            listBox.Items.Add(output);

        }
        private void RegistrarPlugin(string pluginId)
        {

            EscreverPlugin(listBox1,pluginId);
            //registrando plugin de teste


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
