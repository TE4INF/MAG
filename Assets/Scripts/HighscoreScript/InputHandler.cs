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


    public void AddNameToList(){
        highscoreHandler.AddHighscoreIfPossible (new InputEntry(waveSpawner.currentWaveNumber, nameInput.text, PlayerMovement.Kills));
        nameInput.text = "";
    }
}