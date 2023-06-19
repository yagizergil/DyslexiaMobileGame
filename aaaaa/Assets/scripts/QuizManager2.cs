using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class QuizManager2 : MonoBehaviour
{
    public static QuizManager2 instance; //Instance to make is available in other scripts without reference

    [SerializeField] private GameObject gameComplete;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataScriptable questionDataScriptable;
    [SerializeField] private Image questionImage;           //image element to show the image
    [SerializeField] private GameObject[] optionsWordList;   //list of options word in the game
    [SerializeField] private Text levelText; 
    [SerializeField] private Text DogruText;
    [SerializeField] private Text YanlisText;
    private static int count;
    public static int levelCount=1;
    private GameStatus gameStatus = GameStatus.Playing;     
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;                 
    private string answerWordOld;  
    private string answerWord;                             
    public static int correctCount;
    public static int wrongCount;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetQuestion();
    }

    void SetQuestion()
    {
        gameStatus = GameStatus.Playing; //set GameStatus to playing 
        levelCount++;                

        //set the answerWord string variable
        answerWordOld = questionDataScriptable.questions[currentQuestionIndex].answer;
        answerWord = "";
         for (int i = 0; i < answerWordOld.Length; i++)
        {
            answerWord += char.ToUpper(answerWordOld[i]);
        }
        questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;
        levelText.text = "Level " + levelCount;
        count = Random.Range(0,4);
        for(int i=0; i<4; i++){
            if(i==count){
            optionsWordList[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = answerWord;
            }
            else{
             optionsWordList[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = Shuffle(answerWord);
            }
        }
    }

    public string Shuffle(string str)
    {
        char[] array = str.ToCharArray();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(string value)
    {
        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;
        Debug.Log($"Gelen Değer: {value} - Bizim Count: {count} ");
        if(value==count.ToString()){
            correctCount++;
        }
        gameStatus = GameStatus.Next; //set the game status
            currentQuestionIndex++; //increase currentQuestionIndex

                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.5f); //go to next question
                }
                else
                {
                    Debug.Log($"Doğru Sayısı: {correctCount}");

                    DogruText.text=$"DOĞRU SAYISI: {correctCount}";
                    YanlisText.text =$"YANLIŞ SAYISI: {questionDataScriptable.questions.Count-correctCount}";
                    Debug.Log("Game Complete"); //else game is complete
                    gameComplete.SetActive(true);
                }
        
    }

    public void NextQuestion(){
            gameStatus = GameStatus.Next; //set the game status
            currentQuestionIndex++; //increase currentQuestionIndex

                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.5f); //go to next question
                }
                else
                {
                    DogruText.text=$"DOĞRU SAYISI: {correctCount}";
                    YanlisText.text =$"YANLIŞ SAYISI: {questionDataScriptable.questions.Count-correctCount}";
                    Debug.Log("Game Complete"); //else game is complete
                    gameComplete.SetActive(true);
                }
    }
}
