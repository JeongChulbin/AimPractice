using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : HealthManager
{
    public GameObject enemy;
    public Text Pointstext;
    public Text PointstextGameOver;
    int score;

    
    // Start is called before the first frame update
    void Start()
    {
        
        score = 0;
        Pointstext.text = score + "��";

        PointstextGameOver.text = score + "��" ;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying)
        {

            score += 30;
            isDestroying = false;
        }
        Pointstext.text = score + "��";

        PointstextGameOver.text = score + "��";
    }
}
