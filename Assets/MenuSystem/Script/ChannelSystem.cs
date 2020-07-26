using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class ChannelSystem : MonoBehaviour {
	
	static ChannelSystem instance;
	public Camera menuCam;
	public List<Channel> c;
	public List<ChannelLevel> activeChannels;
	public int channelIndex;
	bool waitForRegister=false;
	bool pollForLoad=false;
	public RenderTexture rt;
	public Image logo;
	public Image loadBar;
	public Text gameName;
	public Text displayText;
	public Text channelText;
	public AudioSource channelMusic;
	public static ChannelSystem GetInstance(){
		if (ChannelSystem.instance == null) {
			Debug.Log ("Creating CHannel System");
			GameObject temp = new GameObject ("ChannelSystem");
			instance=temp.AddComponent<ChannelSystem> ();
			instance.Init ();
			Debug.Log ("Lazy instantiation assuming a level is being loaded");
			instance.waitForRegister = true;
		}
		return ChannelSystem.instance;
	}
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
			activeChannels = new List<ChannelLevel> ();
		} else if (instance != this) {
			Debug.Log ("Channel System already exists, committing seppuku");
			Destroy (this.gameObject);
		}
	}

	void Start(){
		Init ();
	}
	void Init(){
		if (!channelMusic)
			channelMusic = GetComponentInChildren<AudioSource> ();
		Object[] c = Resources.LoadAll ("Channels", typeof(Channel));
		this.c = new List<Channel> ();
		foreach (Object o in c) {
			this.c.Add ((Channel)o);
		}
		channelIndex = -1;
		ChangeChannel ();
	}
	bool lockInput=false;
	void Update(){
		if (!lockInput) {
			HandleInput ();
		}
	}

	public void HandleInput(){
		if (Input.GetKeyDown (KeyCode.Tab)) {
			ChangeChannel ();
		} else if (pollForLoad && (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))) {
			lockInput = true;
			channelMusic.Stop();
			StartCoroutine (asyncLoadScene (c [channelIndex]));
		}
	}

	public void ChangeChannel(){
		//on startup we can safely ignore this
		channelMusic.Stop();
		ChannelLevel temp;
		if (channelIndex > -1) {
			temp = c [channelIndex].GetChannelLevel ();
			if (temp) {
				temp.LoseFocus.Invoke ();
				temp.GetCamera ().depth = -1;
			}
		}
		channelIndex++;
		channelIndex = channelIndex % c.Count;
		temp = c [channelIndex].GetChannelLevel ();
		gameName.text = c [channelIndex].displayName;
		displayText.text = c [channelIndex].description;
		if (temp) {
			pollForLoad = false;
			temp.GetCamera ().targetTexture = rt;
			temp.GetCamera ().depth = 0;
			menuCam.gameObject.SetActive (false);
			temp.GainFocus.Invoke();
			StopCoroutine ("showText");
			StartCoroutine (showText (c [channelIndex].displayName,2.5f));
		} else {
			if(c [channelIndex].logo!=null&&logo!=null)
				logo.sprite = c [channelIndex].logo;
			pollForLoad = true;
			menuCam.gameObject.SetActive (true);
			channelMusic.clip=c[channelIndex].channelMusic;
			channelMusic.Play();
		}
	}





	public void RegisterChannel(ChannelLevel cl){
		activeChannels.Add (cl);
		if (waitForRegister) {
			waitForRegister = false;
			cl.transform.position = Vector3.one * 1000 * (channelIndex + 1);
			lockInput = false;
			if (c [channelIndex] == null) {
				c [channelIndex] = new Channel ();
			}
			c [channelIndex].SetChannelLevel (cl);
			cl.StartLevel.Invoke ();
			cl.GainFocus.Invoke ();
			//cl.GetCamera ().gameObject.AddComponent<CameraFilterPack_TV_80> ().Fade=0.25f;

			if (rt) {
				
				cl.GetCamera ().targetTexture = rt;
				cl.GetCamera ().depth = 5;
			}

		}
	}
	
	IEnumerator asyncLoadScene(Channel chan){
		Debug.Log("Attempting to load:"+chan.sceneName);
		AsyncOperation ao=SceneManager.LoadSceneAsync (chan.sceneName, LoadSceneMode.Additive);
		ao.allowSceneActivation=false;
		if (loadBar) {
			loadBar.transform.parent.gameObject.SetActive (true);
			loadBar.fillAmount = 0.0f;
		}
		
		while (ao.progress<0.9f) {
			yield return 0;
			if (loadBar)
				loadBar.fillAmount = (ao.progress / 0.9f);
		}
		Debug.Log("Load complete");
		pollForLoad = false;
		waitForRegister = true;
		ao.allowSceneActivation = true;
		if(loadBar)
			loadBar.transform.parent.gameObject.SetActive (false);

	}

	IEnumerator showText(string t, float time){
		channelText.text = t;
		yield return new WaitForSeconds (time);
		channelText.text = "";
	}
}
