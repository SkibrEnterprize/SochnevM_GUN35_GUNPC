using UnityEngine;

public class Skittle : MonoBehaviour
{
    [SerializeField] private TriggerOfFall _triggerOfFall;

    public bool GetStatusOfFall()
    {
        return _triggerOfFall.IsFalling;
    }
}
