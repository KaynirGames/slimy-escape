using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string firstLevelSceneName = "Level1";
    [SerializeField] private string levelSelectionSceneName = "LevelSelection";

    public void PlayGame()
    {
        sceneFader.FadeToScene(firstLevelSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LevelSelection()
    {
        sceneFader.FadeToScene(levelSelectionSceneName);
    }
}
