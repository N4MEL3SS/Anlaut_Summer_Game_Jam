using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TestSfer 
    : MonoBehaviour
{
    public GameObject thing;
    public GameObject dot;
    public GameObject menuTwo;
    public GameObject menuOne;
    public GameObject applying;
    private bool flag = false;
    private GameObject obj;


    private void Start()
    {
        if (flag == false)
        {
            Vector3 spawnPoint = applying.transform.position + Vector3.up * 2;
            obj = Instantiate(dot, spawnPoint, transform.rotation * Quaternion.Euler (0f, 0f, -180f));
            flag = true;
        }
    }

    void OnMouseDown()
  {
      if (PlayerPrefs.HasKey("Cub"))
      {
          // Instantiate(enemy);
          PlayerPrefs.DeleteKey("Cub");
          Destroy(obj);
          menuOne.SetActive(true);
          menuTwo.SetActive(true);
      }
      
  }

  public void One()
  {
      Instantiate(thing);
      menuTwo.SetActive(false);
      menuOne.SetActive(false);
  }

  public void two()
  {
      menuTwo.SetActive(false);
      menuOne.SetActive(false);
  }
  
  
  // void OnMouseOver()
  // {
  //     if (PlayerPrefs.HasKey("Cub"))
  //     {
  //         dot.SetActive(false);
  //     }
  //     // Texts.SetActive(true);
  // }

  private void OnMouseExit()
  {
      // Texts.SetActive(false);
  }
}
