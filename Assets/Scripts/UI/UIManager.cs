using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseButton.GetComponent<Button>().interactable = false;
        GameMaster.Instance.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
        GameMaster.Instance.ResumeGame();
    }

    public void MainMenu()
    {
        GameMaster.Instance.OpenMainMenu();
    }
}
