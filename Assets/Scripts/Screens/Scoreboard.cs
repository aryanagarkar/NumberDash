using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    TextMeshProUGUI you;
    TextMeshProUGUI yourScoreText;
    static int yourScore = 0;

    TextMeshProUGUI computer;
    TextMeshProUGUI computerScoreText;
    static int computerScore = 0;

    void Awake(){
      you = transform.Find("You").gameObject.GetComponent<TextMeshProUGUI>();
      you.text = "You:";
      yourScoreText = transform.Find("You").GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

      computer = transform.Find("Computer").gameObject.GetComponent<TextMeshProUGUI>();
      computer.text = Opponent.CharacterName + ":";
      computerScoreText =  transform.Find("Computer").GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(); 

      yourScoreText.text = yourScore.ToString();
      computerScoreText.text = computerScore.ToString();
    }

    public void ComputerWon(){
        computerScore += 1;
        computerScoreText.text = computerScore.ToString();
    }

    public void PlayerWon(){
        yourScore += 1;
        yourScoreText.text = yourScore.ToString();
    }

    public void GameTied(){
        yourScore += 1;
        computerScore += 1;
        yourScoreText.text = yourScore.ToString();
        computerScoreText.text = computerScore.ToString();
    }

    public void SetScoreMessage(){
        GameObject message =  GameObject.FindWithTag("ScoreMessage");
        TextMeshProUGUI messageText = message.transform.Find("Message").gameObject.GetComponent<TextMeshProUGUI>();
        if(computerScore > yourScore){
            messageText.text = Opponent.CharacterName + " is winning!\nAre you sure you want to quit?";
        }
        if(yourScore > computerScore){
            messageText.text = "You are winning!\nAre you sure you want to quit?";
        }
        if(yourScore == computerScore){
            messageText.text = "You are tied!\nAre you sure you want to quit?";
        }
    }

    public static void ResetScores(){
        yourScore = 0;
        computerScore = 0;
    }
}
