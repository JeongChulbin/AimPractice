using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class TimeLimit : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;

    public float LimitTime;
    public Text text_Timer;
   
    void Start()
    {
        
    }
    void Update()
    {
        if(LimitTime >0 )
        {
            LimitTime -= Time.deltaTime;
        }
        else if(LimitTime <= 0)
        {
            GameManager.isPause = true;
            GameOver.SetActive(true);
            Time.timeScale = 0f;
            GameManager.canPlayerMove = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        text_Timer.text = "½Ã°£:" + Mathf.Round(LimitTime);
    }

    public void Retry()
    {
        SceneManager.LoadScene(2);
        GameManager.isPause = false;
        GameOver.SetActive(false);
        Time.timeScale = 1f;
        GameManager.canPlayerMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public  void Title()
    {
        SceneManager.LoadScene(0);
    }

    
}
