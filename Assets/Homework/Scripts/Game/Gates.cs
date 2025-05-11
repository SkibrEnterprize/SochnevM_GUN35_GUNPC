using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Ball>() != null)
        {
            _score++;
            Debug.Log($"Total score is - {_score}!!!");
            Destroy(other.gameObject);
        }
    }
}
