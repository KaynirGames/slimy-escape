using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    #region Instance
    public static PlayerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Player.onPlayerDeath += OnPlayerDeath;
    }
    #endregion

    private void OnPlayerDeath()
    {
        StartCoroutine(GameMaster.Instance.RestartLevel(1.5f));
    }
}
