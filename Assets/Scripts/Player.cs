using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //[SerializeField] private float minVelocity;
    //[SerializeField] private float maxVelocity;

    [HideInInspector] public Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Толкает игрока с заданной силой
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
}
