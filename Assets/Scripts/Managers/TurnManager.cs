using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static Player currentPlayer;
   // GameObject turnText;
   /// Animator turnTextAnimator;
    //TextMeshProUGUI textComponent;
    Image characterImage;
    Image playerImage;
    Animator characterAnimator;
    Animator playerAnimator;
    //Timer turnTextdelay;
    //Timer displayTurnText;

    Sprite character;
    string characterName;

    public Player CurrentPlayer {
        get {return currentPlayer;}
    }

    void Start(){
        //turnText = GameObject.FindWithTag("TurnText");
        characterImage = GameObject.FindWithTag("Character").GetComponent<Image>();
        playerImage = GameObject.FindWithTag("Player").GetComponent<Image>();
        characterAnimator = GameObject.FindWithTag("Character").GetComponent<Animator>();
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        //turnTextAnimator = turnText.GetComponent<Animator>();
       /* turnTextdelay = gameObject.AddComponent<Timer>();
        turnTextdelay.Duration = 1;
        displayTurnText = gameObject.AddComponent<Timer>();
        displayTurnText.Duration = 1;
        */

        if(Character.CharacterToPlayWith != null){
            character = Character.CharacterToPlayWith;
        }

        if(Character.CharacterName != null){
            characterName = Character.CharacterName;
        }

        currentPlayer = Player.You;

        //textComponent = turnText.GetComponent<TextMeshProUGUI>();
       // textComponent.enabled = true;
     //   turnTextAnimator.Play("TurnText", -1, 0f);
        //textComponent.text = "Your Turn";
        //displayTurnText.Run();
        characterImage.enabled = true;
        characterImage.sprite = character;
        playerImage.enabled = true;
        playerAnimator.enabled = true;
        playerAnimator.Play("character", 0, 0f);
    }


   /* public void Update(){
         if (turnTextdelay.Finished)
        {
            ChangeText(currentPlayer);
            turnTextdelay.Stop();
            displayTurnText.Run();
        }
        if (displayTurnText.Finished)
        {
            textComponent.enabled = false;
            displayTurnText.Stop();
        }
    } */

    public void finishGame() {
        playerAnimator.enabled = false;
        characterAnimator.enabled = false;
    }

    public void ChangeTurn(){
        if (currentPlayer == Player.Computer) {
            currentPlayer = Player.You;
          //  turnTextdelay.Run();
            characterAnimator.enabled = false;
            playerAnimator.enabled = true;
            playerAnimator.Play("character", 0, 0f);
            
        }else {
            currentPlayer = Player.Computer;
            //turnTextdelay.Run();
            characterAnimator.Play("character", 0, 0f);
            playerAnimator.enabled = false;
            characterAnimator.enabled = true;
            //characterImage.enabled = true;
            //characterImage.sprite = character;
            GetComponent<GameBoard>().PlayComputersTurn();
        }
    }

    /*private void ChangeText(Player name){
        //textComponent.enabled = true;
        string text = "";
        switch (name)
        {
            case Player.You :
            text = "Your Turn";
            break;
            case Player.Computer :
            text = characterName + "'s Turn";
            break;
        }
        textComponent = GameObject.FindWithTag("TurnText").GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
    }*/
}
