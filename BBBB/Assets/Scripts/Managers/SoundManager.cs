using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField, Header("Drag into scene. Play music across levels or sound effects once.")]
    public AudioSource musicSource;
    public AudioSource soundEffectSource;
    
    public AudioClip[] soundEffects;
    public AudioClip[] music;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void PlayMusic()
    {        
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(int soundID)
    {
        soundEffectSource.PlayOneShot(soundEffects[soundID]);
    }
    public void PlayRandomEatSFX()
    {
        int eatSoundToPlay = Random.Range(0, 2);
        soundEffectSource.PlayOneShot(soundEffects[eatSoundToPlay]);
    }
}
