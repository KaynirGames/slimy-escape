using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private Vector2 defaultScale; // начальный размер луча
    private float newScaleX; // новый размер луча (до конечной точки)

    private void Awake()
    {
        defaultScale = transform.localScale;
        newScaleX = Mathf.Abs(startPoint.localPosition.y - endPoint.localPosition.y);
    }

    public void ActivateBeam()
    {
        transform.localScale = new Vector2(newScaleX, defaultScale.y);
    }

    public void DeactivateBeam()
    {
        transform.localScale = defaultScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Die();
        }
    }
}
