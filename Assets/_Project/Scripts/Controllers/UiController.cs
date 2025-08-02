using UnityEngine;

public sealed class UiController : MonoBehaviour
{
    [SerializeField] private UIConfig _uiConfig;        
    public void DisplayAttemptsScore(string score)
    {
    _uiConfig.TotalAttemptsText.text = score;        
    }
    public void DisplayTotalSkittle(string score)
    {
        _uiConfig.TotalSkittleText.text = score;
    }
    public void UpdateTotalScore(string score)
    {
    _uiConfig.TotalScoreText.text = score;
    }
    public void UpdateThrowNumber(string score)
    {
    _uiConfig.TotalThrowsText.text = score;
    }

}
