using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //Enables uGUI features

public class Basket : MonoBehaviour
{
  [Header("Set Dynamically")]
  public Text scoreGT; //a
    // Start is called before the first frame update
    void Start()
    {
      //Find a ref to the ScoreCounter Game Object
      GameObject scoreGO = GameObject.Find("ScoreCounter"); //b
      //Get the Text commponent of that GameObject
      scoreGT = scoreGO.GetComponent<Text>(); //c
      // Set the starting number of points to 0
      scoreGT.text = "0";

    }

    // Update is called once per frame
    void Update()
    {
        //Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition; //a
        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z; //b
        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); //c
        // Move the x position of this Basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
    void OnCollisionEnter(Collision coll ) {
      //find out what hit this basketPrefab
      GameObject collidedWith = coll.gameObject; //b
      if(collidedWith.tag == "Apple") { //c
        Destroy(collidedWith);

        //Parse the text of the scoreGT into an integer
        int score = int.Parse(scoreGT.text); //d
        //add points for catching the apple
        score += 100;
        //convert score back to a string and display it
        scoreGT.text = score.ToString();

        //Track the high score
        if (score > HighScore.score) {
          HighScore.score = score;
        }
      }
    }
}
