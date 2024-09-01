using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button muteButton;
    public Button cancelButton;
    public Button confirmButton;
    public AudioMixer audioMixer;

    private float initialMusic;
    private float initialSfx;
    private float savedMusic;
    private float savedSfx;

    void Start()
    {
        initialMusic = PlayerPrefs.GetFloat("Music", 0.75f);
        initialSfx = PlayerPrefs.GetFloat("Sfx", 0.75f);

        if (initialMusic == 0f)
        {
            audioMixer.SetFloat("Music", -80f);
        }
        else
        {
            audioMixer.SetFloat("Music", Mathf.Log10(initialMusic) * 20);
        }

        if (initialSfx == 0f)
        {
            audioMixer.SetFloat("Sfx", -80f);
        }
        else
        {
            audioMixer.SetFloat("Sfx", Mathf.Log10(initialSfx) * 20);
        }


        musicSlider.value = initialMusic;
        sfxSlider.value = initialSfx;

        musicSlider.onValueChanged.AddListener(SetMusic);
        sfxSlider.onValueChanged.AddListener(SetSfx);
        muteButton.onClick.AddListener(Mute);
        cancelButton.onClick.AddListener(Cancel);
        confirmButton.onClick.AddListener(Confirm);
    }

    public void SetMusic(float volume)
    {
        if (volume == 0)
        {
            audioMixer.SetFloat("Music", -80f);
        }
        else
        {
            audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        }
    }

    public void SetSfx(float volume)
    {
        if (volume == 0)
        {
            audioMixer.SetFloat("Sfx", -80f);
        }
        else
        {
            audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
        }
    }

    public void Mute()
    {
        savedMusic = musicSlider.value;
        savedSfx = sfxSlider.value;

        audioMixer.SetFloat("Music", -80f);
        audioMixer.SetFloat("Sfx", -80f);

        musicSlider.value = 0f;
        sfxSlider.value = 0f;
    }

    public void Cancel()
    {
        musicSlider.value = initialMusic;
        sfxSlider.value = initialSfx;
    }

    public void Confirm()
    {
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("Sfx", sfxSlider.value);
        PlayerPrefs.Save();

        initialMusic = musicSlider.value;
        initialSfx = sfxSlider.value;
    }
}
