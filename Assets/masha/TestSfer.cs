using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSfer 
    : MonoBehaviour
{
    public GameObject enemy;
    // public GameObject Texts;
  void OnMouseDown()
  {
      if (PlayerPrefs.HasKey("Cub"))
      {
          Instantiate(enemy);
          // PlayerPrefs.DeleteKey("Cube");
      }
  }
  
  // void OnMouseOver()
  // {
  //     Text.SetActive(true);
  // }
}
