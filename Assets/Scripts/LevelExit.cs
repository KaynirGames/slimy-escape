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
            GameMaster.instance.RestartLevel();
            // старт следующего уровня, либо выход в меню выбора уровней, если это последний
        }
    }
}
