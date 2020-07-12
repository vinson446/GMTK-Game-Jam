using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BASD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotomenu()
    {
        SceneManager.LoadScene(0);
    }

    public void startgame()
    {
        SceneManager.LoadScene(3);
    }
}
