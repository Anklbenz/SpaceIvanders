using UnityEngine;

public class BonusUfo : MovingObject, IDamageable
{
   public override void Initialize(Vector2 dir, Vector2 pos){
        base.Initialize(dir, pos);
        gameObject.SetActive(true);
        
        Moving = true;
        Collider.enabled = true;
        SpriteRenderer.enabled = true;
    }

    public void TakeDamage(){
        Moving = false;
        Collider.enabled = false;
        SpriteRenderer.enabled = false;
    }
}
