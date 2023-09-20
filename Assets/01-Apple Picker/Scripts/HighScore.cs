using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //goooo eeeeeee

public class HighScore : MonoBehaviour{
static public int score = 1000; //a
    // Update is called once per frame
    void Update()
    {
      Text gt = this.GetComponent<Text>();
      gt.text = "High Score: "+score;
    }
}
