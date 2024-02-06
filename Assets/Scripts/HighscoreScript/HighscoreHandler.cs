using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    List<InputEntry> highscoreList = new List<InputEntry> ();
    [SerializeField] int maxCount = 6;
    [SerializeField] string filename;

    public delegate void OnHighscoreListChanged (List<InputEntry> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;


    private void Start(){
        LoadHighscores ();
    }

    private void LoadHighscores(){
        highscoreList = FileHandler.ReadListFromJSON<InputEntry> (filename);

        while (highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt (maxCount);
        }

        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke (highscoreList);
        }
    }

    private void SaveHighscore(){
        FileHandler.SaveToJSON<InputEntry> (highscoreList, filename);
    }

    public void AddHighscoreIfPossible (InputEntry element){
        for (int i = 0; i < maxCount; i++){
            if(i >= highscoreList.Count || element.kills > highscoreList[i].kills)
            {
                highscoreList.Insert (i, element);

                while (highscoreList.Count > maxCount){
                    highscoreList.RemoveAt (maxCount);
                }

                SaveHighscore();

                if (onHighscoreListChanged != null)
                {
                onHighscoreListChanged.Invoke (highscoreList);
                }

                break;
            }
        }
    }
}
