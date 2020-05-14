using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private float restartDelayOnDeath = 0.5f; // Задержка рестарта уровня при смерти игрока

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

    /// <summary>
    /// Перезапустить текущий уровень
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Перезапустить текущий уровень с задержкой
    /// </summary>
    /// <param name="delay">Задержка в секундах</param>
    public IEnumerator RestartLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(RestartLevel(restartDelayOnDeath));
    }
}
