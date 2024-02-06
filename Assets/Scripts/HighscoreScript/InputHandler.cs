using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] int kills = 59;
    [SerializeField] int Waves = 38;
    [SerializeField] string filename;

    //List<InputEntry> entries = new List<InputEntry> ();

    private void Start(){
        //entries = FileHandler.ReadListFromJSON<InputEntry> (filename);
    }

    public void AddNameToList(){
        //entries.Add (new InputEntry (Waves, nameInput.text, Kills));
        highscoreHandler.AddHighscoreIfPossible (new InputEntry(Waves, nameInput.text, kills));
        //FileHandler.SaveToJSON<InputEntry> (entries, filename);
        nameInput.text = "";
    }
}