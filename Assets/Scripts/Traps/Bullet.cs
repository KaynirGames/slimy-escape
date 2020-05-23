using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect; // Частицы после столкновения с объектом.

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<Player>().Die();
        }

        if(!col.collider.CompareTag("Turret"))
        {
            Explode();
        }
    }
    /// <summary>
    /// Уничтожение пули при столкновении с объектом.
    /// </summary>
    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioMaster.Instance.PlaySoundEffect("AcidBulletImpact");
        Destroy(gameObject);
    }
}
