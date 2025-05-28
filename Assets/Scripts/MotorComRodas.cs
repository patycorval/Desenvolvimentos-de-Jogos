using UnityEngine;

public class MotorComRodas : MonoBehaviour
{
    public float torque = 150f;
    public float forcaPulo = 120f; // você pode aumentar para 30f, 40f...

    public LayerMask camadaChao; // Defina no Inspector
    public float raioVerificacao = 0.1f;

    private Rigidbody2D[] rodas;
    private Rigidbody2D motorRb;

    public void Start()
    {
        GameObject[] rodasObj = GameObject.FindGameObjectsWithTag("rodaTag");
        rodas = new Rigidbody2D[rodasObj.Length];

        for (int i = 0; i < rodasObj.Length; i++)
        {
            rodas[i] = rodasObj[i].GetComponent<Rigidbody2D>();
        }

        GameObject motorObj = GameObject.FindGameObjectWithTag("motorTag");
        if (motorObj != null)
        {
            motorRb = motorObj.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        float direcao = Input.GetAxisRaw("Horizontal");

        foreach (Rigidbody2D roda in rodas)
        {
            if (roda != null)
                roda.AddTorque(-direcao * torque * Time.deltaTime, ForceMode2D.Force);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && EstaNoChao())
        {
            if (motorRb != null)
            {
                motorRb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
                Debug.Log("⬆️ PULO aplicado no motor para levantar toda a máquina.");
            }
        }
    }

    bool EstaNoChao()
    {
        foreach (Rigidbody2D roda in rodas)
        {
            if (roda != null)
            {
                Collider2D col = Physics2D.OverlapCircle(roda.position, raioVerificacao, camadaChao);
                if (col != null)
                    return true;
            }
        }
        return false;
    }
}
