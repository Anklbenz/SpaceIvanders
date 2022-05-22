using System;
using UnityEngine;

public class Movement : IDisposable
{
    public float Speed{ get; set; }
    
    private readonly Transform _transform;
    private readonly Enemy[,] _enemies;
    private readonly float _verticalOffset;
    private Vector2 _direction = Vector2.right;

    public Movement(Enemy[,] enemies, Transform transform, float speed, float verticalOffset){
        _enemies = enemies;
        _transform = transform;
        _verticalOffset = verticalOffset;
        
        Speed = speed;

        foreach (var enemy in _enemies)
            enemy.HitTheWallEvent += WallHitLogic;
    }

    public void Dispose(){
        foreach (var enemy in _enemies)
            enemy.HitTheWallEvent -= WallHitLogic;
    }

    public void Move(){
        _transform.Translate(_direction * Speed * Time.deltaTime);
    }

    private void WallHitLogic(Vector2 dir){
        if (dir != _direction) return;

        _direction *= -1;
        _transform.Translate(Vector2.down * _verticalOffset);
    }

}
