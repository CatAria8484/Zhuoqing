using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

internal class UserDataManager : MonoBehaviour
{
    public static UserDataManager instance = null;

    internal UserData userData;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    internal void SaveUserData()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, userData);
        file.Close();
    }

    internal void LoadUserData()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            userData = (UserData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        catch (FileNotFoundException exception)
        {
            Debug.Log(exception.Message);
            userData = new UserData();
        }
    }

    public void DeleteUserData()
    {
        UserData _userData = new UserData();

        FileStream file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, _userData);
        file.Close();
    }
}

[Serializable]
internal class UserData
{
    public float f_val = 0.5f;
    public float f_current_val = 0.5f;
    public float w_val = 0.5f;
    public float w_current_val = 0.5f;
    public int isClear = 0;
    public int currentFile = 1;
    public int currentLine = 0;
}