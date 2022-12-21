using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Player
{
    public string playerName { get; set; }
    public Socket socket { get; set; }

    public double playerScore;
    public Player(string userName, Socket socket)
    {
        this.playerName = userName;
        this.socket = socket;
        this.playerScore = 0;
    }

    public static Player createNewPlayer(Socket Client)
    {
        Byte[] buffer = new Byte[256];
        Client.Receive(buffer);
        string playerName = Encoding.Default.GetString(buffer);
        playerName = playerName.Trim('\0');
        Player tempPlayer = new Player(playerName, Client);
        return tempPlayer;
    }
}

