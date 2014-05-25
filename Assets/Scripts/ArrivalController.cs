using UnityEngine;
using System.Collections;

public class ArrivalController : MonoBehaviour {
  public float arrivalDuration;
  public MonoBehaviour nextController;

  private float arrivalEnd;
  private float startZ;
  private float zRange;


  void Start() {
    arrivalEnd = Time.time + arrivalDuration;
    startZ = transform.position.z;
    zRange = 0.0f - startZ;
  }


  void FixedUpdate() {
    float t = Time.time;
    if (t >= arrivalEnd) {
      enabled = false;
      nextController.enabled = true;
      return;
    }

    float deltaT = 1.0f - (arrivalEnd - t) / arrivalDuration;
    float deltaRadians = Mathf.PI  * deltaT / 2.0f;

    Vector3 newPos = transform.position;
    newPos.z = Mathf.Sin(deltaRadians) * zRange + startZ;
    transform.position = newPos;
  }
}
