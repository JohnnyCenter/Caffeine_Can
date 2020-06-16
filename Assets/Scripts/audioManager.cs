using System;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{

    public AudioMixerGroup audioMixer;

    public sound[] sounds;


    // Start is called before the first frame update
    void Awake()
    {
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.outputAudioMixerGroup = audioMixer;
        }
    }

  public void Play (string name)
    {
        sound s = Array.Find(sounds, zound => zound.name == name);
        s.source.Play();
    }
}
