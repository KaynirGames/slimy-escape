using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public List<SoundEffect> soundEffects; // Список звуковых эффектов в игре.

    [HideInInspector] public AudioSource previewSource; // Источник звука для предварительного просмотра звукового эффекта.

    #region Instance
    public static AudioMaster Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var sound in soundEffects)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
        }

        PlaySoundEffect("MainTheme");
    }
    #endregion

    /// <summary>
    /// Воспроизвести звуковой эффект из мастера аудио.
    /// </summary>
    /// <param name="soundName">Название звукового эффекта.</param>
    public void PlaySoundEffect(string soundName)
    {
        SoundEffect soundEffect = soundEffects.Find(sound => sound.soundName == soundName);

        if (soundEffect == null)
        {
            Debug.LogWarning($"Sound effect: {soundName} is not found.");
            return;
        }

        soundEffect.Play();
    }

    /// <summary>
    /// Предварительный просмотр звуковой дорожки из инспектора
    /// </summary>
    /// <param name="soundName"></param>
    public void PreviewSound(string soundName)
    {
        SoundEffect soundEffect = soundEffects.Find(sound => sound.soundName == soundName);

        if (previewSource == null)
            previewSource = gameObject.AddComponent<AudioSource>();

        previewSource.hideFlags = HideFlags.HideAndDontSave;

        soundEffect.source = previewSource;
        soundEffect.Play();
    }
}
