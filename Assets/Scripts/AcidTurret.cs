using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTurret : MonoBehaviour
{
    [SerializeField, Tooltip("Задержка между выстрелами")] private float fireDelay = 1f;
    [SerializeField, Tooltip("Сила выстрела")] private float launchForce = 5f;
    [SerializeField] private GameObject acidBulletPrefab;
    [SerializeField] private Transform firePoint;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Fire");
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(acidBulletPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * launchForce, ForceMode2D.Impulse);
        StartCoroutine(WaitForDelay());
    }

    public IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        animator.SetTrigger("Fire");
    }
}
