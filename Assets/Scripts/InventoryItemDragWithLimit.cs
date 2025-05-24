using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDragWithLimit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefabDaPeca;
    public RectTransform areaConstruida;
    public TextMeshProUGUI textoQuantidade;
    public int quantidadeMaxima = 1;

    private int quantidadeAtual;
    private GameObject arrastando;

    void Start()
    {
        quantidadeAtual = quantidadeMaxima;
        AtualizarUI();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (quantidadeAtual <= 0) return;

        arrastando = new GameObject("Peca Fantasma");
        var sr = arrastando.AddComponent<SpriteRenderer>();
        sr.sprite = GetComponent<Image>().sprite;
        sr.sortingOrder = 50;
        arrastando.transform.localScale = Vector3.one * 0.15f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (arrastando == null) return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0f;
        arrastando.transform.position = worldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (arrastando != null)
        {
            Destroy(arrastando);
        }

        if (quantidadeAtual <= 0) return;

        // Converte a posição da tela para o mundo
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0f;

        Debug.Log("Tentando instanciar peça em: " + worldPos);

        GameObject novaPeca = Instantiate(prefabDaPeca, worldPos, Quaternion.identity);

        // Corrige visual e física
        novaPeca.transform.localScale = Vector3.one * 0.15f;
        novaPeca.layer = LayerMask.NameToLayer("Default");

        SpriteRenderer sr = novaPeca.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = 20;

        Debug.Log("Instanciada: " + novaPeca.name + " em " + novaPeca.transform.position);

        // Atualiza quantidade e UI
        quantidadeAtual--;
        AtualizarUI();

        // Desativa botão se acabou
        if (quantidadeAtual <= 0)
        {
            GetComponent<Button>().interactable = false;
        }
    }

void AtualizarUI()
{
    if (textoQuantidade != null)
        textoQuantidade.text = "x" + quantidadeAtual;

    var botao = GetComponent<Button>();
    if (botao != null)
        botao.interactable = quantidadeAtual > 0;
}

        public void Restaurar()
    {
        quantidadeAtual = quantidadeMaxima;
        AtualizarUI();
    }
}
