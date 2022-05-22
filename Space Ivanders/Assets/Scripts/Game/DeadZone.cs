using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public event Action<bool> DeadZoneEnterEvent; 
    
    private void OnTriggerEnter2D(Collider2D other){
        var enemy = other.transform.GetComponent<Enemy>();
        if(enemy)
            DeadZoneEnterEvent?.Invoke(false);
    }
}
