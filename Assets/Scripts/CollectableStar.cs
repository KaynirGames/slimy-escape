using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableStar : MonoBehaviour
{
    public delegate void OnStarCollected();
    public static OnStarCollected onStarCollected;

    [SerializeField] private int starIndex;
    
    private GameMaster gameMaster;
    private string currentLevelName;

    private void Start()
    {
        gameMaster = GameMaster.Instance;
        currentLevelName = SceneManager.GetActiveScene().name;

        if (gameMaster.IsStarCollected(currentLevelName, starIndex))
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gameMaster.AddStarToCollection(currentLevelName);
            gameMaster.MarkStarAsCollected(currentLevelName, starIndex);

            onStarCollected?.Invoke();

            gameObject.SetActive(false);
        }
    }
}
