using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PecaFisica : MonoBehaviour
{
    private Rigidbody2D rb;
    private AutoConnector conector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        conector = GetComponent<AutoConnector>();
    }

    public void AtivarSimulacao()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;
        rb.gravityScale = 1f;
        rb.freezeRotation = false;

        if (conector != null)
            conector.enabled = true;
    }

    public void AtivarConstrucao()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.freezeRotation = true;
        rb.gravityScale = 0f;

        if (conector != null)
            conector.enabled = false;
    }
}
