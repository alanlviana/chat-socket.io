const app = require('express')()
const http = require('http').createServer(app);
const io = require('socket.io')(http);

app.get('/', (req, res) => {
    res.sendFile(__dirname + '/index.html');
});

io.on('connection', (socket) => {
  console.log('a user connected');
  socket.on('disconnect', () => {
      console.log('user disconnected')
  })

  socket.on('chat message', (msg) => {
    io.emit('chat message', msg);
  });

  socket.on('initialize', (data) => {
    io.emit('Host_Initialize', data)
  });

  socket.on('log', (data) => {
    io.emit('Host_Log', data)
  });

  socket.on('Host_Result', (host_data) => {
    console.log('Host_Result <', {host_data})
    io.emit('Web_Result', host_data)
  })
});

http.listen(3000, () => {
    console.log('listening on *:3000')
})