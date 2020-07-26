using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Input.mouseScrollDelta.y;
	}
}
