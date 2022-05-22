using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class ShipInputReader : ScriptableObject, InputReceiver.IInputActions
{
    public event Action<Vector3> MoveEvent;
    public event Action FireEvent;
    private InputReceiver _inputActions;

    private void OnEnable(){
        _inputActions = new InputReceiver();
        _inputActions.Enable();
        _inputActions.Input.SetCallbacks(this);
    }

    private void OnDisable(){
        _inputActions.Disable();
    }

    public void OnMove(InputAction.CallbackContext ctx){
        var movement = Vector2.left * ctx.ReadValue<float>();
        MoveEvent?.Invoke(movement);
    }

    public void OnFire(InputAction.CallbackContext context){
        FireEvent?.Invoke();
    }
}