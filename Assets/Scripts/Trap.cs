using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField, Tooltip("Задержка между срабатыванием ловушки")] private float trapActDelay = 0.5f;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Die();
        }
    }
    
    /// <summary>
    /// Событие в анимации, меняет текущий статус ловушки на противоположный
    /// </summary>
    /// <returns></returns>
    public IEnumerator ChangeTrapStatus()
    {
        yield return new WaitForSeconds(trapActDelay);
        animator.SetBool("isActive", !animator.GetBool("isActive"));
    }
}
