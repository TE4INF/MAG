using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] playerMovement PlayerMovement;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField mail;


    public string GetScoreAsString()
    {
        // Create a new InputEntry with the current score
        InputEntry entry = new InputEntry(waveSpawner.currentWaveNumber, nameInput.text, PlayerMovement.Kills);
        
        // Return the score as a string representation
        return $"Player: {entry.playerName}, Kills: {entry.kills}";
    }

    public string GetMailAsString()
    {
        
        // Create a new InputEntry with the current score
        MailEntry entry = new MailEntry(mail.text, nameInput.text);
        
        // Return the score as a string representation
        return $"Player: {entry.playerName}, mail: {entry.Gmail}";
    }

    public void AddNameToList()
    {
        highscoreHandler.AddHighscoreIfPossible(new InputEntry(waveSpawner.currentWaveNumber, nameInput.text, PlayerMovement.Kills));
        nameInput.text = "";
        mail.text = "";
    }
}