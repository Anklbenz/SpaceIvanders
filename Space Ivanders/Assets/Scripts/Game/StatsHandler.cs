using System;

public class StatsHandler : IDisposable
{
    public event Action ScoreChangedEvent, LiveCountChangedEvent;
    public event Action<bool> LiveIsOutEvent;

    public int Score => _score;
    public int LivesCount => _livesCount;
    private int _score, _livesCount;
    private readonly Player _player;
    private readonly Enemy[,] _enemies;

    public StatsHandler(Enemy[,] enemies, Player player, int livesCount){
        _player = player;
        _player.PlayerTakeHitEvent += SpendLife;
        _livesCount = livesCount;
        _enemies = enemies;
        foreach (var enemy in _enemies)
            enemy.EnemyDeadEvent += AddScore;
    }

    public void LabelsRefresh(){
        ScoreLabelRefresh();
        LiveLabelRefresh();
    }

    public void Dispose(){
        _player.PlayerTakeHitEvent += SpendLife;
        foreach (var enemy in _enemies)
            enemy.EnemyDeadEvent -= AddScore;
    }

    private void AddScore(Enemy sender){
        _score += sender.Points;
        ScoreLabelRefresh();
    }

    private void ScoreLabelRefresh(){
        ScoreChangedEvent?.Invoke();
    }

    private void SpendLife(){
        _livesCount--;
        if (_livesCount < 0){
            LiveIsOutEvent?.Invoke(false);
            return;
        }

        LiveLabelRefresh();
    }

    private void LiveLabelRefresh(){
        LiveCountChangedEvent?.Invoke();
    }
}