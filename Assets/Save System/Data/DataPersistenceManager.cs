using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Data Configuration")]

    [SerializeField] string fileName;
    public static DataPersistenceManager Instance { get; private set; }

    private GameData gameData;

    private List<IDataPersistance> dataPersistances;

    private FileDataHandler fileDataHandler;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Error: More then one Data Persistence Manager in the scene.");
        }
        Instance = this;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistances = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        //load any saved data
        //if no data can be loaded initialize new data

        if (this.gameData == null)
        {
            Debug.Log("Error: attempted to load empty data, setting data settings to default.");
            NewGame();
        }

        foreach (IDataPersistance ID in dataPersistances)
        {
            ID.LoadData(gameData);
        }

       // Debug.Log("Battery: " + gameData.BatteryCount.ToString());
    }

    public void SaveGame() 
    {
        //pass data to other scripts to be saved
        //save data to file using data handler

        foreach (IDataPersistance ID in dataPersistances)
        {
            ID.SaveData(ref gameData);
        }

       // Debug.Log("Battery: " + gameData.BatteryCount.ToString());

        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistenceObjects);
    }
}
