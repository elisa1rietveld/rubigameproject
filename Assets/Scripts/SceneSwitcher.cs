using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadDemoScene()
    {
        StartCoroutine(LoadSceneWithDelay("DemoScene", 0.3f));
    }

    public void LoadStartScene()
    {
        StartCoroutine(LoadSceneWithDelay("StartMenu", 0.3f));
    }

    public void LoadOptionsScene()
    {
        StartCoroutine(LoadSceneWithDelay("Options", 0.3f));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
