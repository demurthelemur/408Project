using System;

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
}

