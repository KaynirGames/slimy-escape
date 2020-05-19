using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private float restartDelayOnDeath = 0.5f; // Задержка рестарта уровня при смерти игрока
    [SerializeField] private int lastLevelBuildIndex; // Индекс последнего уровня

    public event Action<string, int> OnStarTriggerEnter;

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
        
        Player.onPlayerDeath += OnPlayerDeath;
    }
    #endregion

    public void StarTriggerEnter(string levelName, int starIndex)
    {
        OnStarTriggerEnter?.Invoke(levelName, starIndex);
    }

    /// <summary>
    /// Наращивает количество собранных звезд на указанном уровне.
    /// </summary>
    /// <param name="levelName"></param>
    public void AddStarToCollection(string levelName)
    {
        int currentAmount = GetCollectedStars(levelName);

        if (currentAmount == 3)
            return;

        string playerPrefKey = $"{levelName}_starsCollected";
        PlayerPrefs.SetInt(playerPrefKey, currentAmount + 1);
    }

    /// <summary>
    /// Возвращает количество собранных звезд на указанном уровне.
    /// </summary>
    /// <param name="levelName">Название уровня</param>
    /// <returns></returns>
    public int GetCollectedStars(string levelName)
    {
        string playerPrefKey = $"{levelName}_starsCollected";
        return PlayerPrefs.GetInt(playerPrefKey, 0);
    }
    /// <summary>
    /// Помечает звезду как собранную для указанного уровня.
    /// </summary>
    /// <param name="levelName">Название уровня</param>
    /// <param name="starIndex">Индекс звезды</param>
    public void MarkStarAsCollected(string levelName, int starIndex)
    {
        string playerPrefKey = $"{levelName}_star_{starIndex}_isCollected";
        PlayerPrefs.SetInt(playerPrefKey, 1);
    }

    /// <summary>
    /// Проверяет, была ли собрана указанная звезда.
    /// </summary>
    /// <param name="levelName">Название уровня</param>
    /// <param name="starIndex">Индекс звезды</param>
    /// <returns></returns>
    public bool IsStarCollected(string levelName, int starIndex)
    {
        string playerPrefKey = $"{levelName}_star_{starIndex}_isCollected";
        return PlayerPrefs.GetInt(playerPrefKey, 0) == 1;
    }

    /// <summary>
    /// Возвращает номер достигнутого уровня, нумерация начинается с 2 (индекс первого уровня в билде).
    /// </summary>
    /// <returns></returns>
    public int GetReachedLevel()
    {
        return PlayerPrefs.GetInt("reachedLevel", 2);
    }

    /// <summary>
    /// Устанавливает новый достигнутый уровень.
    /// </summary>
    public void SetReachedLevel()
    {
        int reachedLevel = GetReachedLevel();
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Ничего не делаем, если текущий уровень уже пройден, либо является последним.
        if (reachedLevel > currentLevel || currentLevel == lastLevelBuildIndex)
            return;

        // Прошли текущий уровень, а значит достигли следующего.
        PlayerPrefs.SetInt("reachedLevel", currentLevel + 1);
    }

    /// <summary>
    /// Перезапускает текущий уровень с задержкой.
    /// </summary>
    private IEnumerator DelayedRestart(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Выполняет действия при смерти игрока.
    /// </summary>
    private void OnPlayerDeath()
    {
        StartCoroutine(DelayedRestart(restartDelayOnDeath));
    }
}
