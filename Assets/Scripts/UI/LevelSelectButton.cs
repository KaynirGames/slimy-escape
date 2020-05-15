using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite collectedStarSprite;
    [SerializeField] private string levelName;

    private void Start()
    {
        for (int i = 0; i < GameMaster.Instance.GetCollectedStarsAmount(levelName); i++)
        {
            // Показать собранные на уровне звезды
            starImages[i].sprite = collectedStarSprite;
        }
    }
}
