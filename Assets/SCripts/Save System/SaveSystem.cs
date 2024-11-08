using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPersistentData
{
    public void SaveThis();

}
public class SaveSystem
{
    public static void SaveData<T>(T data, string fileName, bool logDebug = false) where T : struct
    {
        File.WriteAllText(Path.Join(Application.persistentDataPath, fileName), JsonUtility.ToJson(data));
        if (logDebug) Debug.Log("Saved file " + fileName + " to " + Application.persistentDataPath);
    }

}
