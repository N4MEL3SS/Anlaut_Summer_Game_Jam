using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectScript : MonoBehaviour
{ 
    void OnMouseDown()
    {
        Destroy(gameObject);
        PlayerPrefs.SetInt(gameObject.name, 1);
    }
}
