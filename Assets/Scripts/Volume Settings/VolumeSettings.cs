using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider voiceSlider;

    const string MIXER_MASTER = "Master Volume";
    const string MIXER_MUSIC = "Music Volume";
    const string MIXER_SFX = "SFX Volume";
    const string MIXER_VOICE = "Voice Volume";

    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        voiceSlider.onValueChanged.AddListener(SetVoiceVolume);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    void SetVoiceVolume(float value)
    {
        mixer.SetFloat(MIXER_VOICE, Mathf.Log10(value) * 20);
    }
}
