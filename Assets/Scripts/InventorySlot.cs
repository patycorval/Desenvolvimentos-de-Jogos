using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject prefabDaPeca;
    public int quantidadeMaxima = 3;

    private int quantidadeAtual;
    public TextMeshProUGUI textoQuantidade;
    public Button botao;

    void Start()
    {
        Restaurar();
    }

    public void TentarInstanciar()
    {
        if (quantidadeAtual <= 0) return;

        GameObject novaPeca = Instantiate(prefabDaPeca, transform.position, Quaternion.identity);
        quantidadeAtual--;
        AtualizarUI();
    }

    public void Restaurar()
    {
        quantidadeAtual = quantidadeMaxima;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        textoQuantidade.text = "x" + quantidadeAtual;
        botao.interactable = (quantidadeAtual > 0);
    }
}
