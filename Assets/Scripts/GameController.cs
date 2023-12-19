using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    GameObject Bag;

    Slider f_Doubt;
    Slider w_Doubt;

    bool isOpen = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        Bag = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject;

        f_Doubt = GameObject.Find("Sliders").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        w_Doubt = GameObject.Find("Sliders").transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Slider>();

        // load data
        UserDataManager.instance.LoadUserData();
        f_Doubt.value = UserDataManager.instance.userData.f_current_val;
        w_Doubt.value = UserDataManager.instance.userData.w_current_val;
    }

    public void f_good()
    {
        f_Doubt.value -= 0.1f;

        savedata();
    }

    public void f_bad()
    {
        f_Doubt.value += 0.1f;

        savedata();
    }

    public void w_good()
    {
        w_Doubt.value -= 0.1f;

        savedata();
    }

    public void w_bad()
    {
        w_Doubt.value += 0.1f;

        savedata();
    }

    // save data
    void savedata()
    {
        UserDataManager.instance.LoadUserData();
        UserDataManager.instance.userData.f_val = f_Doubt.value;
        UserDataManager.instance.userData.w_val = w_Doubt.value;
        UserDataManager.instance.SaveUserData();
    }

    public void OpenBag()
    {
        float x, y;
        x = Bag.transform.position.x;
        y = Bag.transform.position.y;

        if (!isOpen)    // open
        {
            Bag.transform.position = new Vector2(x - 160, y);
            isOpen = true;
        }
        else            // close
        {
            Bag.transform.position = new Vector2(x + 160, y);
            isOpen = false;
        }
    }
}