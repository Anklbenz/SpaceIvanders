using UnityEngine;

public class BonusSpawner
{
    private readonly BonusUfo _prefabUfo;
    private readonly Transform _createPoint;

    public BonusSpawner(BonusUfo prefab, Transform createPoint){
        _createPoint = createPoint;
        _prefabUfo = prefab;
        _prefabUfo = Object.Instantiate(prefab, createPoint.position, Quaternion.identity);
        _prefabUfo.gameObject.SetActive(false);
    }

    public void BonusRun(Vector2 dir){
        _prefabUfo.Initialize(dir, _createPoint.position);
    }
}
