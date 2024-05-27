using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager instance;

    [Header ("AudioSources")]
    public AudioSource UI_Source;
    public AudioSource BGM_Source;
    public AudioSource Voice_Source;
    public AudioSource Effect_Source;

    [Header ("AudioClips")]
    public AudioClip[] Clicke_Effects;
    public AudioClip[] BGMS;
    public AudioClip[] CharVoices;
    public AudioClip[] Effects;
    public AudioClip[] JumpingSounds;
    public AudioClip[] SteppingSounds;
    
    public void PlayEffects(string name)
    {
        foreach (AudioClip audioFile in Effects)
        {
            if (audioFile.name == name)
            {
                Effect_Source.clip = audioFile;
                Effect_Source.Play();
            }
        }
    }
    public void onWalking()
    {
        UI_Source.clip = SteppingSounds[Random.Range(0, SteppingSounds.Length)];
        UI_Source.Play();
    }
    public void onJump()
    {
        UI_Source.clip = JumpingSounds[Random.Range(0, JumpingSounds.Length)];
        UI_Source.Play();
    }
    public void onClick()
    {
        UI_Source.clip = Clicke_Effects[Random.Range(0, Clicke_Effects.Length)];
        UI_Source.Play();
    }
    
    public void stopPlayingBGM()
    {
        BGM_Source.Stop();
    }

    public void startPlayingBGM()
    {
        BGM_Source.clip = BGMS[Random.Range(0, BGMS.Length)];
        BGM_Source.Play();
    }

    public void startCharVoice(int index)
    {
        if (Voice_Source.isPlaying)
        {
            Voice_Source.Stop();
        }

        foreach(AudioClip audioFile in CharVoices)
        {
            if (audioFile.name == index.ToString())
            {
                Voice_Source.clip = audioFile;
                Voice_Source.Play();
            }
        }
    }

    private void Update()
    {
        if (!BGM_Source.isPlaying)
        {
            startPlayingBGM();
        }
    }
    void Awake()
    {
        // SingleTone Pattern Implemention
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        startPlayingBGM();

        CharVoices = Resources.LoadAll<AudioClip>("Audio/Voices");
    }
}
