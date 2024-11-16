using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings: MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _sliderMusic;

    private const float _defaultMusicVolume = -20;
    private const float _minMusicVolume = -30;
    private const float _offSoundVolume = -80;

    private readonly string MusicSave = "MusicVolume";
    private readonly string Music = "Music";

    private void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicSave, _defaultMusicVolume);
        _sliderMusic.value = musicVolume;
        ValueMusic(musicVolume);
    }

    public void ValueMusic(float volume)
    {
        float musicVolume = volume <= _minMusicVolume ? _offSoundVolume : volume;
        _mixer.audioMixer.SetFloat(Music, musicVolume);

        PlayerPrefs.SetFloat(MusicSave, musicVolume);
    }
}