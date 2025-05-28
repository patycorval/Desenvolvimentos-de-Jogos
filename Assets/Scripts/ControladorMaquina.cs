using UnityEngine;
using System.Collections.Generic;

public class ControladorMaquina : MonoBehaviour
{
    public float velocidade = 100f;
    public float forcaPulo = 80f;
    public LayerMask camadaChao;
    public float cooldownPulo = 0.5f;

    private Rigidbody2D motorRb;
    private List<Rigidbody2D> rodas = new List<Rigidbody2D>();
    private bool controleAtivo = false;
    private float tempoUltimoPulo = 0f;

    public void IniciarControle()
    {
        controleAtivo = true;
        ProcurarComponentes();
        Debug.Log("✅ IniciarControle() chamado. Controle ativado.");
    }

    public void Update()
    {
        if (!controleAtivo || motorRb == null || rodas.Count == 0)
            return;

        float direcao = Input.GetAxisRaw("Horizontal");

        foreach (Rigidbody2D roda in rodas)
        {
            if (roda != null)
                roda.AddTorque(-direcao * velocidade);
        }

        // Pulo
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && EstaNoChao() && Time.time - tempoUltimoPulo > cooldownPulo)
        {
            motorRb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            tempoUltimoPulo = Time.time;
            Debug.Log("⬆️ PULO executado.");
        }
    }

    private void ProcurarComponentes()
    {
        GameObject[] motores = GameObject.FindGameObjectsWithTag("motorTag");
        foreach (GameObject obj in motores)
        {
            PecaMotor peca = obj.GetComponent<PecaMotor>();
            if (peca != null && peca.ehMotor)
            {
                motorRb = obj.GetComponent<Rigidbody2D>();
                motorRb.gravityScale = 1f;
                motorRb.freezeRotation = true;

                CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
                if (cam != null)
                    cam.alvo = motorRb.transform;

                Debug.Log("✅ Motor conectado: " + obj.name);
                break;
            }
        }

        rodas.Clear();
        GameObject[] rodasObjs = GameObject.FindGameObjectsWithTag("rodaTag");
        foreach (GameObject obj in rodasObjs)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
                rodas.Add(rb);
        }
    }

    private bool EstaNoChao()
    {
        foreach (Rigidbody2D roda in rodas)
        {
            RaycastHit2D hit = Physics2D.Raycast(roda.position, Vector2.down, 0.6f, camadaChao);
            Debug.DrawRay(roda.position, Vector2.down * 0.6f, Color.red, 0.1f);
            if (hit.collider != null)
                return true;
        }
        return false;
    }
}
