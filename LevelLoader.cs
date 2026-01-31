using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    private bool triggered = false;

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
            StartCoroutine(LoadNextLevelDelayed(1.5f));
        }
    }

    IEnumerator LoadNextLevelDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextLevel();
    }
}