using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("volume", volume);
    }    //hi github
}
