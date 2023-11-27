using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    GameObject Sliders;
    GameObject Mainscreen;
    GameObject Mainflie;
    GameObject Infiltration;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Sliders = GameObject.Find("Sliders");
            Mainscreen = GameObject.Find("MainScreen");
            Mainflie = GameObject.Find("MainFileScreen");
            Infiltration = GameObject.Find("InfiltrationScreen");

            Sliders.transform.GetChild(0).gameObject.SetActive(true);
            Mainscreen.transform.GetChild(0).gameObject.SetActive(true);
            Mainflie.transform.GetChild(0).gameObject.SetActive(false);
            Infiltration.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void InTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void InMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void InStoryLine()
    {
        Mainscreen.transform.GetChild(0).gameObject.SetActive(false);
        Mainflie.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OutStoryLine()
    {
        Mainflie.transform.GetChild(0).gameObject.SetActive(false);
        Mainscreen.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void InInfiltration()
    {
        Sliders.transform.GetChild(0).gameObject.SetActive(false);
        Mainscreen.transform.GetChild(0).gameObject.SetActive(false);
        Infiltration.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OutInfiltration()
    {
        Infiltration.transform.GetChild(0).gameObject.SetActive(false);
        Mainscreen.transform.GetChild(0).gameObject.SetActive(true);
        Sliders.transform.GetChild(0).gameObject.SetActive(true);
    }
}
