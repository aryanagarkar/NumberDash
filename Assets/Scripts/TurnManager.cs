using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class TurnManager : MonoBehaviour
{
    public static Player currentPlayer;
    GameObject gameBoard;
    TextMeshProUGUI textComponent;
    Image characterImage;

    Sprite character;

    public Player CurrentPlayer {
        get {return currentPlayer;}
    }

    void Start(){
        characterImage = GameObject.FindWithTag("Character").GetComponent<Image>();

        if(Character.CharacterToPlayWith != null){
            character = Character.CharacterToPlayWith;
        }

        currentPlayer = Player.You;
        gameBoard = GameObject.FindWithTag("GameBoard");
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = currentPlayer.ToString();
    }

    public void finishGame() {
        textComponent.text = "";
    }

    public void ChangeTurn(){
        if (currentPlayer == Player.Computer) {
            currentPlayer = Player.You;
            textComponent.text = currentPlayer.ToString();
            characterImage.enabled = false;
        }else {
            currentPlayer = Player.Computer;
            textComponent.text = currentPlayer.ToString();
                characterImage.enabled = true;
            characterImage.sprite = character;
            gameBoard.GetComponent<GameBoard>().PlayComputersTurn();
        }
    }
}
