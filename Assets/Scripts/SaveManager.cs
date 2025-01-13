using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{

    private string dataDirPath = "";
    private string dataFileName = "elementFinishTimes";

    void Start()
    {
        dataDirPath = Application.persistentDataPath;
    }


    void Update()
    {
        
    }

    public void Save(Dictionary<string, List<float>> elementTimes)
    {

        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Directory.CreateDirectory(dataDirPath);

        string dataToStore = JsonConvert.SerializeObject(elementTimes);

        using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }

        Debug.Log("Saved data to: " +  fullPath);

    }

    public Dictionary<string, List<float>> Load ()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Dictionary<string, List<float>> loadedData = new Dictionary<string, List<float>>();


        if (File.Exists(fullPath)) {

            string dataToLoad = "";

            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }
            loadedData = JsonConvert.DeserializeObject <Dictionary<string, List<float>>>(dataToLoad);
        }

        Debug.Log("Loaded Data");

        return loadedData;
    }
}
