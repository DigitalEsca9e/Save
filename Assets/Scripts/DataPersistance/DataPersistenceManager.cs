using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameData gameData;


    //allows the DataPersistenceManage instances to be called publicly but set privately 
    public static DataPersistenceManager instance { get; private set; }


    public void Awake()
    {
        if (instance!=null)
        {
            Debug.LogError("Found More than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    //Stop gap for testing - calls LoadGame at start
    private void Start()
    {
        LoadGame();
    }


    private void NewGame()
    {
        this.gameData =new GameData();
    }

    private void SaveGame()
    {

    }

    private void LoadGame()
    {

        //Checkd for no data and calls NewGame function to create a new data 
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Intializing data to default values");
            NewGame();
        }
    }

    //Stop Gap for testing -calls SaveGame on quit
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
