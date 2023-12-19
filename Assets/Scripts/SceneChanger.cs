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

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)      // access only MainScene
        {
            Sliders = GameObject.Find("Sliders").transform.GetChild(0).gameObject;
            Mainscreen = GameObject.Find("MainScreen").transform.GetChild(0).gameObject;
            MainflieScreen = GameObject.Find("MainFileScreen").transform.GetChild(0).gameObject;
            InfiltrationScreen = GameObject.Find("InfiltrationScreen").transform.GetChild(0).gameObject;
            UserDataManager.instance.LoadUserData();

            if (instance == null)
                instance = this;
        }
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

        UserDataManager.instance.LoadUserData();
        if (UserDataManager.instance.userData.isClear > 0)
            Mainscreen.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
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
        Sliders.SetActive(true);
        UserDataManager.instance.LoadUserData();
        StoryManager.instance.StartStory(UserDataManager.instance.userData.currentFile);
        //InMainScreen();
    }

    public void InExplorationScreen()
    {

    }

    public void SelectMainfile_1()
    {
        UserDataManager.instance.LoadUserData();
        UserDataManager.instance.userData.currentFile = 1;
        UserDataManager.instance.SaveUserData();
        InInfiltrationScreen();
    }

    public void SelectMainfile_2()
    {
        UserDataManager.instance.LoadUserData();
        UserDataManager.instance.userData.currentFile = 2;
        UserDataManager.instance.SaveUserData();
        InInfiltrationScreen();
    }

    public void SelectMainfile_3()
    {
        UserDataManager.instance.LoadUserData();
        UserDataManager.instance.userData.currentFile = 3;
        UserDataManager.instance.SaveUserData();
        InInfiltrationScreen();
    }
}
