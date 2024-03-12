using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public Button button;

    void Start()
    {
        // Initially, disable the button
        button.interactable = false;

        // Register callback methods for input field value changes
        inputField1.onValueChanged.AddListener(delegate { CheckInputFields(); });
    }

    void CheckInputFields()
    {
        // Check if both input fields have text
        if (!string.IsNullOrEmpty(inputField1.text))
        {
            // Enable the button if both input fields have text
            button.interactable = true;
        }
        else
        {
            // Disable the button if any of the input fields is empty
            button.interactable = false;
        }
    }
}