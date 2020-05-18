using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float levelTimeLimit = 30f;
    [SerializeField] private Gradient timerColor;

    [SerializeField] private GameObject uiTimerText;
    [SerializeField] private Player player;

    private float timer;
    private Text timerText;
    private bool isTimerEnded;

    private void Start()
    {
        timer = levelTimeLimit;
        timerText = uiTimerText.GetComponent<Text>();
        isTimerEnded = false;
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
