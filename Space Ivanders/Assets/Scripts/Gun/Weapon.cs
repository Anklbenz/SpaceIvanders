using UnityEngine;

public class Weapon
{
  private const int POOL_AMOUNT = 5;

  private readonly PoolObjects<Bullet> _pool;
  private readonly PoolObjects<Bullet>[] _pools;
  private readonly Transform _firePoint;
  private readonly float _delay;
  private float _nextShotTime;

  public Weapon(Bullet prefab, float delay, Transform firePoint, Transform parent = null){
    _delay = delay;
    _firePoint = firePoint;
    _pool = new PoolObjects<Bullet>(prefab, POOL_AMOUNT, true, parent);
  }

  public Weapon(Bullet[] prefabs, Transform parent = null){
    _pools = new PoolObjects<Bullet>[prefabs.Length];

    for (var i = 0; i < prefabs.Length; i++)
      _pools[i] = new PoolObjects<Bullet>(prefabs[i], POOL_AMOUNT, true, parent);
  }

  public void Shoot(Vector2 dir){
    if (_nextShotTime > Time.realtimeSinceStartup) return;
    _nextShotTime = _delay + Time.realtimeSinceStartup;

    var bullet = _pool.GetFreeElement();
    bullet.Initialize(dir, _firePoint.position);
  }

  public void Shoot(Vector2 dir, Vector2 firePoint){
    var randomPool = Random.Range(0, _pools.Length);
    var bullet = _pools[randomPool].GetFreeElement();
    bullet.Initialize(dir, firePoint);
  }
}