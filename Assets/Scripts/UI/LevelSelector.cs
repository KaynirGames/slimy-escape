using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Button[] levelButtons;

    private int reachedLevel; // Достигнутый уровень, номер основывается на индексе сцены в билде

    private void Start()
    {
        reachedLevel = PlayerPrefs.GetInt("reachedLevel", 2);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Закрываем все уровни выше достигнутого
            if (i + 2 > reachedLevel)
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
