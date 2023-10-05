using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
  static public GameObject POI;

  [Header("Set in Inspector")]
  public float easing = 0.05f;
  public Vector2 minXY = Vector2.zero;
  [Header("Set Dynamically")]
  public float camZ;

  void Awake() {
    camZ = this.transform.position.z;
  }

  void FixedUpdate() {
    //if there's only one line following an if, it doesn't need braces
  //  if (POI == null) return;  //return if there is no POI

  //Get the position of the POI
//  Vector3 destination = POI.transform.position;
  Vector3 destination;
  //If there is no POI return to P: 0,0,0,
  if (POI ==null) {
    destination =Vector3.zero;
  }else {
    //Get the position of the POI
    destination = POI.transform.position;
    //If poi is a projectile check to see if it's at rest
    if (POI.tag == "Projectile") {
      //if it is sleeping (not moving)
      if (POI.GetComponent<Rigidbody>().IsSleeping()) {
        //return to default view
        POI =null;
        //in the next Update
        return;
      }
    }
  }
  //Limit the X and Y to minimum values
  destination.x = Mathf.Max(minXY.x, destination.x);
  destination.y = Mathf.Max(minXY.y, destination.y);
  //Interpolate from the current camera position toward destination
  destination = Vector3.Lerp(transform.position, destination, easing);
  //force destination.z to be camZ to keep the camera far enough away
  destination.z = camZ;
  //set camera transform to the destination
  transform.position = destination;
  //Set the orthographicSize of the Camera to keep the ground in view
  Camera.main.orthographicSize = destination.y + 10;
  }
}
