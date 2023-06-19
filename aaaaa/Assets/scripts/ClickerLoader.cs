using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClickerLoader : MonoBehaviour
{
private int clickCount = 0;

  public void OnButtonClick()
    {
        clickCount++;
        
        if (clickCount >= 9)
        {
            LoadScene("controlPanel");
        }
    }

    public void LoadScene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }
}
