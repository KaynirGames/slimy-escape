using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [Header("Названия основных сцен")]
    [Tooltip("Название сцены для главного меню")] public string mainMenuSceneName = "MainMenu";
    [Tooltip("Название сцены для выбора уровня")] public string levelSelectionSceneName = "LevelSelection";
    [Tooltip("Название сцены для первого уровня")] public string firstLevelSceneName = "Level1";

    #region Instance
    public static GameMaster Instance { get; private set; }

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
    }
    #endregion

    /// <summary>
    /// Загрузить указанный игровой уровень
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    /// <summary>
    /// Загрузить первый игровой уровень
    /// </summary>
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }

    /// <summary>
    /// Перезапустить текущий игровой уровень
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Перезапустить текущий игровой уровень с задержкой
    /// </summary>
    /// <param name="delay">Задержка в секундах</param>
    public IEnumerator RestartLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Поставить паузу в игре
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    /// <summary>
    /// Снять паузу с игры
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    /// <summary>
    /// Открыть сцену с главным меню
    /// </summary>
    public void OpenMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(mainMenuSceneName);
    }
    /// <summary>
    /// Открыть сцену с выбором игрового уровня
    /// </summary>
    public void OpenLevelSelection()
    {
        SceneManager.LoadScene(levelSelectionSceneName);
    }
    /// <summary>
    /// Выйти из игры
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
