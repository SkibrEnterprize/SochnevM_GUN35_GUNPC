using UnityEngine;
using Zenject;

public class SceneController : MonoBehaviour
{
    //[Inject]
    //public ScenesManager scenesManager;
    //private int currentSceneIndex = 0; 

    public void OpenMainScene()
    {
        //scenesManager.LoadScene(0);
        //Debug.Log("Opened Main Scene");
    }

    public void OpenGameScene()
    {
        //currentSceneIndex = 1; 
        //scenesManager.LoadScene(currentSceneIndex);
        //Debug.Log("Opened Game Scene");
    }
}