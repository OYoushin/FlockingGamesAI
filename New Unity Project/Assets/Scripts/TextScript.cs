using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public static int sheepDead = 0;
    public static int sheepSafe = 0;
    Text score;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Sheep Dead -  " + sheepDead+ "   Sheep Safe - " + sheepSafe;
    }
}
