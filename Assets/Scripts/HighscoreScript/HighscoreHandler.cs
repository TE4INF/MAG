using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HighscoreHandler : MonoBehaviour
{
    InputHandler inputhandler;

    List<InputEntry> highscoreList = new List<InputEntry>();
    [SerializeField] int maxCount = 6;
    [SerializeField] string filename;

    public delegate void OnHighscoreListChanged(List<InputEntry> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;


    private void Start()
    {
        LoadHighscores();

        inputhandler = GetComponent<InputHandler>();
    }

    private void LoadHighscores()
    {
        highscoreList = FileHandler.ReadListFromJSON<InputEntry>(filename);

        while (highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }

        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(highscoreList);
        }
    }

    public void saveHighscore(string pst)
    {
        StartCoroutine(PosttData(pst));
    }

    public IEnumerator PosttData(string dataStr)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", dataStr);

        UnityWebRequest www = UnityWebRequest.Post("https://itkompisar.ktcprojekt.se/php/post.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;

            Debug.Log(results);
        }

        www.Dispose();
    }

    private void SaveHighscore()
    {
        string scoreString = inputhandler.GetScoreAsString();
        FileHandler.SaveToJSON<InputEntry>(highscoreList, filename);
        saveHighscore(scoreString);
        Debug.Log(scoreString);
    }


    public void AddHighscoreIfPossible(InputEntry element)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= highscoreList.Count || element.kills > highscoreList[i].kills)
            {
                highscoreList.Insert(i, element);

                while (highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(maxCount);
                }

                SaveHighscore();

                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(highscoreList);
                }

                break;
            }
        }
    }
}
