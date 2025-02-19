using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadDemoScene()
    {
        SceneManager.LoadScene("DemoScene");
    }
}
