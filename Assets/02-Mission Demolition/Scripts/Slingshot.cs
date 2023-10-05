using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
  static private Slingshot S;
  [Header("Set in Inspector")]
  public GameObject prefabProjectile;
  public float velocityMult = 8f;
  [Header("Set Dynamically")]
  public GameObject launchPoint;
  public Vector3 launchPos;
  public GameObject projectile;
  public bool aimingMode;
  private Rigidbody projectileRigidbody;

  static public Vector3 LAUNCH_POS {
    get {
      if (S == null) return Vector3.zero;
      return S.launchPos;
    }
  }

  void Awake() {
    S = this; //c
    Transform launchPointTrans = transform.Find("LaunchPoint");
    launchPoint = launchPointTrans.gameObject;
    launchPoint.SetActive(false); //b
    launchPos = launchPointTrans.position;
}
  void Start() {

  }

  void OnMouseEnter(){
    //print("Slingshot:OnMouseEnter()");
    launchPoint.SetActive(true); //b
  }

  void OnMouseExit() {
    //print("Slingshot:OnMouseEnter");
    launchPoint.SetActive( false ); //b
  }
  void OnMouseDown() {
    aimingMode = true;
    projectile = Instantiate(prefabProjectile) as GameObject;
    projectile.transform.position = launchPos;
    projectile.GetComponent<Rigidbody>().isKinematic = true;
    projectileRigidbody = projectile.GetComponent<Rigidbody>();
    projectileRigidbody.isKinematic = true;
  }

  void Update() {
    if(!aimingMode) return;
    // Get the current mouse position in 2D screen coordinates
    Vector3 mousePos2D = Input.mousePosition; // c
    mousePos2D.z = -Camera.main.transform.position.z;
    Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

// Find the delta from the launchPos to the mousePos3D
    Vector3 mouseDelta = mousePos3D-launchPos;
// Limit mouseDelta to the radius of the Slingshot SphereCollider // d
    float maxMagnitude = this.GetComponent<SphereCollider>().radius;
    if (mouseDelta.magnitude > maxMagnitude) {
      mouseDelta.Normalize();
      mouseDelta *= maxMagnitude;
    }

//Move the projectile to this new position
    if (Input.GetMouseButtonUp(0)) {
      //The mouse has been released
      aimingMode = false;
      projectileRigidbody.isKinematic = false;
      projectileRigidbody.velocity = -mouseDelta * velocityMult;
      FollowCam.POI = projectile;
      projectile = null;
    }

  }


}
