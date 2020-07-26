using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MouseControls : MonoBehaviour {
	public static MouseControls instance;
	void Awake(){
		instance = this;
	}
	Vector2 lastMousePos;
	public SatTruck selected;
	public Image cursor;
	//public Vector2 cursorOffset = new Vector2 (330, 230);
	GraphicRaycaster gr;
	Button lastButton;
	public EventSystem eventSystem;
	Camera cam;
	public void SelectTruck(int i){
		if(selected)
			selected.OnUnselect ();
		selected=SatTruck.GetTruck (i);
		selected.OnSelect();
	}

	// Update is called once per frame
	void Update () {
		Vector2 mousePos = Input.mousePosition;//ScreenMouse.GetScreenMousePosition();
		//Debug.Log(mousePos);
		if(cursor){
			cursor.rectTransform.position = mousePos;//new Vector3(mousePos.x-cursorOffset.x,mousePos.y-cursorOffset.y,0);
		}
		//On Mouse Click
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			/*
			if (!gr) {
				gr = GetComponentInChildren<GraphicRaycaster> ();
			} else {
				//Set up a fake UI Event call
				//this was necessary for the CRT menu and can be removed now.
				//this lets us force events through the tv
				PointerEventData ped = new PointerEventData (eventSystem);
				ped.position = mousePos;

				List<RaycastResult> results = new List<RaycastResult>();
				gr.Raycast (ped,results);


				foreach (RaycastResult r in results) {
					Button b = r.gameObject.GetComponent<Button> ();
					
					if (b) {
						if(selected)
							selected.OnUnselect();
						b.OnPointerClick (ped);
						
					}
					Debug.Log (r.gameObject.name);
				}
			}
			*/
			if (cam == null) {
				cam = Camera.main;
			}
			//raycast to see what we hit
			if (Physics.Raycast (cam.ScreenPointToRay(mousePos), out hit)) {
				Debug.Log (hit.collider.gameObject.name);

				SatTruck stTest=hit.collider.gameObject.GetComponentInParent<SatTruck> ();
				//if it is a SatTruck, Select it
				if (stTest!=null&&stTest.currentCityType.Equals(Team.Blue)) {
					if(selected)
						selected.OnUnselect();
					selected = stTest;
					selected.OnSelect();
				}
				lastMousePos =mousePos;
			}
			//else if we are holding down the mouse Button
		} else if (Input.GetMouseButton (0)) {
			Vector2 delta = mousePos - lastMousePos;
			transform.localPosition += new Vector3 (-delta.x, 0, -delta.y);
			lastMousePos = mousePos;
			//else if we right click
		} else if (Input.GetMouseButtonDown (1)&&selected!=null) {
			//Debug.Log ("Right Click");
			RaycastHit hit;
			if (Physics.Raycast (cam.ScreenPointToRay(mousePos), out hit)) {
				//Debug.Log ("raycasted succes");
				if (hit.collider.gameObject.name.Equals ("Ground")) {
					selected.SetDestination (hit.point);
					Debug.Log ("Destination:" + hit.point);
				}
			}
		}
	}
}
