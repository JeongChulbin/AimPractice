using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public float startTime;
    float minX;
    float maxX;
    public float moveSpeed;
    public int sign = -1;

    private void Start()
    {
        minX += transform.position.x - 4;
        maxX += transform.position.x + 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= startTime)
        {
            //이동 로직 초리
            transform.position += new Vector3(moveSpeed * Time.deltaTime * sign, 0, 0 );

            if(transform.position.x <= minX || transform.position.x >= maxX)
            {
                sign *= -1;
            }
        }
    }
}
