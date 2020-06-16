using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsSlider : MonoBehaviour
{

    public AudioMixer mikser;

    public void SetVolume(float volume)
    {
        Debug.Log(volume);

        mikser.SetFloat("volume", volume);
    }

}
