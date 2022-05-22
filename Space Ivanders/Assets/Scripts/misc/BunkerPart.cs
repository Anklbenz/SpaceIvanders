using System.Threading.Tasks;
using UnityEngine;

public class BunkerPart : MonoBehaviour, IDamageable
{
    public void TakeDamage(){
        //Paticles play
        gameObject.SetActive(false);
    }
}
