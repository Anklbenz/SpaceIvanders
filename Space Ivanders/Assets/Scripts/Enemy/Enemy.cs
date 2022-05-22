using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action<Enemy> EnemyDeadEvent;
    public event Action<Vector2> HitTheWallEvent;

    public Transform FirePoint => firePoint;
    public Color SpriteColor => spriteRenderer.color;
    public Vector2 Position => transform.position;
    public int Points => pointCost;

    [SerializeField] private int pointCost;
    [SerializeField] private LayerMask boundsLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticlesPlayer particlesPlayer;
    private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
    private BoxCollider2D _collider;

    private void Awake(){
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnDestroy(){
        _tokenSource.Cancel();
    }

    public async void TakeDamage(){
        EnemyDeadEvent?.Invoke(this);

        _collider.enabled = false;
        spriteRenderer.enabled = false;

        var particlesPlayTime = particlesPlayer.PlayDuration;
        particlesPlayer.Color = spriteRenderer.color;
        particlesPlayer.Play();

        try{
            await Task.Delay(particlesPlayTime, _tokenSource.Token);
            gameObject.SetActive(false);
        }
        catch{
            // ignored
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if ((boundsLayer.value & (1 << other.gameObject.layer)) > 0)
            HitTheWallEvent?.Invoke(other.transform.position.x < transform.position.x ? Vector2.left : Vector2.right);
    }
}