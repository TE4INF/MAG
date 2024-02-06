using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField] GameObject HighscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject> ();

    private void OnEnable(){
        HighscoreHandler.onHighscoreListChanged += UpdateUI;
    }

    private void OnDisable(){
        HighscoreHandler.onHighscoreListChanged -= UpdateUI;
    }

    private void UpdateUI (List<InputEntry> list){
        for (int i = 0; i < list.Count; i++)
        {
            InputEntry el = list[i];

            if(el.points > 0)
            {
                if(i >=uiElements.Count)
                {
                    var inst = Instantiate (HighscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                    uiElements.Add (inst);
                }

                var texts = uiElements[i].GetComponentInChildren<Text>();
                texts[0].text = el.Waves.ToString();
                texts[1].text = el.player;
                texts[2].text = el.Kills.ToString();
            }
        }
    }
}
