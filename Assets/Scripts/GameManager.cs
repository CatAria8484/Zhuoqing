using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Slider z_Doubt;
    Slider x_Doubt;

    void Start()
    {
        z_Doubt = GameObject.Find("z_Doubt").GetComponent<Slider>();
        x_Doubt = GameObject.Find("x_Doubt").GetComponent<Slider>();
    }

    void Update()
    {

    }

    public void CheckDoubt()
    {

    }
}