using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            StartCoroutine(WaitAndLoad());
        });
    }
    
    IEnumerator WaitAndLoad()
    {
        // Disable and gray out button
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = Color.gray;
        
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        
        // Load next scene
        SceneManager.LoadScene(1);
    }
}