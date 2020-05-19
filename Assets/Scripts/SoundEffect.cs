using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio Master/Sound Effect")]
public class SoundEffect : ScriptableObject
{
    /// <summary>
    /// Название звукового эффекта.
    /// </summary>
    public string soundName;
    /// <summary>
    /// Аудиодорожка звукового эффекта.
    /// </summary>
    public AudioClip clip;
    /// <summary>
    /// Громкость воспроизведения.
    /// </summary>
    [Range(0f, 1f)] public float volume;
    /// <summary>
    /// Высота звука.
    /// </summary>
    [Range(.1f, 3f)] public float pitch;
    /// <summary>
    /// Воспроизведение звукового эффекта в цикле.
    /// </summary>
    public bool loop;
    /// <summary>
    /// Источник звука
    /// </summary>
    [HideInInspector] public AudioSource source;

    /// <summary>
    /// Инициализация источника звука.
    /// </summary>
    private void InitializeAudioSource()
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
    }
    /// <summary>
    /// Воспроизведение звукового эффекта.
    /// </summary>
    public void Play()
    {
        InitializeAudioSource();
        source.Play();
    }
}
