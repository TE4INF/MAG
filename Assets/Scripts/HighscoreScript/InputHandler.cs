using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] int Kills = 59;
    [SerializeField] int Waves = 38;
    [SerializeField] string filename;

    List<InputEntry> entries = new List<InputEntry> ();

    private void Start(){
        entries = FileHandler.ReadListFromJSON<InputEntry> (filename);
    }

    public void AddNameToList(){
        //entries.Add (new InputEntry (Waves, nameInput.text, Kills));
        HighscoreHandler.AddHighscoreIfPossible (new InputEntry(Waves, nameInput.text, Kills));
        //FileHandler.SaveToJSON<InputEntry> (entries, filename);
        nameInput.text = "";
    }
}