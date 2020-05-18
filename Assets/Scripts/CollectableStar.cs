using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableStar : MonoBehaviour
{
    [SerializeField] private int starIndex;

    private string currentLevelName;

    private void Start()
    {
        currentLevelName = SceneManager.GetActiveScene().name;

        if (GameMaster.Instance.IsStarCollected(currentLevelName, starIndex))
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameMaster.Instance.StarTriggerEnter(currentLevelName, starIndex);

            gameObject.SetActive(false);
        }
    }
}
