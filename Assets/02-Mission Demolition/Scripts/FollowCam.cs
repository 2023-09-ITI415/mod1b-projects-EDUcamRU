using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
  static public GameObject POI;

  [Header("Set Dynamically")]
  public float camZ;

  void Awake() {
    camZ = this.transform.position.z;
  }

  void FixedUpdate() {
    //if there's only one line following an if, it doesn't need braces
    if (POI == null) return;  //return if there is no POI

  //Get the position of the POI
  Vector3 destination = POI.transform.position;
  //force destination.z to be camZ to keep the camera far enough away
  destination.z = camZ;
  //set camera transform to the destination
  transform.position = destination;
  }
}
