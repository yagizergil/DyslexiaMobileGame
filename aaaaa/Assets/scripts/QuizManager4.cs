using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class QuizManager4 : MonoBehaviour
{
    public static QuizManager4 instance; //Instance to make is available in other scripts without reference

    [SerializeField] private GameObject gameComplete;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataScriptable questionDataScriptable;
    [SerializeField] private Image questionImage;           
    [SerializeField] private WordData[] optionsWordList;  
    [SerializeField] private Button[] buttonList;  
    [SerializeField] private Text levelText; 
    [SerializeField] private Text DogruText;
    [SerializeField] private Text YanlisText;
    public static int correctCount;
    public static int wrongCount;
    public static int levelCount = 1;
    private GameStatus gameStatus = GameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[12];    
    private int[] shuffleList = new int[3];            //array which store char of each options               //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;   
    int rand;                

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
        SetQuestion();                                  //set question
    }

    void SetQuestion()
    {
        gameStatus = GameStatus.Playing; //set GameStatus to playing 
        levelCount++;                   

        //set the answerWord string variable
        answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
        //set the image of question
        questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;
           
        ResetQuestion();                               //reset the answers and options value to orignal     
        levelText.text = "Level " + levelCount;                   //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array
        rand = Random.Range(1,6);
        SetButtons(rand); 
        //add the correct char to the wordsArray
        for (int i = 0; i < rand; i++)
        {
            wordsArray[i] = answerWord[0];
        }
        
        //add the dummy char to wordsArray
        for (int j = rand; j < wordsArray.Length; j++)
        {
            wordsArray[j] = answerWord[1];
        }

        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array
        //set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }
    }
    private void SetButtons(int k) {
       shuffleList = new int[]{k-1,k,k+1};
       shuffleList = ShuffleList.ShuffleListItems<int>(shuffleList.ToList()).ToArray();
       for(int i=0; i<buttonList.Length; i++){
            buttonList[i].transform.GetChild(0).GetComponent<Text>().text=shuffleList[i].ToString();
       }  
    }
    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
 
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
        }
        
        currentAnswerIndex = 0;
    }

    /*{
        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        for(int i=0; i<answerWord.Length; i++){
           if(answerWordList[i].wordValue.ToString() != "_"){
                continue;
           }
            currentAnswerIndex= i;
            break;
        }
        answerWordList[currentAnswerIndex].SetWord(value.wordValue);  //set the answer word list
        
         for(int i=0; i<answerWord.Length; i++){
           if(answerWordList[i].wordValue.ToString() == "_"){
                return;
           }
        }
        
            correctAnswer = true;   //default value
            //loop through answerWordList
            for (int i = 0; i < answerWord.Length; i++)
            {
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                {
                    Debug.Log(char.ToUpper(answerWord[i])+", "+ char.ToUpper(answerWordList[i].wordValue));
                    correctAnswer = false; //set it false
                    break; //and break from the loop
                }
            }

            //if correctAnswer is true
            if (correctAnswer)
            {
                correctCount++;
                levelCount++;
                Debug.Log("Correct Answer");
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
            else{
                Invoke("SetQuestion", 0.5f); //go to next question
            }

        
    }
    */
    public void IsTrue(int index){
        if (buttonList[index].transform.GetChild(0).GetComponent<Text>().text == rand.ToString()){
                correctCount++;
                levelCount++;
                Debug.Log("Correct Answer");
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
        else{
                levelCount++;
                Debug.Log("Wrong Answer");
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
