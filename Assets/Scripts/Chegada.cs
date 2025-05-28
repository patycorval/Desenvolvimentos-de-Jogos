using UnityEngine;

public class Chegada : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject botaoResetar;

    private bool vitoriaAtivada = false;

    private void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (vitoriaAtivada) return; // evita múltiplas ativações

        // Verifica se a peça é parte da máquina (tem Rigidbody2D e foi instanciada)
        if (!other.name.Contains("Clone")) return;

        Debug.Log($"🎯 Vitória ativada por: {other.name}");
        vitoriaAtivada = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (botaoResetar != null)
            botaoResetar.SetActive(false);

        Time.timeScale = 0f;
    }
}
