using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TestSfer 
    : MonoBehaviour
{
    public GameObject enemy;
    public GameObject dot;
    public GameObject menuTwo;
    public GameObject menuOne;
  void OnMouseDown()
  {
      if (PlayerPrefs.HasKey("Cub"))
      {
          // Instantiate(enemy);
          PlayerPrefs.DeleteKey("Cub");
          dot.SetActive(false);
          menuOne.SetActive(true);
          menuTwo.SetActive(true);
      }
      
  }

  public void One()
  {
      Instantiate(enemy);
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
