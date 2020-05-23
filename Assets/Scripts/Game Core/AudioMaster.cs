using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] private int bgThemeChangeLevelIndex; // Индекс уровня в билде, где меняется фоновая музыка.

    public List<SoundEffect> soundEffects; // Список звуковых эффектов в игре.

    [HideInInspector] public AudioSource previewSource; // Источник звука для предварительного просмотра звукового эффекта.

    private bool isDefaultBGTheme = true; // Очередь воспроизводить стандартную фоновую музыку?

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
    }
    #endregion

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < bgThemeChangeLevelIndex && isDefaultBGTheme)
        {
            MuteSoundEffect("MainThemeAlt");
            PlaySoundEffect("MainTheme");
            isDefaultBGTheme = false;
        }

        if (SceneManager.GetActiveScene().buildIndex >= bgThemeChangeLevelIndex && !isDefaultBGTheme)
        {
            MuteSoundEffect("MainTheme");
            PlaySoundEffect("MainThemeAlt");
            isDefaultBGTheme = true;
        }
    }

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
    /// Остановить воспроизведение звукового эффекта.
    /// </summary>
    /// <param name="soundName">Название звукового эффекта.</param>
    public void MuteSoundEffect(string soundName)
    {
        SoundEffect soundEffect = soundEffects.Find(sound => sound.soundName == soundName);

        if (soundEffect == null)
        {
            Debug.LogWarning($"Sound effect: {soundName} is not found.");
            return;
        }

        soundEffect.Mute();
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
