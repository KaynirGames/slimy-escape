using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Закрываем все уровни выше достигнутого
            if (i + 2 > GameMaster.Instance.GetReachedLevel())
                levelButtons[i].interactable = false;
        }
    }

    public void SelectLevel(string sceneName)
    {
        sceneFader.FadeToScene(sceneName);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
