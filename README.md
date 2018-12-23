# Simple Communicator
Send a single message between devices via TCP port listening

NOTE: The listening device will stop listening after the first message is received - this program is designed for single-execution events.


## Library reference
Listener Example:
```
using SimpleComm;
. . .
const int PORT_NO = 4000;
const string SERVER_IP = "127.0.0.1";
. . .

Listener listener = new Listener();
string message = listener.Listen(SERVER_IP, PORT_NO);
// Now use the `message` variable to execute functions as needed
```

Sender Example:
```
using SimpleComm;
. . .
const int PORT_NO = 4000;
const string SERVER_IP = "127.0.0.1";
. . .

Sender sender = new Sender();
sender.Send(args[0], SERVER_IP, PORT_NO);
```

<br>

# Compiling Changes
## SimpleComm.cs to DLL
>`csc -target:library SimpleComm.cs`

## Test.cs to EXE
>`csc Test.cs /r:SimpleComm.dll`

<br>

# Testing
## Listener: In one terminal, execute:
>`Test.exe`

OSX:
>`mono Test.exe`

## Sender: In another terminal, execute:
>`Test.exe command`

OSX:
>`mono Test.exe command`