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

        if (PlayerPrefs.HasKey("snekOne"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("snekOne");
        }
        if (PlayerPrefs.HasKey("snekTwo"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("snekTwo");
        }
        if (PlayerPrefs.HasKey("snekThree"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("snekThree");
        }
        if (PlayerPrefs.HasKey("snecFour"))
        {
            healthBar.transform.localScale += new Vector3(15, 0, 0);
            PlayerPrefs.DeleteKey("snecFour");
        }
        
    }
}
