using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragLauncher : MonoBehaviour
{
    /// <summary>
    /// Максимальный радиус натяжения
    /// </summary>
    [SerializeField] private float maxDragRadius = 2.5f;
    /// <summary>
    /// Максимальная сила запуска
    /// </summary>
    [SerializeField] private float maxLaunchForce = 15f;
    /// <summary>
    /// Начальная точка натяжения
    /// </summary>
    private Vector2 startDragPoint;
    /// <summary>
    /// Конечная точка натяжения
    /// </summary>
    private Vector2 endDragPoint;
    /// <summary>
    /// Траектория запуска
    /// </summary>
    private LineRenderer launchTrajectory;

    private void Start()
    {
        launchTrajectory = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Устанавливает начальную точку натяжения запускаемого объекта
    /// </summary>
    public void SetStartDragPoint(Vector3 launchObjectPos)
    {
        startDragPoint = launchObjectPos;

        launchTrajectory.enabled = true;
        launchTrajectory.SetPosition(0, startDragPoint);
    }
    /// <summary>
    /// Устанавливает конечную точку натяжения запускаемого объекта
    /// </summary>
    public void SetEndDragPoint()
    {       
        endDragPoint = CorrectEndPoint();
        launchTrajectory.SetPosition(1, endDragPoint);
    }
    /// <summary>
    /// Рассчитывает силу запуска объекта
    /// </summary>
    public Vector2 GetLaunchForce()
    {
        launchTrajectory.enabled = false;

        Vector2 direction = (startDragPoint - endDragPoint).normalized;
        float distance = Vector2.Distance(startDragPoint, endDragPoint);
        float launchForceFactor = Mathf.Clamp01(distance / maxDragRadius);

        return direction * launchForceFactor * maxLaunchForce;
    }
    /// <summary>
    /// Корректирует конечную точку при превышении максимального радиуса натяжения
    /// </summary>
    private Vector2 CorrectEndPoint()
    {
        Vector2 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(startDragPoint, currentPoint) > maxDragRadius)
        {
            return startDragPoint + (currentPoint - startDragPoint).normalized * maxDragRadius;
        }
        else
        {
            return currentPoint;
        }
    }
}
