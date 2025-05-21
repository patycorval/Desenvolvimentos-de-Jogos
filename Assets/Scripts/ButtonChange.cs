using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonChange : MonoBehaviour
{
    public GameManager gameManager;           // Script que ativa/desativa física
    public TextMeshProUGUI textoBotao;        // Texto do botão (TESTAR/EDITAR)
    public GameObject uiConstrucao;           // Painel que contém o inventário + build area

    private bool emConstrucao = true;

    public void AlternarModo()
    {
        if (emConstrucao)
        {
            // Entrar no modo de simulação
            uiConstrucao.SetActive(false); // Esconde BuildArea + Inventário
            gameManager.IniciarSimulacao();
            textoBotao.text = "EDITAR";
        }
        else
        {
            // Voltar ao modo de construção
            uiConstrucao.SetActive(true); // Mostra BuildArea + Inventário
            gameManager.VoltarParaConstrucao();
            textoBotao.text = "TESTAR";
        }

        emConstrucao = !emConstrucao;
    }
}
