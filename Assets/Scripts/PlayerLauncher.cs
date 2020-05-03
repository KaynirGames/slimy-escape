using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerLauncher : MonoBehaviour
{
    /// <summary>
    /// Модификатор для расчета силы толчка
    /// </summary>
    [SerializeField] private float forceModifier;

    [SerializeField] private float maxLaunchRadius = 4f;
    [SerializeField] private float maxForce = 10f;

    private Player player;
    private Camera cam;
    private LineRenderer lr;
    /// <summary>
    /// Начальная точка приложения силы
    /// </summary>
    private Vector2 startPoint;
    /// <summary>
    /// Конечная точка приложения силы
    /// </summary>
    private Vector2 endPoint;
    /// <summary>
    /// Расстояние между начальной и конечной точкой
    /// </summary>
    private float distance;
    /// <summary>
    /// Сила толчка
    /// </summary>
    private Vector2 launchForce;
    /// <summary>
    /// Вектор направления толчка
    /// </summary>
    private Vector2 direction;

    private void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLaunchStart();
        }
        if(Input.GetMouseButton(0))
        {
            OnLaunchCharge();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnLaunchRelease();
        }
    }
    /// <summary>
    /// При инициализации толчка
    /// </summary>
    private void OnLaunchStart()
    {
        startPoint = player.transform.position;
        lr.enabled = true;
        lr.SetPosition(0, startPoint);
    }
    /// <summary>
    /// При выборе силы толчка
    /// </summary>
    private void OnLaunchCharge()
    {
        Vector2 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(startPoint, currentPoint) > maxLaunchRadius)
        {
            endPoint = startPoint + (currentPoint - startPoint).normalized * maxLaunchRadius;
        }
        else endPoint = currentPoint;
        lr.SetPosition(1, endPoint);
        direction = (startPoint - endPoint).normalized;
        distance = Vector2.Distance(startPoint, endPoint);
    }
    /// <summary>
    /// При выполнении толчка
    /// </summary>
    private void OnLaunchRelease()
    {
        lr.enabled = false;
        launchForce = direction * Mathf.Clamp01(distance / maxLaunchRadius) * maxForce;
        player.Launch(launchForce);
    }
}
