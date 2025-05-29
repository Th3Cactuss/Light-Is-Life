using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        //address of the file we are saving to in the computer's data
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream)) 
                    { 
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from JSON back to C# data

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("Error occured while trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data) 
    { 
        //address of the file we are saving to in the computer's data
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //creates a directory path in case one doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            Debug.Log(Path.GetDirectoryName(fullPath));  

            //serialize C# data into a JSON file

            string dataToStore = JsonUtility.ToJson(data, true);

            //write the data to the save file

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }

        catch (Exception e) //catches an error that may occur while saving
        { 
        Debug.Log("Error occured while trying to save data to: " +  fullPath + '\n' + e);
        }
    }
}
