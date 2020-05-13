using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "MainMenu";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameMaster.Instance.LoadLevel(nextSceneName);
        }
    }
}
