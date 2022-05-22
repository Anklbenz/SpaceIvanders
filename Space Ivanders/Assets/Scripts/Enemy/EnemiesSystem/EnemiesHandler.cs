using UnityEngine;

public class EnemiesHandler : MonoBehaviour
{
    public Enemy[,] Enemies => _enemies;
    public Movement Movement{ get; private set; }
    public Shooting Shooting{ get; private set; }

    [Range(0, 2)]
    [SerializeField] private float speed, verticalOffset;
    [SerializeField] private Bullet[] bulletPrefabs;
    [SerializeField] private float shotDelay;
    [Header("GridSettings")]
    [SerializeField] private Enemy[] enemiesPrefabs;
    [Range(1, 11)]
    [SerializeField] private int enemiesCount;
    [SerializeField] private Vector2 gridOffset;

    private Enemy[,] _enemies;
    private float _shotTimer;

    private void Awake(){
        _enemies = new Enemy[enemiesPrefabs.Length, enemiesCount];

        CreateMap(enemiesPrefabs, enemiesPrefabs.Length, enemiesCount, gridOffset, transform);
        Shooting = new Shooting(bulletPrefabs, _enemies);
        Movement = new Movement(_enemies, transform, speed, verticalOffset);
    }

    private void FixedUpdate(){
        Movement.Move();

        if (_shotTimer >= Time.realtimeSinceStartup) return;
        Shooting.Shoot();
        _shotTimer = Time.realtimeSinceStartup + shotDelay;
    }

    private void CreateMap(Enemy[] prefabs, int rows, int cols, Vector2 offset, Transform parent){
        for (var i = 0; i < rows; i++){
            for (var j = 0; j < cols; j++){
                _enemies[i, j] = Object.Instantiate(prefabs[i], parent);
                _enemies[i, j].transform.Translate(new Vector3(offset.x * j, offset.y * i));
            }
        }
    }
}