﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public void SelectLevel(string levelName)
    {
        GameMaster.Instance.LoadLevel(levelName);
    }
}
