using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MovingObject
{
    [SerializeField] private ParticlesPlayer particlesPlayer;
    private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

    private void OnDestroy(){
        _tokenSource.Cancel();
    }

    private void OnTriggerEnter2D(Collider2D other){
        var damageable = other.GetComponent<IDamageable>();

        if (damageable == null) return;
        damageable.TakeDamage();

        Moving = false;
        Collider.enabled = false;
        SpriteRenderer.enabled = false;

        ParticlesPlayAndDeactivate();
    }

    public override void Initialize(Vector2 dir, Vector2 pos){
        base.Initialize(dir, pos);
        Moving = true;
        Collider.enabled = true;
        SpriteRenderer.enabled = true;
    }

    private async void ParticlesPlayAndDeactivate(){
        var playTime = particlesPlayer.PlayDuration;
        particlesPlayer.Play();

        try{
            await Task.Delay(playTime, _tokenSource.Token);
        }
        catch{
            /* ignored*/
        }

        gameObject.SetActive(false);
    }
}