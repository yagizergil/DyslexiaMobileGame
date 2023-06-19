using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SoruEkle : MonoBehaviour
{

    [SerializeField] private Text _soruText;
    [SerializeField] private Text sorulariGor;
    [SerializeField] private QuizDataScriptable[] quizDataObject;
    [SerializeField] private Toggle toggle;
    public void ButtonListener(string buttonName){
        switch (buttonName) {
            case "KelimeBulma":
                KelimeBulma();
                break;
            case "HarfTamamlama":
                HarfTamamlama();
                break;
            case "HarfKaristirma":
                HarfKaristirma();
                break;
            case "HarfSayisi":
                HarfSayisi();
                break;
            default:
                break;   
        }  

    }
    private void KelimeBulma(){
        if(String.IsNullOrWhiteSpace(_soruText.text)){
            SorulariGor(0);
            return;
        }    
        if(toggle.isOn){
            DeleteTexts(0);
            return;
        }
        QuestionData qd = new QuestionData();
        qd.answer=_soruText.text;
        quizDataObject[0].questions.Add(qd); 

    }
    private void HarfTamamlama(){
        if(String.IsNullOrWhiteSpace(_soruText.text)){
            SorulariGor(1);
            return;
        }    
        if(toggle.isOn){
            DeleteTexts(1);
            return;
        }
        QuestionData qd = new QuestionData();
        qd.answer=_soruText.text;
        quizDataObject[1].questions.Add(qd); 

    }
    private void HarfKaristirma(){
        if(String.IsNullOrWhiteSpace(_soruText.text)){
            SorulariGor(2);
            return;
         }
        if(toggle.isOn){
            DeleteTexts(2);
            return;
        }
        QuestionData qd = new QuestionData();
        qd.answer=_soruText.text;
        quizDataObject[2].questions.Add(qd); 

    }
    private void HarfSayisi(){
        if(String.IsNullOrWhiteSpace(_soruText.text)){
            SorulariGor(3);
            return;
        }    
        if(toggle.isOn){
            DeleteTexts(3);
            return;
        }
        QuestionData qd = new QuestionData();
        qd.answer=_soruText.text;
        quizDataObject[3].questions.Add(qd); 

        
    }
    private void DeleteTexts(int sayi){
        foreach(var qd in quizDataObject[sayi].questions){
            if(qd.answer==_soruText.text){
                quizDataObject[sayi].questions.Remove(qd);
            }
        }

    }
    private void SorulariGor(int sayi){
        sorulariGor.text="";
        foreach(var qd in quizDataObject[sayi].questions){
            sorulariGor.text+=qd.answer;
            sorulariGor.text+="\n";
        }
    }
}
