using UnityEngine;
public class ScenesManager : MonoBehaviour 
{
    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}