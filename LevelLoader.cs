using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] private float fadeTime = 1f;
    private Texture2D fadeTexture;
    private float fadeAlpha = 0f;
    private bool fading = false;
    
    void Start()
    {
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, Color.black);
        fadeTexture.Apply();
    }
    
    private void Update()
        {
        if (Input.GetKeyDown(KeyCode.N))
            {
                LoadNextLevel();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadCurrentLevel();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadPrevLevel();
            }
        }

    public void LoadPrevLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(StartFade());
        }
    }

    IEnumerator StartFade()
    {
        fading = true;
        float timer = 0f;
        
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            fadeAlpha = Mathf.Clamp01(timer / fadeTime);
            yield return null;
        }
        
        fadeAlpha = 1f;
        LoadNextLevel();
    }

    void OnGUI()
    {
        if (fading && fadeAlpha > 0)
        {
            // Draw black texture over entire screen
            GUI.color = new Color(0, 0, 0, fadeAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }
}