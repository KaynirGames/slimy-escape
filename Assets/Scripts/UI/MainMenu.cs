using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameMaster.Instance.LoadLevel("Level0");
    }

    public void ExitGame()
    {
        GameMaster.Instance.ExitGame();
    }
}
