using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem  
{
   
    public static void SaveData(CurrencyManager currencyManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Currency.Data";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(currencyManager);
        formatter.Serialize(stream, data);
        stream.Close();

        
    }

    public static Data LoadData()
    {

        string path = Application.persistentDataPath + "/Currency.Data";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;

        }
        else
        {
            Debug.Log("File Not Found at location " + path);
            return null;
        }

         
    }

}
