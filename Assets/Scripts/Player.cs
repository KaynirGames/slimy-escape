using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject groundCheckPoint; // точка для проверки нахождения на земле
    [SerializeField] private LayerMask groundLayerMask; // какой слой является землей

    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Запускает игрока с заданной силой
    /// </summary>
    public void Launch(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void Die()
    {
        //death particle effect
        Destroy(gameObject);
        //убрать рестарт в класс-менеджер сцен
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Проверяет, находится ли персонаж на земле
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapBox(groundCheckPoint.transform.position, col.bounds.extents, 0, groundLayerMask);
        return collider != null;
    }
}
