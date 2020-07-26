using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public Player bluePlayer;
    public Player redPlayer;
    public botSpawner botSpawner;
    public ResourceSpawner ResourceSpawner;
    public int resourceDrainPerSecond = 2;
    private int level = 3;
    public int maxResourceValue = 1000;
    public int initialResourceValue = 500;
    public int minResource5SatTrucks = 200;
    public int minResource4SatTrucks = 150;
    public int minResource3SatTrucks = 100;
    public int minResource2SatTrucks = 50;
    private bool isInitiallied = false;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        bluePlayer.InitResources(initialResourceValue, maxResourceValue);
        redPlayer.InitResources(initialResourceValue, maxResourceValue);
    }



    //Update is called every frame.
    void Update()
    {
        if (!isInitiallied)
        {
            InitGame();
        }
    }

    public void AdjustResources(City aCity, int aValue)
    {
        switch (aCity.team)
        {
            case Team.Blue:
                bluePlayer.UpdateResources(aValue);
                if(bluePlayer.resourceValue == 0)
                {
                    HandleEndGame(redPlayer);
                }
                if (bluePlayer.resourceValue == maxResourceValue)
                {
                    HandleEndGame(bluePlayer);
                }
                break;
            case Team.Red:
                redPlayer.UpdateResources(aValue);
                if (redPlayer.resourceValue == 0)
                {
                    HandleEndGame(bluePlayer);
                }
                if (redPlayer.resourceValue == maxResourceValue)
                {
                    HandleEndGame(redPlayer);
                }
                break;
        }
    }

    protected void HandleEndGame(Player aWinner)
    {

    }
}


