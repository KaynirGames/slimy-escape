using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Start()
    {
        gameMaster = GameMaster.Instance;
    }

    public void PlayGame()
    {
        gameMaster.LoadFirstLevel();
    }

    public void ExitGame()
    {
        gameMaster.ExitGame();
    }

    public void SelectLevel()
    {
        gameMaster.OpenLevelSelection();
    }
}
