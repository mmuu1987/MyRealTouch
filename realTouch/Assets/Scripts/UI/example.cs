using UnityEngine;
using System.Collections;

public class example : MonoBehaviour {
 
	 
	public void Awake() {

        Transform cam = Camera.main.transform;

        Vector3 cameraRelative = cam.InverseTransformPoint(transform.position);
		if (cameraRelative.z > 0)
            print("The object is in front of the camera" + cameraRelative);
		else
            print("The object is behind the camera" + cameraRelative);
	}
}