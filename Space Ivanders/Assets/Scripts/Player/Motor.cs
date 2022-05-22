using System;
using UnityEngine;
using UnityEngine.UI;

public class Motor
{
    private readonly Rigidbody2D _rigidbody;
    private readonly Transform _transform;
    private readonly float _speed;
    private Vector3 _direction;

    public Motor(Transform transform, Rigidbody2D rigidbody, float speed = 1f){

        _speed = speed;
        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void OnInput(Vector3 direction){
        _direction = direction;
    }

    public void Move(){
        
        _rigidbody.MovePosition(_transform.position + _direction * Time.deltaTime * _speed);
    }
}
