using UnityEngine;

public sealed class UIController : MonoBehaviour
{
    [SerializeField] private UIConfig _uiConfig;
   public void UpdateTotalDirtCollect(string text)
    {
       _uiConfig.TotalDirtCollect.text = text;
    }
}
