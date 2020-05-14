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

    private void Awake()
    {
        pauseMenu.SetActive(false);
        CollectableStar.onStarCollected += AddCollectedStars;

        string levelName = SceneManager.GetActiveScene().name;
        int collectedStarsAmount = PlayerPrefs.GetInt($"{levelName}StarsCollected", 0);
        AddCollectedStars(collectedStarsAmount);
    }

    private void AddCollectedStars(int collectedStarsAmount)
    {
        for (int i = 0; i < collectedStarsAmount; i++)
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
