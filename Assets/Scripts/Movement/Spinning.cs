using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    
    //SCRIPT TO BE DELETED
    
    void Update()
    {
        transform.Rotate(0, 300.0f * Time.deltaTime, 0);
    }
}
