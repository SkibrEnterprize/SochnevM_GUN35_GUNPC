using UnityEngine;

public class Skittle : MonoBehaviour
{
    [SerializeField] private float _angleThreshold = 45f;

    public bool GetStatusOfFall()
    {
        var rotationX = transform.localEulerAngles.x;
        var rotationZ = transform.localEulerAngles.z;

        if (Mathf.Abs(rotationX) >= _angleThreshold ||
            Mathf.Abs(rotationZ) >= _angleThreshold)
        {
            return true;
        }
        return false;
    }
}
