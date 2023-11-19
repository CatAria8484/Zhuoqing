using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
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
        SceneManager.LoadScene("StoryLine");
    }

    public void InInfiltration()
    {
        SceneManager.LoadScene("Infiltration");
    }
}
