using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    GameObject Bag;

    Slider z_Doubt;
    Slider x_Doubt;

    public float z_val;
    public float x_val;

    void Start()
    {
        Bag = GameObject.Find("OpenBag");

        z_Doubt = GameObject.Find("z_Doubt").GetComponent<Slider>();
        x_Doubt = GameObject.Find("x_Doubt").GetComponent<Slider>();
        z_val = z_Doubt.value;
        x_val = x_Doubt.value;
    }

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    public void testClick()
    {
        z_val -= 0.1f;
        CheckDoubt(z_Doubt, z_Doubt.value - 0.1f);
    }

    public void CheckDoubt(Slider Doubt, float Val)
    {
        Doubt.value = Val;
    }

    public void OpenBag()
    {
        
    }
}