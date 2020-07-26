using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HotkeyButton : MonoBehaviour {
	public int unitNum;
	public Color selectColor = Color.white;
	public Color unselectColor=Color.black;
	Image img;
	// Use this for initialization
	bool reg;
	void Start () {
		reg=SatTruck.RegisterHotkey (unitNum, this);
		img = GetComponent<Image> ();
	}
	void Update(){
		if (!reg) {
			reg = SatTruck.RegisterHotkey (unitNum, this);
		} else
			enabled = false;
	}
	
	public void OnSelect(){
		img.color = selectColor;
	}
	public void OnUnselect(){
		img.color = unselectColor;
	}
	public void OnDeath(){
		img.color=Color.red;
	}
}
