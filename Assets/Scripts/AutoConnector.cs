using UnityEngine;

public class AutoConnector : MonoBehaviour
{
    private bool conectado = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (conectado || collision.rigidbody == null) return;

        Rigidbody2D thisRb = GetComponent<Rigidbody2D>();
        Rigidbody2D otherRb = collision.rigidbody;

        if (thisRb.bodyType == RigidbodyType2D.Kinematic && otherRb.bodyType == RigidbodyType2D.Kinematic)
        {
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = otherRb;

            joint.autoConfigureConnectedAnchor = true;
            joint.enableCollision = false;
            joint.breakForce = float.PositiveInfinity;
            joint.breakTorque = float.PositiveInfinity;

            conectado = true;
        }
    }
}
