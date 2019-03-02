using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Cenario1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HoverEffect(Transform button)
    {
        button.localScale = new Vector3(1.2f, 1.2f);
    }

    public void UnhoverEffect(Transform button)
    {
        button.localScale = new Vector3(1f, 1f);
    }
}
