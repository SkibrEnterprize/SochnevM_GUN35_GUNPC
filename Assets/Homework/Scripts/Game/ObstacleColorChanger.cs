using System.Collections;
using UnityEngine;

public class ObstacleColorChanger : MonoBehaviour
{
    [SerializeField]
    private Color _goalColor = Color.green;
    private Renderer _renderer;
    private Coroutine _coroutineResetColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _coroutineResetColor = null;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            _renderer.material.color = _goalColor;
            if (_coroutineResetColor != null) StopCoroutine(_coroutineResetColor);
            _coroutineResetColor = StartCoroutine("ResetColor");
        }
    }
    private IEnumerator ResetColor()
    {
        yield return new WaitForSecondsRealtime(1f);
        _renderer.material.color = Color.white;
    }

}

