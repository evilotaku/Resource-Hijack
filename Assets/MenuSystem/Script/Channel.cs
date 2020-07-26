using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenuAttribute]
public class Channel : ScriptableObject {

	public string displayName;
	public string sceneName;
	public Sprite logo;
	private ChannelLevel cl;
	[TextArea]
	public string description;

	public AudioClip channelMusic;
	public ChannelLevel GetChannelLevel(){
		return cl;
	}
	public void SetChannelLevel(ChannelLevel cl){
		this.cl = cl;
	}

}
