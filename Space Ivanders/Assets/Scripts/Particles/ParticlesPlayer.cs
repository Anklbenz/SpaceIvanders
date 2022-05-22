using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    public int PlayDuration{ get; private set; }

    public Color Color{
        set => _main.startColor = value;
    }

    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _main;

    private void Awake(){
        _particleSystem = GetComponent<ParticleSystem>();
        _main = _particleSystem.main;
        
        PlayDuration = (int) (_main.duration * 1000); // milliseconds
    }
    
    public void Play() => _particleSystem.Play();
}
