using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
   


    public GameObject Enemy;

    void Start()
    {
        InvokeRepeating("Randomspawn", 1f, 2f);
    }

    void Randomspawn()
    {

        
            float Posx = Random.Range(-30f, -50f);
            float Posy = Random.Range(10f, 15f);
            float Posz = Random.Range(-15f, 15f);

            GameObject enemy = Instantiate(Enemy, new Vector3(Posx, Posy, Posz), Quaternion.identity);
        
    }
}
