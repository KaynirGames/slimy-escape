using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<Player>().Die();
        }
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
        // particle effect
    }
}
