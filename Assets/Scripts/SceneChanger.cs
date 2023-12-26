using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance = null;

    GameObject Sliders;
    GameObject Mainscreen;
    GameObject MainflieScreen;
    GameObject InfiltrationScreen;
    GameObject ExplorationScreen;
    GameObject GameOverScreen;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)      // access only MainScene
        {
            Sliders = GameObject.Find("Sliders").transform.GetChild(0).gameObject;
            Mainscreen = GameObject.Find("MainScreen").transform.GetChild(0).gameObject;
            MainflieScreen = GameObject.Find("MainFileScreen").transform.GetChild(0).gameObject;
            InfiltrationScreen = GameObject.Find("InfiltrationScreen").transform.GetChild(0).gameObject;

            GameOverScreen = GameObject.Find("GameOverScreen").transform.GetChild(0).gameObject;
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

        UserDataManager.Instance.LoadUserData();
        if (UserDataManager.Instance.userData.isClear > 0)
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

        StoryManager.Instance.StartStory();
    }

    public void InExplorationScreen()
    {

    }

    public void InGameOverScreen()
    {
        Sliders.SetActive(false);
        InfiltrationScreen.SetActive(false);
        GameOverScreen.SetActive(true);
        Invoke("InTitleScene", 3);
    }

    public void SelectMainfile_1()
    {
        UserDataManager.Instance.LoadUserData();
        UserDataManager.Instance.userData.currentFile = 1;
        UserDataManager.Instance.SaveUserData();

        InInfiltrationScreen();
    }

    public void SelectMainfile_2()
    {
        UserDataManager.Instance.LoadUserData();
        UserDataManager.Instance.userData.currentFile = 2;
        UserDataManager.Instance.SaveUserData();

        InInfiltrationScreen();
    }

    public void SelectMainfile_3()
    {
        UserDataManager.Instance.LoadUserData();
        UserDataManager.Instance.userData.currentFile = 3;
        UserDataManager.Instance.SaveUserData();

        InInfiltrationScreen();
    }
}
