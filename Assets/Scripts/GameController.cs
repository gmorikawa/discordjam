using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Flags")]
    public bool isPaused = false;
    
    [Header("Pause sprites")]
    public Sprite pauseButton;
    public Sprite continueButton;

    void TogglePause()
    {
        Time.timeScale = isPaused ? 1f : 0f;
        isPaused = Time.timeScale == 0f;
    }

    public void PauseButton(GameObject button)
    {
        button.GetComponent<Image>().sprite = isPaused ? pauseButton : continueButton;
        TogglePause();
    }
}
