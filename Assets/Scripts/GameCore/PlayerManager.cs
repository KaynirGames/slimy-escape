using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Player.onPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(GameMaster.instance.RestartLevel(1.5f));
    }
}
