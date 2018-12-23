#Simple Communicator
Send messages between devices via TCP port listening

##Test
><br>`Test.exe 127.0.0.1 4000`
OSX:
><br>`mono Test.exe 127.0.0.1 4000`

##Compile SimpleComm.cs to DLL
><br>`csc -target:library SimpleComm.cs`

##Compile Test.cs to EXE
><br>`csc Test.cs /r:SimpleComm.dll`