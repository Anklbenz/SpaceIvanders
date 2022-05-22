using System;
using UnityEngine.UI;

public class UIDrawer : IDisposable
{
    private readonly Text _livesLabel, _scoreLabel;
    private StatsHandler _stat;

    public UIDrawer(Text livesLabel, Text scoreLabel){
        _livesLabel = livesLabel;
        _scoreLabel = scoreLabel;
    }

    public void StatConnect(StatsHandler stat){
        _stat = stat;
        _stat.ScoreChangedEvent += ScoreLabelUpdate;
        _stat.LiveCountChangedEvent += LiveLabelUpdate;
    }

    public void Dispose(){
        _stat.ScoreChangedEvent -= ScoreLabelUpdate;
        _stat.LiveCountChangedEvent -= LiveLabelUpdate;
    }

    private void LiveLabelUpdate(){
        _livesLabel.text = $"{_stat.LivesCount:00}";
    }

    private void ScoreLabelUpdate(){
        _scoreLabel.text = $"{_stat.Score:0000}";
    }
}
