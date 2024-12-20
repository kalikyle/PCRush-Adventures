using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;

    // Background Music
    [Header("Background Musics")]
    public AudioSource backgroundMusicSource;
    public AudioClip mainMenuBackground;
    public AudioClip homeWorldBackground;
    public AudioClip CPUWorldBackground;
    public AudioClip RAMWorldBackground;
    public AudioClip CPUFANWorldBackground;
    public AudioClip GPUWorldBackground;
    public AudioClip STORAGEWorldBackground;
    public AudioClip PSUWorldBackground;
    public AudioClip MBWorldBackground;
    public AudioClip CASEWorldBackground;
    public AudioClip OnhordeBackground;

    public AudioClip CurrentBackground;

    // Sound Effects
    [Header("UIEffects")]
    public AudioSource soundEffectSource;
    public AudioClip buttonSound;
    public AudioClip collectSound;
    public AudioClip InExploreButtonSound;
    public AudioClip Teleport;
    public AudioClip BuynSell;
    public AudioClip NotEnough;
    public AudioClip UseParts;
    public AudioClip Done;
    public AudioClip Count;
    public AudioClip Dialogue;

    [Header("GameEffects")]
    public AudioSource GameEffectSource;
    public AudioClip RunSound;
    public AudioClip attackSound;

    [Header("WalkingSound")]
    public AudioSource walkingSoundSource;
    public AudioClip walkingSound;


    public Slider BackGround;
    public Slider Effects;

    public float backgroundVolume;
    public float effectsVolume;

    private void Awake()
    {
        // Singleton pattern to keep only one instance of SoundManager across all scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps SoundManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        // Play the main menu background music when the game starts
        //PlayBackgroundMusic(mainMenuBackground);
        LoadSoundSettings();
    }

    // Play a specific background music
    public void PlayBackgroundMusic(AudioClip music)
    {
        if (music != null)
        {
            backgroundMusicSource.clip = music;
            backgroundMusicSource.volume = backgroundVolume;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip is null.");
        }
    }

    // Set the volume of the background music (called by the background music slider)
    public void SetBackgroundVolume(float volume)
    {
        backgroundVolume = volume;
        backgroundMusicSource.volume = backgroundVolume;
        SaveSoundSettings();
    }

    // Set the volume for the sound effects (called by the sound effects slider)
    public void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
        soundEffectSource.volume = effectsVolume;
        walkingSoundSource.volume = effectsVolume;
        SaveSoundSettings();
    }

    // Play a specific sound effect
    public void PlaySoundEffect(AudioClip sound)
    {
        if (sound != null)
        {
            soundEffectSource.clip = sound;
            soundEffectSource.volume = effectsVolume;
            soundEffectSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound effect clip is null.");
        }
    }

    public void PlayWalkSoundEffect(AudioClip sound)
    {
        if (sound != null)
        {
            walkingSoundSource.clip = sound;
            walkingSoundSource.volume = effectsVolume;
            walkingSoundSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound effect clip is null.");
        }
    }
    
    public void GameSoundEffect(AudioClip sound)
    {
        if (sound != null)
        {
            GameEffectSource.clip = sound;
            GameEffectSource.volume = effectsVolume;
            GameEffectSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound effect clip is null.");
        }
    }

    // Play a button click sound
    public void PlayButtonClick()
    {
        PlaySoundEffect(buttonSound);
    }

    // Play walking sound
    public void PlayWalkingSound()
    {
        PlayWalkSoundEffect(walkingSound);
    }
    public void PlayRunSound()
    {
        if (RunSound != null)
        {
            GameEffectSource.volume = effectsVolume; 
            GameEffectSource.PlayOneShot(RunSound);
        }
    }
    // Play attack sound
    public void PlayAttackSound()
    {
        GameSoundEffect(attackSound);
    }
    public void PlayTeleportSound()
    {
        PlaySoundEffect(Teleport);
    }
    
    public void PlayBuyNSell()
    {
        PlaySoundEffect(BuynSell);
    } 
    
    public void PlayNotEnough()
    {
        PlaySoundEffect(NotEnough);
    }
    
    public void PlayUseParts()
    {
        PlaySoundEffect(UseParts);
    }
    public void PlayCollectSound()
    {
        PlaySoundEffect(collectSound);
    }

    public void PlayDoneSound()
    {
        PlaySoundEffect(Done);
    }
    
    public void PlayCountSound()
    {
        PlaySoundEffect(Count);
    }
    public void PlayDialogueSound()
    {
        PlaySoundEffect(Dialogue);
    }

    public void InExploreSound()
    {
        soundEffectSource.Play();
        PlaySoundEffect(InExploreButtonSound);
    }

    // Change the background music for different worlds
    public void PlayMainMenuBackground()
    {
        PlayBackgroundMusic(mainMenuBackground);
    }

    public void PlayHomeWorldBackground()
    {
        PlayBackgroundMusic(homeWorldBackground);
    } 
    
    public void PlayCPUWorldBackground()
    {
        PlayBackgroundMusic(CPUWorldBackground);
    } 
    
    public void PlaYRAMWorldBackground()
    {
        PlayBackgroundMusic(RAMWorldBackground);
    } 
    
    public void PlaYCPUFANWorldBackground()
    {
        PlayBackgroundMusic(CPUFANWorldBackground);
    }
    public void PlaYGPUWorldBackground()
    {
        PlayBackgroundMusic(GPUWorldBackground);
    }

    public void PlaYSTORAGEWorldBackground()
    {
        PlayBackgroundMusic(STORAGEWorldBackground);
    }

    public void PlaYPSUWorldBackground()
    {
        PlayBackgroundMusic(PSUWorldBackground);
    }

    public void PlaYMBWorldBackground()
    {
        PlayBackgroundMusic(MBWorldBackground);
    }

    public void PlaYCASEWorldBackground()
    {
        PlayBackgroundMusic(CASEWorldBackground);
    }

    public void PlaYOnHordeBackground()
    {
        PlayBackgroundMusic(OnhordeBackground);
    }

    //public void PlayOtherWorldBackground()
    //{
    //    PlayBackgroundMusic(otherWorldBackground);
    //}

    // Optionally save and load sound settings
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("BackgroundVolume", backgroundVolume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
       
        PlayerPrefs.Save();
    }

    public void LoadSoundSettings()
    {
        backgroundVolume = PlayerPrefs.GetFloat("BackgroundVolume", 1f);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        backgroundMusicSource.volume = backgroundVolume;
        soundEffectSource.volume = effectsVolume;
        BackGround.value = backgroundVolume;
        Effects.value = effectsVolume;
    }


    public void ChangeMusic(AudioClip newClip)
    {
        StartCoroutine(FadeToNewMusic(newClip));
        CurrentBackground = newClip;
    }

    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        // Fade out current music
        yield return StartCoroutine(FadeOut(backgroundMusicSource));

        // Change music clip
        backgroundMusicSource.clip = newClip;
        backgroundMusicSource.Play();

        // Fade in new music
        yield return StartCoroutine(FadeIn(backgroundMusicSource));
    }
    public float fadeDuration = 5.0f; // Duration of the fade effect in seconds
    private IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop(); // Optionally stop the audio source
    }

    private IEnumerator FadeIn(AudioSource audioSource)
    {
        float startVolume = 0;
        audioSource.volume = startVolume;
        audioSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, backgroundVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = backgroundVolume;
    }

    public void OnApplicationQuit()
    {
        SaveSoundSettings();
    }
}
