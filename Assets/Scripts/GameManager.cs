using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenu;
    
    string PauseMenuButton = "PauseGame";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowPauseMenu();
    }

    void ShowPauseMenu()
    {
        HUD.SetActive(false);
        PauseMenu.SetActive(true);

        Time.timeScale = 0;
    }
    
    public void ShowHUD()
    {
        HUD.SetActive(true);
        PauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false; // stop Play Mode
        // only in build not in Editor
        //Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PauseMenuButton))
        {
            ShowPauseMenu();
        }
    }
    
}
