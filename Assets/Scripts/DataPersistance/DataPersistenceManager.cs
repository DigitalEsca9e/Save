using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private FileDataHandler dataHandler;

    // Start is called before the first frame update
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
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

        this.dataHandler=new FileDataHandler(Application.persistentDataPath, fileName);// Application.persistentDataPath sets the directory to the directory for persistent files for the application

        this.dataPersistenceObjects =FindAllDataPersistenceObjects();
        LoadGame();
    }


    private void NewGame()
    {
        this.gameData =new GameData();
    }

    private void SaveGame()
    {
        //cycles through all IdataPersistence objects and saves the appropriate data
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects){
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void LoadGame()
    {

        this.gameData= dataHandler.Load();

        //Checkd for no data and calls NewGame function to create a new data 
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Intializing data to default values");
            NewGame();
        }

        //cycles through all IdataPersistence objects and loads the appropriate data
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects){
            dataPersistenceObj.LoadData(gameData);
        }
    }

    //Stop Gap for testing -calls SaveGame on quit
    private void OnApplicationQuit()
    {
        SaveGame();
    }


    //function to find all IdataPersistence objects with MonoBehavior
    private List<IDataPersistence> FindAllDataPersistenceObjects(){
        IEnumerable<IDataPersistence> dataPersistenceObjects =FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        //returns a list of IDataPersistence objects
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
