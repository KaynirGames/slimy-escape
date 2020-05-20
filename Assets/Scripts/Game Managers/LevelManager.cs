using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string currentLevelName;

    private void Start()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
        GameMaster.Instance.OnStarTriggerEnter += CollectStar;
    }

    private void CollectStar(string levelName, int starIndex)
    {
        if (currentLevelName == levelName)
        {
            GameMaster.Instance.AddStarToCollection(levelName);
            GameMaster.Instance.MarkStarAsCollected(levelName, starIndex);
        }
    }

    private void OnDestroy()
    {
        GameMaster.Instance.OnStarTriggerEnter -= CollectStar;
    }
}
