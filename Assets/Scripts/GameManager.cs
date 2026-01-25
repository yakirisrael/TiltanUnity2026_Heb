using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenu;
    public GameObject EnemyPrefab;
    
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
        
        SpawnEnemies(10);
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

    void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, new Vector3(i * 0.5f, 0, 0), Quaternion.identity);
        }
    }

}
