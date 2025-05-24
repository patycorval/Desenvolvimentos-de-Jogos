using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject uiConstrucao; // Todo o Canvas de UI da construção
    public TextMeshProUGUI textoBotao;
    public Transform areaConstruida; // Onde as peças são instanciadas
    public List<InventoryItemDragWithLimit> slotsDeInventario;

    private bool emConstrucao = true;

    public void AlternarModo()
    {
        if (emConstrucao)
        {
            // Modo TESTAR
            uiConstrucao.SetActive(false);
            IniciarSimulacao();
            textoBotao.text = "EDITAR";
        }
        else
        {
            // Modo EDITAR
            uiConstrucao.SetActive(true);
            VoltarParaConstrucao();

            // Limpa peças da área de construção
            foreach (Transform filho in areaConstruida)
                Destroy(filho.gameObject);

            // Restaura todos os slots
            foreach (var slot in slotsDeInventario)
                slot.Restaurar();

            textoBotao.text = "TESTAR";
        }

        emConstrucao = !emConstrucao;
    }

  public void IniciarSimulacao()
{
    foreach (var peca in FindObjectsOfType<PecaFisica>())
        peca.AtivarSimulacao();
}

public void VoltarParaConstrucao()
{
    foreach (var peca in FindObjectsOfType<PecaFisica>())
        peca.AtivarConstrucao();
}

}
