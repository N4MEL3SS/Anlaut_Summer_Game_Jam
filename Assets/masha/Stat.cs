using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public static Stat Instance;
    
    void Awake (){
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this){
            Destroy (gameObject);
        }
    }

    // private void Update()
    // {
    //     if (PlayerPrefs.HasKey("Cube"))
    //     {
    //         Debug.Log("exist");
    //     }
    // }
}
