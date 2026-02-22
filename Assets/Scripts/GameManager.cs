using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenu;
    public GameObject[] EnemyPrefab;
    
    string PauseMenuButton = "PauseGame";

    public float SpawnDelay = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowPauseMenu();
        
       // PlayerPrefs.DeleteKey("TotalTimeInt");
       TotalTime = PlayerPrefs.GetInt("TotalTimeInt", 0);
    }

    public float TotalTime = 0;

    private IEnumerator SaveTotalTimePlayed()
    {
        yield return new WaitForSeconds(1);
        
        PlayerPrefs.SetInt("TotalTimeInt", (int)TotalTime);
        PlayerPrefs.Save();
        
        Debug.Log(TotalTime);
        StartCoroutine(SaveTotalTimePlayed());
    }


    void ShowPauseMenu()
    {
        HUD.SetActive(false);
        PauseMenu.SetActive(true);

        Time.timeScale = 0;
    }
    
    public void ShowHUD()
    {
        StartCoroutine(SaveTotalTimePlayed());
        
        if (LevelManager.Instance.leveSounds.Count > 0)
        {
            AudioClip clipToPlay = LevelManager.Instance.leveSounds[0];
            AudioManager.Instance.PlayMusic(clipToPlay);
        }

        HUD.SetActive(true);
        PauseMenu.SetActive(false);

        Time.timeScale = 1;
        
       // StartCoroutine(SpawnEnemies(10));
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
        TotalTime += Time.deltaTime;
        
        if (Input.GetButtonDown(PauseMenuButton))
        {
            ShowPauseMenu();
        }
    }
    
    

    IEnumerator SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab[1], new Vector3(i * 0.5f, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

}
