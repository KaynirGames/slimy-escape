using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite collectedStarSprite;

    private void Start()
    {
        pauseMenu.SetActive(false);
        CollectableStar.onStarCollected += ShowCollectedStars;

        ShowCollectedStars();
    }

    private void ShowCollectedStars()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        int starsAmount = GameMaster.Instance.GetCollectedStarsAmount(currentLevelName);

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
}
