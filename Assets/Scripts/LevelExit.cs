using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private SceneFader sceneFader;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameMaster.Instance.SetReachedLevel();

            sceneFader.FadeToScene(nextLevelName);
        }
    }
}
