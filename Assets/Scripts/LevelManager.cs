using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float levelTimeLimit = 30f;
    [SerializeField] private Gradient timerColor;

    [SerializeField] private GameObject uiTimerText;
    [SerializeField] private Player player;

    private float timer;
    private Text timerText;
    private bool isTimerEnded;
    private int collectedStarsAmount;

    private void Start()
    {
        timer = levelTimeLimit;
        timerText = uiTimerText.GetComponent<Text>();
        isTimerEnded = false;
        collectedStarsAmount = 0;

        CollectableStar.onStarCollected += OnStarCollected;
    }

    private void OnStarCollected()
    {
        collectedStarsAmount++;

        Debug.Log(collectedStarsAmount);

        string currentLevelName = SceneManager.GetActiveScene().name;
        int starsInCollection = GameMaster.Instance.GetCollectedStarsAmount(currentLevelName);

        if (collectedStarsAmount > starsInCollection && collectedStarsAmount <= 3)
        {
            GameMaster.Instance.AddStarsToCollection(currentLevelName, collectedStarsAmount);
        }
    }

    private void Update()
    {
        if (isTimerEnded)
            return;

        if (timer >= 0.0f)
        {
            timerText.color = timerColor.Evaluate(timer / levelTimeLimit);
            timerText.text = timer.ToString("F");
            timer -= Time.deltaTime;
        }
        else if (timer < 0.0f)
        {
            timerText.text = "0.00";
            player.Die();
            isTimerEnded = true;
        }
    }
}
