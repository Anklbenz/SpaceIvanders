using UnityEngine;

public class Shooting
{
    private readonly Weapon _weapon;
    private readonly Enemy[,] _enemies;
    private readonly int _cols, _rows;

    public Shooting(Bullet[] prefabs, Enemy[,] enemies){
        _enemies = enemies;
        _weapon = new Weapon(prefabs);
        _rows = enemies.GetLength(0);
        _cols = enemies.GetLength(1);
    }

    public void Shoot(){
        var enemy = ChooseShootingEnemy();
        if (enemy)
            _weapon.Shoot(Vector2.down, enemy.FirePoint.position);
    }

    private Enemy ChooseShootingEnemy(){
        Enemy enemy = null;

        var shotCol = Random.Range(0, _cols);
        for (var i = 0; i < _rows; i++){
            if (!_enemies[i, shotCol].gameObject.activeInHierarchy) continue;
            enemy = _enemies[i, shotCol];
            break;
        }

        return enemy;
    }
}
