using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SplashController : MonoBehaviour {
    public float splashTime = 3.0f;
    public string nextSceneName = "Home";

    void Start() {
        Invoke("LoadNextScene", splashTime);
    }

    void LoadNextScene() {
        SceneManager.LoadScene(nextSceneName);
    }
}
