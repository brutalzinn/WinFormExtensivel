import socket
import json
import threading
from pymitter import EventEmitter


class WinFormExtension:

    def __init__(self, plugin_id):
        self.plugin_id = plugin_id
        self.Event = EventEmitter(wildcard=True, new_listener=True, max_listeners=-1)
        self.s = socket.socket()        
        self.s.connect(('127.0.0.1', 5555))
        threading.Thread(target=self.Client_Handle).start()
        teste = json.dumps({"Event":"register","Content":self.plugin_id}).encode()
        self.s.send(teste)
        print(teste)
    def Client_Handle(self):
        while True:
            msg = self.s.recv(2048).decode()
            data = json.loads(msg)
            self.Event.emit(data["Event"], data)
            print(msg)



_WinFormExtension = WinFormExtension("meuplugin")

@_WinFormExtension.Event.on("OnAction")
def OnAction(data):
    print("Botão clicado..",data)

@_WinFormExtension.Event.on("OnConnected")
def OnAction(data):
    print("Conectado..",data)


