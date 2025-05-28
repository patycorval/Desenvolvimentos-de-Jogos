using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject painelVitoria;

    public void FecharPainel()
    {
        Time.timeScale = 1f;
        painelVitoria.SetActive(false);
    }
}
