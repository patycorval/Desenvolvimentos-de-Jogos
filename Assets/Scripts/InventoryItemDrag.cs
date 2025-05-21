using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject piecePrefab; // Prefab real da peça
    public Canvas canvas;          // Referência ao Canvas

    private GameObject draggedImage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Cria uma imagem fantasma durante o arrasto
        draggedImage = new GameObject("DraggedImage");
        draggedImage.transform.SetParent(canvas.transform, false);

        Image image = draggedImage.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.raycastTarget = false;
        image.color = new Color(1, 1, 1, 0.7f); // Transparência leve

        RectTransform rt = draggedImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(60, 60);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedImage != null)
        {
            RectTransform rt = draggedImage.GetComponent<RectTransform>();
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out pos);
            rt.localPosition = pos;
        }
    }

   public RectTransform buildAreaRect; // Referência ao BuildArea (UI)

public void OnEndDrag(PointerEventData eventData)
{
    if (draggedImage != null)
    {
        // Converte posição da UI para posição no mundo
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0;

        // Calcula os cantos da área de construção (UI)
        Vector3[] corners = new Vector3[4];
        buildAreaRect.GetWorldCorners(corners);

        Vector3 minWorld = corners[0]; // Inferior esquerdo
        Vector3 maxWorld = corners[2]; // Superior direito

        // Limita o spawn dentro da área da BuildArea
        worldPos.x = Mathf.Clamp(worldPos.x, minWorld.x, maxWorld.x);
        worldPos.y = Mathf.Clamp(worldPos.y, minWorld.y, maxWorld.y);

        // Instancia a peça real no mundo
        GameObject spawned = Instantiate(piecePrefab, worldPos, Quaternion.identity);

        // Corrige escala e opacidade
        spawned.transform.localScale = piecePrefab.transform.localScale;

        SpriteRenderer sr = spawned.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.white;

        // Remove imagem fantasma do drag
        Destroy(draggedImage);
    }
}
}
