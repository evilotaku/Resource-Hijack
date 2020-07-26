using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChannelLevel : MonoBehaviour {
	public string channelName="insert name";
	public UnityEvent StartLevel ;
	public UnityEvent LoseFocus;
	public UnityEvent GainFocus;
	public UnityEvent EndLevel;

	public Camera gameCamera;
	public virtual Camera GetCamera(){
		return gameCamera;
	}
	public virtual void Init(){
		
	}
	// Use this for initialization
	void Start () {
		ChannelSystem.GetInstance ().RegisterChannel (this);
		Init ();
	}
	





}
