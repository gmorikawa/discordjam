using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{
    public Image fadeImage;
    public string startScene;
    public float fadeSpeed = 1f;
    public SoundEffects soundEffects;
    private string currentScene = null;

    public Camera cam;

    private void Start()
    {
        LoadScene(startScene);
    }

    public void LoadScene(string sceneName)
    {
        soundEffects.StopGenerating();
        StartCoroutine(Loading(sceneName));
    }
    
    IEnumerator Loading(string sceneName)
    {
        if (currentScene != null)
        {
            yield return StartCoroutine(FadeOut());
            yield return SceneManager.UnloadSceneAsync(currentScene);
            cam.gameObject.SetActive(true);
        }
        
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        cam.gameObject.SetActive(false);

        currentScene = sceneName;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        if (currentScene == "Cenario1")
            soundEffects.StartGenerating();

        yield return StartCoroutine(FadeIn());
    }

    public IEnumerator FadeOut()
    {
        float i = 0f;
        while(i < 1f)
        {
            i += Time.deltaTime * fadeSpeed;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, i);
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float i = 0f;
        while (i < 1f)
        {
            i += Time.deltaTime * fadeSpeed;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, i);
            yield return null;
        }
    }

    private static LoaderManager _instance;
    public static LoaderManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<LoaderManager>();
            return _instance;
        }
    }
}
