using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float hitPoint = 30f;
    public static bool isDestroying;
    
  

    public void ApplyDamage(float damage)
    {
        hitPoint -= damage;
        if(hitPoint <= 0)
        {
            Destroy(gameObject);
            isDestroying = true;
            
        }
    }
  
}
