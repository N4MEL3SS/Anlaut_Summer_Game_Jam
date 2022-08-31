using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOn : MonoBehaviour
{
    public GameObject textOn;
    public GameObject panel;
    
    public float timeText;

    private bool _isRead = false;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Fire")) && !_isRead)
        {
            StartCoroutine(textShow());
        }
    }

    private IEnumerator textShow()
    {
        panel.SetActive(false);
        panel.SetActive(true);
        textOn.SetActive(true);
        
        yield return new WaitForSeconds(timeText);
        
        textOn.SetActive(false);
        panel.SetActive(false);
        _isRead = true;
    }
}
