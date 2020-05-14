using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableStar : MonoBehaviour
{
    public delegate void OnStarCollected(int collectedStarsAmount);
    public static OnStarCollected onStarCollected;

    [SerializeField] private int starIndex;

    private string levelName;
    private string collectedStarsAmountPrefKey;
    private string isStarCollectedPrefKey;


    private void Start()
    {
        levelName = SceneManager.GetActiveScene().name;

        collectedStarsAmountPrefKey = $"{levelName}StarsCollected";
        isStarCollectedPrefKey = $"{levelName}StarNumber{starIndex}IsCollected";

        //ClearPlayerPrefs();

        if (IsCollected())
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AddToCollection();
            MarkAsCollected();

            onStarCollected?.Invoke(PlayerPrefs.GetInt(collectedStarsAmountPrefKey));

            gameObject.SetActive(false);
        }
    }

    private void AddToCollection()
    {
        int starsCollected = PlayerPrefs.GetInt(collectedStarsAmountPrefKey, 0);

        if (starsCollected == 3)
            return;
        
        PlayerPrefs.SetInt(collectedStarsAmountPrefKey, starsCollected + 1);
        Debug.Log(PlayerPrefs.GetInt(collectedStarsAmountPrefKey, 0));
    }

    private void MarkAsCollected()
    {
        int isStarCollected = PlayerPrefs.GetInt(isStarCollectedPrefKey, 0);
        
        if (isStarCollected != 1)
        {
            PlayerPrefs.SetInt(isStarCollectedPrefKey, 1);
            Debug.Log(PlayerPrefs.GetInt(isStarCollectedPrefKey, 0));
        }
    }

    private bool IsCollected()
    {
        int isStarCollected = PlayerPrefs.GetInt(isStarCollectedPrefKey, 0);

        return isStarCollected == 1;
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(collectedStarsAmountPrefKey);
        PlayerPrefs.DeleteKey(isStarCollectedPrefKey);
    }
}
