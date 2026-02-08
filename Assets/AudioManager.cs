using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
        
        audioSource = GetComponent<AudioSource>();
    }
    

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return;
        
        audioSource.loop = false;
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
