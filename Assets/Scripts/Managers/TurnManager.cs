using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static Player currentPlayer;
    GameObject turnText;
    Animator turnTextAnimator;
    TextMeshProUGUI textComponent;
    Image characterImage;
    TextMeshProUGUI characterText;
    Image playerImage;
    Animator characterAnimator;
    Animator playerAnimator;
    Timer initialComputerTurnDelay;

    Sprite character;
    string characterName;

    public Player CurrentPlayer {
        get {return currentPlayer;}
    }

    void Awake(){
        turnText = GameObject.FindWithTag("TurnText");
        characterImage = GameObject.FindWithTag("Character").GetComponent<Image>();
        playerImage = GameObject.FindWithTag("Player").GetComponent<Image>();
        characterAnimator = GameObject.FindWithTag("Character").GetComponent<Animator>();
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        turnTextAnimator = turnText.GetComponent<Animator>();
        characterText = GameObject.FindWithTag("Character").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        initialComputerTurnDelay = gameObject.AddComponent<Timer>();
        initialComputerTurnDelay.Duration = 2;

        if(Character.CharacterToPlayWith != null){
            character = Character.CharacterToPlayWith;
        }

        if(Character.CharacterName != null){
            characterName = Character.CharacterName;
        }

        int currentPlayerIndex = Random.Range(1, 10) % 2;
        currentPlayer = (Player)currentPlayerIndex;

        textComponent = turnText.GetComponent<TextMeshProUGUI>();
        turnTextAnimator.enabled = true;
        if (currentPlayer == Player.You) {
            textComponent.text = "You Play First!";
        } else {
            textComponent.text = characterName + " Plays First";
        }
        turnTextAnimator.Play("TurnText", 0, 0f);
        
        characterImage.enabled = true;
        characterText.text = characterName;
        characterImage.sprite = character;
        playerImage.enabled = true;
        initialComputerTurnDelay.Run();
    }


   public void Update(){
         if (initialComputerTurnDelay.Finished)
        {
            PlayTurn();
            initialComputerTurnDelay.Stop();
        }
    } 

    public void finishGame() {
        playerAnimator.enabled = false;
        characterAnimator.enabled = false;
    }

    private void PlayTurn(){
         if (currentPlayer == Player.You) {
            characterAnimator.enabled = false;
            playerAnimator.enabled = true;
            playerAnimator.Play("character", 0, 0f);
            
        }else {
            characterAnimator.Play("character", 0, 0f);
            playerAnimator.enabled = false;
            characterAnimator.enabled = true;
            GetComponent<GameBoard>().PlayComputersTurn();
        }
    }

    public void ChangeTurn(){
        if (currentPlayer == Player.Computer) {
            currentPlayer = Player.You;
        }else {
            currentPlayer = Player.Computer;
        }
        PlayTurn();
    }
}
