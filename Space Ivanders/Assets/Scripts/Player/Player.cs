using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour, IDamageable
{
    public event Action PlayerTakeHitEvent;

    [SerializeField] private Transform firePoint;
    private readonly int _hit = Animator.StringToHash("Hit");
    
    private ShipInputReader _inputReceiver;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Weapon _weapon;
    private Motor _motor;

    public void Initialize(ShipInputReader input, float speed, float shotDelay, Transform bulletParent, Bullet bulletPrefab){
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _weapon = new Weapon(bulletPrefab, shotDelay, firePoint, bulletParent);
        _motor = new Motor(transform, _rigidbody, speed);

        _inputReceiver = input;
        _inputReceiver.FireEvent += Shoot;
        _inputReceiver.MoveEvent += Move;
    }

    private void OnDisable(){
        _inputReceiver.FireEvent -= Shoot;
        _inputReceiver.MoveEvent -= Move;
    }

    private void FixedUpdate() => _motor.Move();

    private void Shoot() => _weapon.Shoot(Vector2.up);

    private void Move(Vector3 dir) => _motor.OnInput(dir);

    public void TakeDamage(){
        _animator.SetTrigger(_hit);
        PlayerTakeHitEvent?.Invoke();
    }
}
