using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private ArrowConfig _config;    
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
        _angle = Mathf.Sin(Time.time * _config.SwingSpeed) * _config.MaxAngle;
        transform.rotation = Quaternion.Euler(_initX, _initY + _angle, _initZ);
    }
    private void MoveLeftRigth()
    {
        if (moveRight)
        {
            transform.Translate(Vector3.right * _config.MoveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.left * _config.MoveSpeed * Time.deltaTime, Space.World);
        }
        if (transform.position.x > _config.Distance || transform.position.x < -_config.Distance)
        {
            moveRight = !moveRight;
        }
    }
    public Vector3 GetDirection()
    {
        return transform.forward;
    }
}
