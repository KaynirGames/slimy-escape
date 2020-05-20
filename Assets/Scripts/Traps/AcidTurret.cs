using System.Collections;
using UnityEngine;

public class AcidTurret : MonoBehaviour
{
    [SerializeField, Tooltip("Задержка между выстрелами")] private float fireDelay = 1f;
    [SerializeField, Tooltip("Сила запуска снаряда")] private float launchForce = 5f;
    [SerializeField] private GameObject acidBulletPrefab;
    [SerializeField] private Transform firePoint;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Fire");
    }
    /// <summary>
    /// Выстрел снарядом, событие в анимации.
    /// </summary>
    public void Fire()
    {
        GameObject bullet = Instantiate(acidBulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * launchForce, ForceMode2D.Impulse);
        AudioMaster.Instance.PlaySoundEffect("AcidTurretFire");
        StartCoroutine(WaitForDelay());
    }
    /// <summary>
    /// Выждать время перед следующим выстрелом. 
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        animator.SetTrigger("Fire");
    }
}
