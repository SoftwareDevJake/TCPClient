# TCPClient

1. Assets/Scripts/Client.cs : InitializeClientData() is a part you initialize all the added info from ServerPackets(enum parts), ch(Client Handle where it receives data from Server).
Also, make sure you have server computer's ip address and port number.

2. Assets/Scripts/ClientHandle.cs : This is a part where you deal with info you received from Server.

3. Assets/Scripts/ClientSend.cs : This is a part where acutally sending data back to Server.

4. Assets/Scripts/Packet.cs : This should have same enum of ServerPackets and ClientPackets as Server part.
