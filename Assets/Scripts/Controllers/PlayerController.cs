using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    private Controls _controls;
    private Vector2 _moveInput;

    [Inject]
    private void Construct(Controls controls) {  _controls = controls; }
   
    private void OnEnable()
    {
        // Подписка на события
        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }
  
    void Update()
    {
        // Движение персонажа
        Vector2 movement = _moveInput.normalized * _moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnDisable()
    {
        _controls.Player.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled -= ctx => _moveInput = Vector2.zero;
    }
}
