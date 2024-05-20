using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button click event
        button.onClick.AddListener(SetButtonNameAndLoadScene);
    }

    // Method to handle button click event
    void SetButtonNameAndLoadScene()
    {
        // Set the name of the button
        

        // Load the next scene
        SceneManager.LoadScene("BrightDay");
    }
}
