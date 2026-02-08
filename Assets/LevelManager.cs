using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public List<AudioClip> leveSounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this is the first time
        if (Instance == null)
        {
            Instance = this;
            // keep alive between scene
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // make sure no more than one instance exist
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
