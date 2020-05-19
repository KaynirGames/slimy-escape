﻿using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    /// <summary>
    /// Начальная длина лазерного луча
    /// </summary>
    private Vector2 defaultScale;
    /// <summary>
    /// Длина луча до новой конечной точки
    /// </summary>
    private float newScaleY;

    private void Awake()
    {
        defaultScale = transform.localScale;
        newScaleY = Mathf.Abs(startPoint.localPosition.y - endPoint.localPosition.y);
    }

    public void ActivateBeam()
    {
        transform.localScale = new Vector2(defaultScale.x, newScaleY);
    }

    public void DeactivateBeam()
    {
        transform.localScale = defaultScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Die();
        }
    }
}
