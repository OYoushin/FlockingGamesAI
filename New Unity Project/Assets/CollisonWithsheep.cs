using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class CollisonWithsheep : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("hit Hit");
    }
}
