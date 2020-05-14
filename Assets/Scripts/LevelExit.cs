using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private int nextLevelBuildIndex;
    [SerializeField] private SceneFader sceneFader;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (nextLevelBuildIndex > PlayerPrefs.GetInt("reachedLevel", 3))
            {
                PlayerPrefs.SetInt("reachedLevel", nextLevelBuildIndex);
            }
            sceneFader.FadeToScene(nextLevelBuildIndex);
        }
    }
}
