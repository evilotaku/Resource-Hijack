using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceHelper : MonoBehaviour {
	static VoiceHelper instance;
	public static VoiceHelper GetInstance(){
		return instance;
	}
	AudioSource aud;
	public List<AudioClip> prompt;
	public List<AudioClip> ack;
	// Use this for initialization
	void Start () {
		instance = this;
		aud = GetComponent<AudioSource> ();
	}
	
	public void PlayPrompt(){
		int i=Random.Range (0, prompt.Count);
		aud.PlayOneShot (prompt [i]);
	}
	public void PlayAck(){
		int i=Random.Range (0, prompt.Count);
		aud.PlayOneShot (ack [i]);
	}
    public void PlayNotification(NotificationType aType)
    {
        switch (aType)
        {
            case NotificationType.YouWin:
                PlayPrompt();
                break;
            case NotificationType.YouLoose:
                PlayPrompt();
                break;
            case NotificationType.TruckCmdInititated:
                PlayAck();
                break;
            case NotificationType.BotHiJacked:
                PlayPrompt();
                break;
            case NotificationType.Truck5Deactivated:
                PlayPrompt();
                break;
            case NotificationType.Truck4Deactivated:
                PlayPrompt();
                break;
            case NotificationType.Truck3Deactivated:
                PlayPrompt();
                break;
            case NotificationType.Truck2Deactivated:
                PlayPrompt();
                break;
            case NotificationType.Welcome:
                PlayAck();
                break;
        }
    }
}
