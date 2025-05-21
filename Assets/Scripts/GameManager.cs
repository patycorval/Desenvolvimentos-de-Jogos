using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject uiConstrucao;                 // Painel com inventário + área
    public TextMeshProUGUI textoBotao;              // Texto do botão TESTAR/EDITAR
    public Transform areaConstruida;                // Onde as peças são instanciadas
    public List<InventorySlot> slotsDeInventario;   // Todos os botões com peças

    private bool emConstrucao = true;

    public void AlternarModo()
    {
        if (emConstrucao)
        {
            // Entrar na simulação
            uiConstrucao.SetActive(false);
            IniciarSimulacao();
            textoBotao.text = "EDITAR";
        }
        else
        {
            // Voltar à construção
            uiConstrucao.SetActive(true);
            VoltarParaConstrucao();

            // Destroi todas as peças da cena
            foreach (Transform filho in areaConstruida)
                Destroy(filho.gameObject);

            // Restaura o inventário
            foreach (InventorySlot slot in slotsDeInventario)
                slot.Restaurar();

            textoBotao.text = "TESTAR";
        }

        emConstrucao = !emConstrucao;
    }

    // Ativa a simulação em todas as peças
    public void IniciarSimulacao()
    {
        foreach (PecaFisica peca in FindObjectsOfType<PecaFisica>())
            peca.AtivarSimulacao();
    }

    // Volta para o modo de construção
    public void VoltarParaConstrucao()
    {
        foreach (PecaFisica peca in FindObjectsOfType<PecaFisica>())
            peca.AtivarConstrucao();
    }
}
