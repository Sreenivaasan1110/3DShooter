using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : RayCastGun
{
    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button click event
        GetComponent<Button>().onClick.AddListener(ReloadScene);
    }

    // Method to handle button click event
    void ReloadScene()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f;
        laserc = 5;
        count = 0;

    }
}
