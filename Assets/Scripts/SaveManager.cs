using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    // Class that handles the saving of the state to the file system
    private string dataDirPath = "";
    private string dataFileName = "elementFinishTimes";

    void Start()
    {
        dataDirPath = Application.persistentDataPath;
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

        return loadedData;
    }
}
