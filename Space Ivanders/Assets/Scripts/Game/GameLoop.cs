using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : IDisposable
{
    public event Action<bool> AllEnemiesDestroyedEvent;
    public readonly StatsHandler StatsHandler;
    public DeadZone DeadZone{ get; }

    private const int DEAD_COUNT = 20;

    private const float INCREASE_STEP = 0.015f;
    private readonly EnemiesHandler _enemiesHandler;
    private readonly BonusSpawner _bonusSpawner;
    private readonly List<Enemy> _enemiesList;
    private int _deadCounter;

    public GameLoop(EnemiesHandler enemies, Player player, DeadZone deadZone, BonusUfo bonusPrefab, Transform bonusPoint, int livesCount){
        DeadZone = deadZone;
        _bonusSpawner = new BonusSpawner(bonusPrefab, bonusPoint);
        _enemiesList = new List<Enemy>();
        _enemiesHandler = enemies;
        foreach (var enemy in _enemiesHandler.Enemies){
            enemy.EnemyDeadEvent += IncreaseEnemySpeed;
            enemy.EnemyDeadEvent += BonusUfoSpawn;
            enemy.EnemyDeadEvent += EnemyCountCheck;
            _enemiesList.Add(enemy);
        }

        StatsHandler = new StatsHandler(_enemiesHandler.Enemies, player, livesCount);
    }

    public void Dispose(){
        foreach (var enemy in _enemiesHandler.Enemies){
            enemy.EnemyDeadEvent -= IncreaseEnemySpeed;
            enemy.EnemyDeadEvent -= BonusUfoSpawn;
            enemy.EnemyDeadEvent -= EnemyCountCheck;
        }
    }

    private void BonusUfoSpawn(Enemy enemy){
        _deadCounter++;
        if (_deadCounter == DEAD_COUNT)
            _bonusSpawner.BonusRun(Vector2.right);
    }

    private void IncreaseEnemySpeed(Enemy enemy){
        _enemiesHandler.Movement.Speed += INCREASE_STEP;
    }

    private void EnemyCountCheck(Enemy enemy){
        _enemiesList.Remove(enemy);

        if (_enemiesList.Count == 0)
            AllEnemiesDestroyedEvent?.Invoke(true);
    }
}