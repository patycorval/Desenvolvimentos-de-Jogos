using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Referências de Interface")]
    public GameObject uiConstrucao;
    public GameObject botaoResetar;
    public Transform areaConstruida;
    public List<InventoryItemDragWithLimit> slotsDeInventario;

    [Header("Configuração de Reset da UI")]
    public Vector2 posicaoInicialConstrucaoUI = Vector2.zero;
    public float tempoAnimacao = 0.4f;

    [Header("Configuração de Reset da Câmera")]
    public Vector3 posicaoInicialCamera = new Vector3(0, 0, -10);
    public float zoomInicial = 5f;

    private Coroutine animacaoAtual;
    private Coroutine animacaoCamera;

    public void IniciarSimulacao()
    {
        uiConstrucao.SetActive(false);

        foreach (PecaFisica peca in FindObjectsByType<PecaFisica>(FindObjectsSortMode.None))
        {
            peca.AtivarSimulacao();
        }

        if (botaoResetar != null)
            botaoResetar.SetActive(true);

        // ✅ Ativa o controle da máquina somente após iniciar a simulação
        ControladorMaquina controlador = FindObjectOfType<ControladorMaquina>();
        if (controlador != null)
        {
            controlador.IniciarControle();
            Debug.Log("🚗 Controle da máquina iniciado com sucesso.");
        }
        else
        {
            Debug.LogWarning("⚠️ Nenhum ControladorMaquina encontrado na cena.");
        }
    }

    public void ResetarConstrucao()
    {
        uiConstrucao.SetActive(true);

        if (animacaoAtual != null)
            StopCoroutine(animacaoAtual);
        animacaoAtual = StartCoroutine(AnimarReposicionamentoUI());

        if (animacaoCamera != null)
            StopCoroutine(animacaoCamera);
        animacaoCamera = StartCoroutine(AnimarReposicionamentoCamera());

        foreach (Transform filho in areaConstruida)
        {
            Destroy(filho.gameObject);
        }

        foreach (InventoryItemDragWithLimit slot in slotsDeInventario)
        {
            slot.Restaurar();
        }

        if (botaoResetar != null)
            botaoResetar.SetActive(false);
    }

    private IEnumerator AnimarReposicionamentoUI()
    {
        RectTransform rect = uiConstrucao.GetComponent<RectTransform>();
        Vector2 posInicial = rect.anchoredPosition;
        float tempo = 0f;

        while (tempo < tempoAnimacao)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / tempoAnimacao);
            rect.anchoredPosition = Vector2.Lerp(posInicial, posicaoInicialConstrucaoUI, t);
            yield return null;
        }

        rect.anchoredPosition = posicaoInicialConstrucaoUI;
    }

    private IEnumerator AnimarReposicionamentoCamera()
    {
        Camera cam = Camera.main;
        if (cam == null) yield break;

        Vector3 posInicial = cam.transform.position;
        float zoomInicialAtual = cam.orthographicSize;
        float tempo = 0f;

        while (tempo < tempoAnimacao)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / tempoAnimacao);

            cam.transform.position = Vector3.Lerp(posInicial, posicaoInicialCamera, t);
            cam.orthographicSize = Mathf.Lerp(zoomInicialAtual, zoomInicial, t);

            yield return null;
        }

        cam.transform.position = posicaoInicialCamera;
        cam.orthographicSize = zoomInicial;
    }
}
