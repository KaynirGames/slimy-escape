using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [Header("Настройки эффекта затемнения экрана")]
    [SerializeField] private CanvasGroup imageAlpha;    // Объект, отвечающий за прозрачность экрана затемнения
    [SerializeField] private AnimationCurve alphaCurve; // Кривая изменения прозрачности по времени
    [SerializeField] private bool useFadeInOnLoad = true; // Использовать эффект затемнения при загрузке сцены?

    private void Start()
    {
        if (useFadeInOnLoad)
            StartCoroutine(FadeIn());
    }
    
    /// <summary>
    /// Выход из затемнения экрана
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            imageAlpha.alpha = alphaCurve.Evaluate(t);

            yield return 0; // Дождаться следующего фрейма, затем продолжить
        }
    }
    /// <summary>
    /// Затемнение экрана
    /// </summary>
    /// <param name="sceneName">Название сцены</param>
    /// <returns></returns>
    private IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            imageAlpha.alpha = alphaCurve.Evaluate(t);

            yield return 0; // Дождаться следующего фрейма, затем продолжить
        }

        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// Затемнение экрана
    /// </summary>
    /// <param name="sceneName">Индекс сцены в билде</param>
    /// <returns></returns>
    private IEnumerator FadeOut(int sceneIndex)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            imageAlpha.alpha = alphaCurve.Evaluate(t);

            yield return 0; // Дождаться следующего фрейма, затем продолжить
        }

        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Переход к сцене с затемнением экрана
    /// </summary>
    /// <param name="sceneName">Название сцены</param>
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    /// <summary>
    /// Переход к сцене с затемнением экрана
    /// </summary>
    /// <param name="sceneIndex">Индекс сцены в билде</param>
    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeOut(sceneIndex));
    }
}
