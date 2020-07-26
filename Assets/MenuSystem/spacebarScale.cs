using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spacebarScale : MonoBehaviour {

	Image i;
	
	// Update is called once per frame
	void Update () {
		if (!i) {
			i = GetComponent<Image> ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			i.transform.localScale = new Vector3(i.transform.localScale.x,i.transform.localScale.y*1.1f,i.transform.localScale.z);
		}
	}
}
