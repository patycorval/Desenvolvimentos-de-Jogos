using UnityEngine;
using System.Collections.Generic;

public class ControladorMaquina : MonoBehaviour
{
    public float velocidade = 300f;
    public float alturaPulo = 100000000000000000f; // Agora em metros por segundo, não força
    public LayerMask camadaChao;

    private Rigidbody2D motorRb;
    private List<Rigidbody2D> rodas = new List<Rigidbody2D>();
    private bool controleAtivo = false;

    public void IniciarControle()
    {
        controleAtivo = true;
        ProcurarComponentes();
        Debug.Log("✅ IniciarControle() chamado. Controle ativado.");
    }

    void Update()
    {
        if (!controleAtivo || motorRb == null || rodas.Count == 0)
        {
            return;
        }

        float direcao = Input.GetAxisRaw("Horizontal");
        Debug.Log($"🎮 Direção: {direcao}");

        foreach (Rigidbody2D roda in rodas)
        {
            if (roda != null)
            {
                roda.AddTorque(-direcao * velocidade);
                Debug.Log($"🌀 Torque aplicado na roda: {roda.name}");
            }
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && EstaNoChao())
        {
            if (motorRb != null)
            {
                // Aplica pulo direto na velocidade vertical
                motorRb.velocity = new Vector2(motorRb.velocity.x, alturaPulo);
                Debug.Log("⬆️ PULO executado via velocity.");
            }
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
            {
                rodas.Add(rb);
            }
        }

        Debug.Log($"🔍 Rodas detectadas: {rodas.Count}");
        if (rodas.Count == 0)
        {
            Debug.LogWarning("⚠️ Nenhuma roda válida encontrada!");
        }
    }

    private bool EstaNoChao()
    {
        foreach (Rigidbody2D roda in rodas)
        {
            RaycastHit2D hit = Physics2D.Raycast(roda.position, Vector2.down, 0.5f, camadaChao);
            Debug.DrawRay(roda.position, Vector2.down * 0.5f, Color.red, 0.1f);

            if (hit.collider != null)
            {
                Debug.Log($"✅ Roda {roda.name} tocando o chão.");
                return true;
            }
        }

        return false;
    }
}
