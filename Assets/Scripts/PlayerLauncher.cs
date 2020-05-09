using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerLauncher : MonoBehaviour
{
    /// <summary>
    /// Радиус круга для ограничения силы запуска игрока
    /// </summary>
    [SerializeField] private float launchCircleRadius = 2.5f;
    /// <summary>
    /// Максимальная сила для запуска игрока
    /// </summary>
    [SerializeField] private float maxForce = 15f;

    private Player player;
    private Camera cam;

    /// <summary>
    /// Начальная точка приложения силы
    /// </summary>
    private Vector2 startPoint;
    /// <summary>
    /// Конечная точка приложения силы
    /// </summary>
    private Vector2 endPoint;

    /// <summary>
    /// Траектория приложения силы
    /// </summary>
    private LineRenderer forceTrajectory;

    private void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
        forceTrajectory = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (player.IsGrounded())
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnLaunchStart();
            }
            if (Input.GetMouseButton(0))
            {
                OnLaunchCharge();
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnLaunchRelease();
            }
        }
    }
    /// <summary>
    /// Начальная стадия запуска игрока
    /// </summary>
    private void OnLaunchStart()
    {
        startPoint = player.transform.position;

        forceTrajectory.enabled = true;
        forceTrajectory.SetPosition(0, startPoint);
    }
    /// <summary>
    /// Стадия выбора направления и силы запуска игрока
    /// </summary>
    private void OnLaunchCharge()
    {       
        endPoint = CorrectEndPoint(launchCircleRadius);
        forceTrajectory.SetPosition(1, endPoint);
    }
    /// <summary>
    /// Стадия запуска игрока
    /// </summary>
    private void OnLaunchRelease()
    {
        forceTrajectory.enabled = false;

        Vector2 direction = (startPoint - endPoint).normalized;
        float distance = Vector2.Distance(startPoint, endPoint);
        Vector2 launchForce = direction * Mathf.Clamp01(distance / launchCircleRadius) * maxForce;

        player.Launch(launchForce);
    }
    /// <summary>
    /// Корректирует положение конечной точки при выходе за границы радиуса запуска
    /// </summary>
    private Vector2 CorrectEndPoint(float launchRadius)
    {
        Vector2 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(startPoint, currentPoint) > launchRadius)
        {
            return startPoint + (currentPoint - startPoint).normalized * launchRadius;
        }
        else
        {
            return currentPoint;
        }
    }
}
