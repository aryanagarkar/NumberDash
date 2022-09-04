using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    TextMeshProUGUI yourScoreText;
    static float yourScore = 0;

    TextMeshProUGUI computerScoreText;
    static float computerScore = 0;

    void Awake(){
      yourScoreText = transform.Find("You").GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
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
        yourScore += 0.5f;
        computerScore += 0.5f;
        yourScoreText.text = yourScore.ToString();
        computerScoreText.text = computerScore.ToString();
    }
}
