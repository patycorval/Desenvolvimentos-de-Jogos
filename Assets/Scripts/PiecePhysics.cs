using UnityEngine;

public class PecaFisica : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AtivarSimulacao()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void AtivarConstrucao()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
