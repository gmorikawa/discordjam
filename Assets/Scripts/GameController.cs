using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Flags")]
    public bool isPaused = false;
    
    [Header("Pause sprites")]
    public Sprite pauseButton;
    public Sprite continueButton;

    [Header("UI")]
    public GameObject darkenEffect;
    public GameObject btnPause;

    void TogglePause()
    {
        Time.timeScale = isPaused ? 1f : 0f;
        isPaused = Time.timeScale == 0f;
    }

    public void PauseButton()
    {
        if(isPaused)
        {
            btnPause.GetComponent<Image>().sprite = pauseButton;
        }
        else
        {
            btnPause.GetComponent<Image>().sprite = continueButton;
        }
        
        TogglePause();
        darkenEffect.SetActive(isPaused);
    }

    public void GameToGame()
    {
        LoaderManager.Instance.LoadScene("Cenario1");
    }

    public void GameToMenu()
    {
        Time.timeScale = 1f;

        if (LoaderManager.Instance == null)
            print("Print");

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
