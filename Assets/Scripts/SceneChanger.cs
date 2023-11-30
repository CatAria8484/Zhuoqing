using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    GameObject Sliders;
    GameObject Mainscreen;
    GameObject MainflieScreen;
    GameObject InfiltrationScreen;
    GameObject ExplorationScreen;

    int WhatFile = 1;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)      // access only MainScene
        {
            Sliders = GameObject.Find("Sliders").transform.GetChild(0).gameObject;
            Mainscreen = GameObject.Find("MainScreen").transform.GetChild(0).gameObject;
            MainflieScreen = GameObject.Find("MainFileScreen").transform.GetChild(0).gameObject;
            InfiltrationScreen = GameObject.Find("InfiltrationScreen").transform.GetChild(0).gameObject;
        }

        if (instance == null) instance = this;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)      // access only MainScene
            if (StoryManager.instance.isClear > 0)
                Mainscreen.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
    }

    public void InTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void InMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void InMainScreen()
    {
        Sliders.SetActive(true);
        Mainscreen.SetActive(true);
        MainflieScreen.SetActive(false);
        InfiltrationScreen.SetActive(false);
    }

    public void InStoryLineScreen()
    {
        Sliders.SetActive(false);
        Mainscreen.SetActive(false);
        MainflieScreen.SetActive(true);
        InfiltrationScreen.SetActive(false);
    }

    public void InInfiltrationScreen()
    {
        Mainscreen.SetActive(false);
        MainflieScreen.SetActive(false);
        InfiltrationScreen.SetActive(true);
        StoryManager.instance.StartStory(WhatFile);
    }

    public void InExplorationScreen()
    {

    }

    public void SelectMainfile_1()
    {
        WhatFile = 1;
        InInfiltrationScreen();
    }

    public void SelectMainfile_2()
    {
        WhatFile = 2;
        InInfiltrationScreen();
    }

    public void SelectMainfile_3()
    {
        WhatFile = 3;
        InInfiltrationScreen();
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
