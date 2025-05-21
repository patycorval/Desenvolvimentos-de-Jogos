using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CarregarMontagemScene()
    {
        SceneManager.LoadScene("MontagemScene");
    }

    public void CarregarSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}