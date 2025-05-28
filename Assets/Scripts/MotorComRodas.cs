// using UnityEngine;

// public class MotorComRodas : MonoBehaviour
// {
//     public float torque = 150f;
//     public float forcaPulo = 8f;
//     public LayerMask camadaChao; // Defina no Inspector
//     public float raioVerificacao = 0.1f;

//     private Rigidbody2D[] rodas;

//     void Start()
//     {
//         GameObject[] rodasObj = GameObject.FindGameObjectsWithTag("rodaTag");
//         rodas = new Rigidbody2D[rodasObj.Length];

//         for (int i = 0; i < rodasObj.Length; i++)
//         {
//             rodas[i] = rodasObj[i].GetComponent<Rigidbody2D>();
//         }
//     }

//     void Update()
//     {
//         float direcao = Input.GetAxisRaw("Horizontal");

//         // Gira as rodas
//         foreach (Rigidbody2D roda in rodas)
//         {
//             if (roda != null)
//                 roda.AddTorque(-direcao * torque * Time.deltaTime, ForceMode2D.Force);
//         }

//         // Pulo: só se pelo menos uma roda estiver tocando o chão
//         if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && EstaNoChao())
//         {
//             foreach (Rigidbody2D roda in rodas)
//             {
//                 if (roda != null)
//                     roda.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
//             }
//         }
//     }

//     bool EstaNoChao()
//     {
//         foreach (Rigidbody2D roda in rodas)
//         {
//             if (roda != null)
//             {
//                 Collider2D col = Physics2D.OverlapCircle(roda.position, raioVerificacao, camadaChao);
//                 if (col != null)
//                     return true;
//             }
//         }
//         return false;
//     }
// }
