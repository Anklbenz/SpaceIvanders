using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovingObject : MonoBehaviour
{
   [SerializeField] private float speed, lifeTime;
   protected SpriteRenderer SpriteRenderer;
   protected BoxCollider2D Collider;
   protected bool Moving;

   private Rigidbody2D _rigidbody;
   private Vector2 _direction;
   private float _timer;

   private void Awake(){
      _rigidbody = GetComponent<Rigidbody2D>();
      SpriteRenderer = GetComponent<SpriteRenderer>();
      Collider = GetComponent<BoxCollider2D>();
   }

   public virtual void Initialize(Vector2 dir, Vector2 pos){
      transform.position = pos;
      _direction = dir;
      _timer = Time.realtimeSinceStartup + lifeTime;
   }

   private void FixedUpdate(){
      if (!Moving) return;

      Move();
      LifeTimeCheck();
   }

   private void Move(){
      _rigidbody.MovePosition(transform.position + (Vector3) _direction * speed * Time.deltaTime);
   }

   private void LifeTimeCheck(){
      if (_timer < Time.realtimeSinceStartup)
         gameObject.SetActive(false);
   }
}
