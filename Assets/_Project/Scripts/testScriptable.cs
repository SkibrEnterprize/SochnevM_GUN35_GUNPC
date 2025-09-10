using UnityEngine;

public class testScriptable : MonoBehaviour
{
    [SerializeField] private SomeConfigScriptable config;

    private void Awake()
    {
        int test = config.Int1;
    }

}
