import socketio

class WinFormExtension:

    def __init__(self, plugin_id):
        self.plugin_id = plugin_id
        self.sio = socketio.Client()

    def events(self):
        @self.sio.event
        def connection(data):
            print('connection established')
            self.sio.emit('register', self.plugin_id)

        @self.sio.event
        def echo(data):
            print('message received with ', data)
            #self.sio.emit('input', {'response': 'my response'})

        @self.sio.event
        def disconnect(data):
            print('disconnected from server')

    def Start(self):
        self.sio.connect('http://127.0.0.1:5555')
        self.sio.wait()



teste = WinFormExtension("meuplugin")
teste.events()
teste.Start()





