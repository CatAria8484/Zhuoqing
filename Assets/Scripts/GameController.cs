using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    UserData userData;

    GameObject Bag;

    Slider f_Doubt;
    Slider w_Doubt;

    bool isOpen = false;
    bool isGood, isF;

    void Start()
    {
        Bag = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(2).transform.GetChild(1).gameObject;

        f_Doubt = GameObject.Find("Sliders").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        w_Doubt = GameObject.Find("Sliders").transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Slider>();

        // load data
        LoadUserData();
        f_Doubt.value = userData.f_val;
        w_Doubt.value = userData.w_val;
    }

    void Update()
    {
        // save data
        userData.f_val = f_Doubt.value;
        userData.w_val = w_Doubt.value;
        userData.isClear = StoryManager.instance.isClear;
        SaveUserData();
    }

    public void BadChoice()
    {
        isGood = false;

        ChangeDoubt();
    }

    public void GoodChoice()
    {
        isGood = true;

        ChangeDoubt();
    }

    void ChangeDoubt()
    {
        if (isGood && isF)
            f_Doubt.value -= 0.1f;
        else if (isGood && !isF)
            w_Doubt.value -= 0.1f;
        else if (!isGood && isF)
            f_Doubt.value += 0.1f;
        else if (!isGood && !isF)
            w_Doubt.value += 0.1f;
    }

    void CheckFDoubt()
    {
        if (f_Doubt.value >= 0.8)
        {
            StoryManager.instance.z_Dead();
        }
    }

    void CheckWDoubt()
    {
        if (w_Doubt.value <= 0.5)
        {
            StoryManager.instance.z_getHint();
        }
    }

    public void OpenBag()
    {
        float x, y;
        x = Bag.transform.position.x;
        y = Bag.transform.position.y;

        if (!isOpen)    // open
        {
            Bag.transform.position = new Vector2(x - 135, y);
            isOpen = true;
        }
        else            // close
        {
            Bag.transform.position = new Vector2(x + 135, y);
            isOpen = false;
        }
    }

    void SaveUserData()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, userData);
        file.Close();
    }

    void LoadUserData()
    {
        FileStream file;

        try
        {
            file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Open);
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
}

[Serializable]
class UserData
{
    public float f_val = 0.5f;
    public float w_val = 0.5f;
    public int isClear = 0;
}