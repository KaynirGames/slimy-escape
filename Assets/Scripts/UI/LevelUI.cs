using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite collectedStarSprite;

    private string currentLevelName;

    private void Start()
    {
        pauseMenu.SetActive(false);
        currentLevelName = SceneManager.GetActiveScene().name;

        GameMaster.Instance.OnStarTriggerEnter += ShowCollectedStars;

        ShowCollectedStars(currentLevelName, 0);
    }

    private void ShowCollectedStars(string levelName, int starIndex)
    {
        if (currentLevelName != levelName)
            return;

        int starsAmount = GameMaster.Instance.GetCollectedStars(levelName);

        for (int i = 0; i < starsAmount; i++)
        {
            starImages[i].sprite = collectedStarSprite;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
        pauseButton.GetComponent<Button>().interactable = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        GameMaster.Instance.OnStarTriggerEnter -= ShowCollectedStars;
    }
}
