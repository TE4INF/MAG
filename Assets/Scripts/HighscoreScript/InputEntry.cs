using System;

[Serializable]
public class InputEntry{
    public string playerName;
    public int kills;
    public int Waves;

    public InputEntry (int Waves, string name, int kills)
    {
        this.Waves = Waves;        
        playerName = name;
        this.kills = kills;
    }
}