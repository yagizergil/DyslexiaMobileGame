using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{

    public void LoadScene(string sceneName)
    { 
        QuizManager.wrongCount=0;
        QuizManager2.wrongCount=0;
        QuizManager3.wrongCount=0;
        QuizManager4.wrongCount=0;

        QuizManager.correctCount=0;
        QuizManager2.correctCount=0;
        QuizManager3.correctCount=0;
        QuizManager4.correctCount=0;
        
        QuizManager.levelCount =0;
        QuizManager2.levelCount =0;
        QuizManager3.levelCount =0;
        QuizManager4.levelCount =0;
        SceneManager.LoadScene(sceneName);
    }
}

