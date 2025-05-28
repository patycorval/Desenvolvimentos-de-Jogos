using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AutoConnector : MonoBehaviour
{
    public float raioConexao = 0.5f;
    public LayerMask camadaConectavel;

    public void ConectarAgora()
    {
        Collider2D[] proximos = Physics2D.OverlapCircleAll(transform.position, raioConexao, camadaConectavel);
        Debug.Log($"[{name}] Detectados {proximos.Length} objetos próximos");

        Rigidbody2D meuRb = GetComponent<Rigidbody2D>();

        foreach (var col in proximos)
        {
            if (col.gameObject == gameObject) continue;

            Rigidbody2D outroRb = col.attachedRigidbody;
            if (outroRb == null) continue;

            // Se for roda, conecta com hinge
            if (CompareTag("rodaTag"))
            {
                HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
                joint.connectedBody = outroRb;
                joint.autoConfigureConnectedAnchor = true;
                joint.useMotor = false;
                Debug.Log($"🔧 [{name}] Conectado via HingeJoint2D com {col.name}");
            }
            else
            {
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = outroRb;
                joint.enableCollision = false;
                joint.breakForce = 10000f;
                joint.breakTorque = 10000f;
                Debug.Log($"🔗 [{name}] Conectado via FixedJoint2D com {col.name}");
            }

            break; // conecta com apenas 1 objeto próximo
        }
    }
}
