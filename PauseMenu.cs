using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour
{
    

   [SerializeField] private GameObject pauseMenuUI;

  // public GameObject Player;
  
    // Update is called once per frame

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.isPause)
                Pause();
            else
                Resume();
        }
        
    }

  public void Resume()
    {
        GameManager.isPause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

     
      
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

  public  void Pause()
    {   
        GameManager.isPause = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
     

        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}