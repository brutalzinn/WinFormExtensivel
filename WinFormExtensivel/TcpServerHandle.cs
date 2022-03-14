using NetCoreServer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WinFormExtensivel.Plugins.Message;

namespace WinFormExtensivel
{
 

    public class TcpChatServer : TcpServer
    {
        public TcpChatServer(IPAddress address, int port) : base(address, port) { }
        private ConcurrentDictionary<string, BaseEmitter> Emitters { get; set; } = new ConcurrentDictionary<string, BaseEmitter>();

        protected override TcpSession CreateSession() { return new TcpChatSession(this); }


     
        protected override void OnError(SocketError error)
        {
            
            Console.WriteLine($"Chat TCP server caught an error with code {error}");
        }
    }

}
