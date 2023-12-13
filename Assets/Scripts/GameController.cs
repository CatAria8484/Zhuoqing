using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
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
        DataManager.instance.LoadUserData();
        f_Doubt.value = DataManager.instance.userData.f_val;
        w_Doubt.value = DataManager.instance.userData.w_val;
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

        // save data
        DataManager.instance.LoadUserData();
        DataManager.instance.userData.f_val = f_Doubt.value;
        DataManager.instance.userData.w_val = w_Doubt.value;
        DataManager.instance.SaveUserData();
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
}