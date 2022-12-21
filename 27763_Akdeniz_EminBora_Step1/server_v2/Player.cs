using System;

public class Player
{
    public string player_name { get; set; }
    public Socket socket { get; set; }

    public double score;
    public Player(string player_name, Socket socket)
    {
        this.player_name = player_name;
        this.socket = socket;
        this.score = 0;
    }
}

