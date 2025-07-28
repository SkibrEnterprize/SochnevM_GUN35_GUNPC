using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float _swingSpeed = 2f;
    [SerializeField] private float _maxAngle = 30f; 

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _distance = 3f;
    private float _initX;
    private float _initY;
    private float _initZ;
    private float _angle;
    public bool moveRight = true;

    private void Start()
    {
        _initX = transform.rotation.eulerAngles.x;
        _initY = transform.rotation.eulerAngles.y;
        _initZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        Rotate();
        MoveLeftRigth();
    }

    private void Rotate()
    {
        _angle = Mathf.Sin(Time.time * _swingSpeed) * _maxAngle;
        transform.rotation = Quaternion.Euler(_initX, _initY + _angle, _initZ);
    }
    private void MoveLeftRigth()
    {
        if (moveRight)
        {
            transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (transform.position.x > _distance || transform.position.x < -_distance)
        {
            moveRight = !moveRight;
        }
    }
    public Vector3 GetDirection()
    {
        return transform.forward;
    }
}
