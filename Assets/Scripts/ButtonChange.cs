using UnityEngine;
using TMPro;

public class ButtonChange : MonoBehaviour
{
    public GameManager gameManager;          // Referência ao GameManager
    public TextMeshProUGUI textoBotao;       // Referência ao texto do botão

    private bool emConstrucao = true;

    public void AlternarModo()
    {
        if (emConstrucao)
        {
            gameManager.IniciarSimulacao();
            textoBotao.text = "EDITAR";
        }
        else
        {
            gameManager.VoltarParaConstrucao();
            textoBotao.text = "TESTAR";
        }

        emConstrucao = !emConstrucao;
    }
}
