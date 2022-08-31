using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergiBand : MonoBehaviour
{
    public float MaxEnergi = 100f;
    public float Lostenergi = 1000f;
    public float CurrentEnergi = 75f;

    public GameObject healthBar;
    private void FixedUpdate()
    {
        if (CurrentEnergi > 1)
        {
            CurrentEnergi = CurrentEnergi - Time.fixedDeltaTime * Lostenergi;
            // SizeMinus();
            healthBar.transform.localScale -= new Vector3(Time.fixedDeltaTime * Lostenergi, 0, 0);
        }
        Debug.Log((int)CurrentEnergi + "%");

        if (PlayerPrefs.HasKey("Almond"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("Almond");
        }
        if (PlayerPrefs.HasKey("Milk"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("Milk");
        }
        if (PlayerPrefs.HasKey("Crunch"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("Crunch");
        }
        if (PlayerPrefs.HasKey("OG"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("OG");
        }
        
    }
}
