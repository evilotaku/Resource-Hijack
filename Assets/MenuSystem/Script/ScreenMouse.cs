using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMouse : MonoBehaviour {
	static Camera cam; 
	/*
	public delegate void MouseEvent(int index,Vector2 xy);
	public MouseEvent MouseButton;
	public MouseEvent MouseButtonDown;
	public MouseEvent MouseButtonUp;
	*/
	public LineRenderer rayTester;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		//MouseButtonDown += empty;
	}
	/*
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if (Physics.Raycast (cam.ScreenPointToRay(Input.mousePosition), out hit)) {
				Debug.Log (hit.textureCoord2);
				Camera c=ChannelSystem.GetInstance ().c [ChannelSystem.GetInstance ().channelIndex].GetChannelLevel ().GetCamera ();
				Ray r = c.ViewportPointToRay (hit.textureCoord);

				rayTester.SetPosition(0,r.origin);
				rayTester.SetPosition(1,r.origin+ r.direction*1000);
				MouseButtonDown (0, hit.textureCoord);
			}
		}
	}
	void empty(int i, Vector2 xy){
		
	}
	*/
	public static Ray GetScreenMouseRay(){
		RaycastHit hit;
		Ray r=new Ray();
		if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit)) {
			Camera c=ChannelSystem.GetInstance ().c [ChannelSystem.GetInstance ().channelIndex].GetChannelLevel ().GetCamera ();
			r = c.ViewportPointToRay (hit.textureCoord);

		}
		return r;
	}
	public static Vector2 GetScreenMousePosition(){
		RaycastHit hit;
		
		if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit)) {
			
			return new Vector2(hit.textureCoord.x*640,hit.textureCoord.y*480);
		}
		return Vector2.zero;
	}
}
