using System;

[Serializable]
public class MailEntry{
    public string playerName;
    public string Gmail;

    public MailEntry (string mail, string name)
    {
        Gmail = mail;        
        playerName = name;
    }
}