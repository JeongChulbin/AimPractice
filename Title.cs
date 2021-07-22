using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
}
