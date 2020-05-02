using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField] private Player player;

    private Camera cam;

    /// <summary>
    /// Начальная позиция
    /// </summary>
    private Vector2 startPos;
    private Vector2 endPos;
    /// <summary>
    /// Сила толчка
    /// </summary>
    private Vector2 launchForce;
    /// <summary>
    /// Направление толчка
    /// </summary>
    private Vector2 direction;
    /// <summary>
    /// Расстояние между начальной и конечной точкой
    /// </summary>
    private float distance;


    private void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.GetMouseButton(0))
        {
            endPos = cam.ScreenToWorldPoint(Input.mousePosition);
            direction = (startPos - endPos).normalized;
            distance = Vector2.Distance(startPos, endPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            launchForce = direction * distance * 4f;
            Debug.Log(launchForce);
            player.Launch(launchForce);
        }
    }
}
