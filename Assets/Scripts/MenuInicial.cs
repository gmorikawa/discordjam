using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Play()
    {
        LoaderManager.Instance.LoadScene("Cenario1");
    }

    public void Credits()
    {
        LoaderManager.Instance.LoadScene("Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CreditsToMenu()
    {
        LoaderManager.Instance.LoadScene("Menu");
    }

    public void HoverEffect(Transform button)
    {
        button.localScale = new Vector3(0.9f, 0.9f);
    }

    public void UnhoverEffect(Transform button)
    {
        button.localScale = new Vector3(0.8f, 0.8f);
    }
}
