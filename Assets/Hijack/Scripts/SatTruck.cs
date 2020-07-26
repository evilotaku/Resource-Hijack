using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SatTruck : MonoBehaviour
{
	static List<SatTruck> allSats= new List<SatTruck>();
	public static int RegisterSelf(SatTruck st){
		allSats.Add (st);
		return allSats.Count - 1;
	}
	public static bool RegisterHotkey(int i, HotkeyButton img){
		if (i < allSats.Count) {
			allSats [i].SetHotkey (img);
			return true;
		} else
			return false;
	}
	public static SatTruck GetTruck(int i){
		if (i >= allSats.Count)
			return null;
		return allSats [i];
	}



    public Team currentCityType;
    public float Speed;
    public float TruckBumperDistance;
    public float RadarDisatance;
    public Material newMaterialRef;
	NavMeshAgent agent;
	HotkeyButton hotkey;
	LineRenderer lr;
    // Use this for initialization
    void Start () {
        //Shader shader = GetComponent<Shader>();
        //switch (currentCityType)
        //{
        //    case CityType.Player:
        //        shader.material = City.playerMaterial;
        //        newMaterialRef = City.playerMaterial;
        //        break;
        //    case CityType.AI:
        //        shader.material = City.aiMaterial;
        //        newMaterialRef = City.aiMaterial;
        //        break;
        //    case CityType.Robot:
        //        shader.material = City.robotMaterial;
        //        newMaterialRef = City.robotMaterial;
        //        break;
        //}
		agent=GetComponent<NavMeshAgent>();
		lr = GetComponentInChildren<LineRenderer> ();
		if(currentCityType==Team.Blue)
			SatTruck.RegisterSelf(this);
    }
	void Update(){
		lr.SetPosition (0, transform.position+Vector3.up*5);
		lr.SetPosition (1, agent.destination);
	}
	public void SetHotkey(HotkeyButton i){
		hotkey = i;
	}
	public void OnSelect(){
		if (hotkey)
			hotkey.OnSelect ();
		if (lr)
			lr.enabled= (true);
		VoiceHelper.GetInstance ().PlayPrompt ();
		 
	}
	public void OnUnselect(){
		if (hotkey)
			hotkey.OnUnselect ();
		if (lr)
			lr.enabled=false;
	}

	public void OnDeath(){
		hotkey.OnDeath();
	}
	public void SetDestination(Vector3 pos){
		agent.SetDestination (pos);
		VoiceHelper.GetInstance ().PlayAck ();
	}
}
