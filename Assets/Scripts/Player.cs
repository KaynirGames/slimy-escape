using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject groundCheckPoint; // точка для проверки нахождения на земле
    [SerializeField] private LayerMask groundLayerMask; // какой слой является землей
    [SerializeField] private ParticleSystem landingEffect; // частицы при приземлении
    [SerializeField] private ParticleSystem deathEffect; // частицы после смерти

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;

    private DragLauncher dragLauncher;

    public delegate void OnPlayerDeath();
    public static OnPlayerDeath onPlayerDeath;

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
        // Для теста
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
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
    /// Событие под конец анимации приземления
    /// </summary>
    public void OnLandingEnd()
    {
        animator.SetBool("wasLaunched", false);
    }

    /// <summary>
    /// Событие в начале анимации приземления
    /// </summary>
    public void OnLandingStart()
    {
        Instantiate(landingEffect, transform.position, Quaternion.identity);
    }

    public void Die()
    {
        onPlayerDeath?.Invoke();

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
