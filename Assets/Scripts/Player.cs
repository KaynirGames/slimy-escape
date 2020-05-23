using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject groundCheckPoint; // точка для проверки нахождения на земле
    [SerializeField] private LayerMask groundLayerMask; // какой слой является землей
    [SerializeField] private ParticleSystem landingEffect; // частицы при приземлении
    [SerializeField] private ParticleSystem deathEffect; // частицы после смерти

    private Rigidbody2D rb;
    private Collider2D playerCol;
    private Animator animator;

    private DragLauncher dragLauncher;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        dragLauncher = GetComponent<DragLauncher>();
    }

    private void Update()
    {
        // Предоставить управление персонажем, если он находится на земле, и не было произведено нажатие по UI элементу.
        if (IsGrounded() && !EventSystem.current.IsPointerOverGameObject())
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

                AudioMaster.Instance.PlaySoundEffect("Jump");
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
        Collider2D collider = Physics2D.OverlapBox(groundCheckPoint.transform.position, playerCol.bounds.extents * 1.25f, 0, groundLayerMask);

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
        AudioMaster.Instance.PlaySoundEffect("Landing");
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioMaster.Instance.PlaySoundEffect("PlayerDeath");

        GameMaster.Instance.PlayerIsDead();

        gameObject.SetActive(false);
    }
}
