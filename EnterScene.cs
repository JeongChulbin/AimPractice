using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameManager.isPause = false;
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
            GameManager.canPlayerMove = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
           
        }
    }
}
