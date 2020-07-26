using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public City City;
    public Text playerNameComp;
    public Text resourceValueComp;
    public Text resourceMaxValueComp;
    public Slider resourceSlider;
    public string PlayerName;
    public int resourceValue = 500;
    public int maxResourceValue = 1000;

    public void InitGame()
    {

    }

	// Use this for initialization
	void Start () {
        playerNameComp.text = PlayerName + " Resources: ";
        InitResources(resourceValue, maxResourceValue);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateResources(int aValue)
    {
        resourceValue += aValue;
        if (resourceValue < 0)
        {
            resourceValue = 0;
        }
        if (resourceValue > maxResourceValue)
        {
            resourceValue = maxResourceValue;
        }

        resourceValueComp.text = resourceValue.ToString();
    }

    public void InitResources(int aCurrentValue, int aMaxValue)
    {
        resourceValueComp.text = aCurrentValue.ToString();
        resourceSlider.value = aCurrentValue;
        resourceValue = aCurrentValue;
        resourceMaxValueComp.text = aMaxValue.ToString();
        maxResourceValue = aMaxValue;
    }
}
