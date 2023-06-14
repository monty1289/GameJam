// /////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audio Manager.
//
// This code is release under the MIT licence. It is provided as-is and without any warranty.
//
// Developed by Daniel Rodríguez (Seth Illgard) in April 2010 http://www.silentkraken.com
//
// /////////////////////////////////////////////////////////////////////////////////////////////////////////
 
using System;
using UnityEngine;
using UnityEngine.Audio;
 
public class AudioManager : MonoBehaviour
{
    #region Public Fields
 
    public static AudioManager Instance;
 
    public AudioMixerGroup masterGroup;
    public AudioMixer masterMixer;
    public AudioMixerGroup musicGroup;
 
    public AudioMixerGroup soundGroup;
 
    #endregion Public Fields
 
    #region Public Enums
 
    public enum AudioChannel { Master, Sound, Music }
 
    #endregion Public Enums
 
    #region Public Methods
 
    /// <summary>
    /// Plays a sound at the given point in space by creating an empty game object with an
    /// AudioSource in that place and destroys it after it finished playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="emitter"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource CreatePlaySource(AudioClip clip, Transform emitter, float volume, float pitch, bool music = false)
    {
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = emitter.position;
        go.transform.parent = emitter;
 
        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
 
        // Output sound through the sound group or music group
        if (music)
            source.outputAudioMixerGroup = musicGroup;
        else
            source.outputAudioMixerGroup = soundGroup;
 
        source.Play();
        return source;
    }
 
    public AudioSource Play(AudioClip clip, Transform emitter)
    {
        return Play(clip, emitter, 1f, 1f);
    }
 
    public AudioSource Play(AudioClip clip, Transform emitter, float volume)
    {
        return Play(clip, emitter, volume, 1f);
    }
 
    /// <summary>
    /// Plays a sound by creating an empty game object with an AudioSource and attaching it to
    /// the given transform (so it moves with the transform). Destroys it after it finished playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="emitter"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
    {
        //Create an empty game object
        AudioSource source = CreatePlaySource(clip, emitter, volume, pitch);
        Destroy(source.gameObject, clip.length);
        return source;
    }
 
    public AudioSource Play(AudioClip clip, Vector3 point)
    {
        return Play(clip, point, 1f, 1f);
    }
 
    public AudioSource Play(AudioClip clip, Vector3 point, float volume)
    {
        return Play(clip, point, volume, 1f);
    }
 
    /// <summary>
    /// Plays a sound at the given point in space by creating an empty game object with an
    /// AudioSource in that place and destroys it after it finished playing.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="point"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
    {
        AudioSource source = CreatePlaySource(clip, point, volume, pitch);
        Destroy(source.gameObject, clip.length);
        return source;
    }
 
    /// <summary>
    /// Plays the sound effect in a loop. Should destroy the audio source in your script when it
    /// is ready to end.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="point"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource PlayLoop(AudioClip clip, Transform emitter, float volume = 1f, float pitch = 1f, bool music = true)
    {
        AudioSource source = CreatePlaySource(clip, emitter, volume, pitch, true);
        source.loop = true;
        return source;
    }
 
    /// <summary>
    /// Plays the sound effect in a loop. Should destroy the audio source in your script when it
    /// is ready to end.
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="point"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public AudioSource PlayLoop(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, bool music = true)
    {
        AudioSource source = CreatePlaySource(clip, point, volume, pitch, true);
        source.loop = true;
        return source;
    }
 
    public void SetVolume(AudioChannel channel, float volume)
    {
        // Converts the 0 - 100 input into decibles | volume = 1 is -40 DB / Mute. Volume 100 is
        // 0 0 DB.
        float adjustedVolume = -40 + (volume * 8 / 20);
 
        // Effectively completed muted if volume if 0
        if (volume == 0)
        {
            adjustedVolume = -100;
        }
 
        switch (channel)
        {
            case AudioChannel.Master:
                masterMixer.SetFloat("MasterVolume", adjustedVolume);
                break;
 
            case AudioChannel.Sound:
                masterMixer.SetFloat("SoundVolume", adjustedVolume);
                break;
 
            case AudioChannel.Music:
                masterMixer.SetFloat("MusicVolume", adjustedVolume);
                break;
        }
    }
 
    #endregion Public Methods
 
    #region Private Methods
 
    private void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }
 
        //If instance already exists and it's not this:
        else if (Instance != this)
 
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }
 
    private AudioSource CreatePlaySource(AudioClip clip, Vector3 point, float volume, float pitch, bool music = false)
    {
        //Create an empty game object
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = point;
 
        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
 
        // Output sound through the sound group or music group
        if (music)
            source.outputAudioMixerGroup = musicGroup;
        else
            source.outputAudioMixerGroup = soundGroup;
 
        source.Play();
        return source;
    }
 
    /// <summary>
    /// Set up audio levels
    /// </summary>
    private void Start()
    {
        // Set the audio levels from player preferences
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 100);
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume", 100);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 100);
 
        // Update the audio mixer
        SetVolume(AudioChannel.Master, masterVolume);
        SetVolume(AudioChannel.Sound, soundVolume);
        SetVolume(AudioChannel.Music, musicVolume);
    }
 
    #endregion Private Methods
}
