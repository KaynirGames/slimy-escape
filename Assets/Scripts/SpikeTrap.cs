using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField, Tooltip("Задержка активации/деактивации шипа")] float trapActDelay = 0.25f;

    private Animator animator;
    private Collider2D col;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Die();
        }
    }
    /// <summary>
    /// Событие в анимации, активирует ловушку с задержкой
    /// </summary>
    /// <returns></returns>
    public IEnumerator OpenTrap()
    {
        yield return new WaitForSeconds(trapActDelay);
        animator.SetBool("isOpen", true);
    }
    /// <summary>
    /// Событие в анимации, деактивирует ловушку с задержкой
    /// </summary>
    /// <returns></returns>
    public IEnumerator CloseTrap()
    {
        yield return new WaitForSeconds(trapActDelay);
        animator.SetBool("isOpen", false);
    }
    /// <summary>
    /// Событие в анимации, включает/выключает коллайдер ловушки
    /// </summary>
    public void ChangeColliderState()
    {
        col.enabled = !col.enabled;
    }
}
