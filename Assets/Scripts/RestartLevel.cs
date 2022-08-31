using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    
    public GameObject textOn;
    public GameObject panel;
    
    public float timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        restart();
        exit();
    }

    private void restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    private void exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Sleep());
        }
    }
    
    private IEnumerator Sleep()
    {
        textOn.SetActive(false);
        panel.SetActive(true);
        textOn.SetActive(true);
        
        yield return new WaitForSeconds(timeText);
        
        textOn.SetActive(false);
        panel.SetActive(false);
        
        yield return new WaitForSeconds(1);
        
        restart();
    }
}
