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
    private Animator animator;

    private DragLauncher dragLauncher;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        dragLauncher = GetComponent<DragLauncher>();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragLauncher.SetStartDragPoint(transform.position);
                animator.SetBool("isLaunching", true);
            }
            if (Input.GetMouseButton(0))
            {
                dragLauncher.SetEndDragPoint();
            }
            if (Input.GetMouseButtonUp(0))
            {
                Launch(dragLauncher.GetLaunchForce());
                animator.SetBool("isLaunching", false);
                animator.SetBool("wasLaunched", true);
            }
        }
    }

    /// <summary>
    /// Запускает игрока с заданной силой
    /// </summary>
    private void Launch(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Проверяет, находится ли персонаж на земле
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapBox(groundCheckPoint.transform.position, col.bounds.extents, 0, groundLayerMask);
        if (collider != null)
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }
        return collider != null;
    }

    /// <summary>
    /// Событие в анимации приземления
    /// </summary>
    public void OnLanding()
    {
        animator.SetBool("wasLaunched", false);
    }

    public void Die()
    {
        //death particle effect
        Destroy(gameObject);
        //убрать рестарт в класс-менеджер сцен
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
