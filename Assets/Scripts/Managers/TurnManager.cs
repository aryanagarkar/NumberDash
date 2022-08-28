using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static PlayerStatus currentPlayer;
    GameObject turnText;
    GameObject character;
    GameObject player;
    Animator turnTextAnimator;
    TextMeshProUGUI textComponent;
    Image characterImage;
    TextMeshProUGUI characterText;
    Image playerImage;
    TextMeshProUGUI playerText;
    Animator characterAnimator;
    Animator playerAnimator;
    Timer initialComputerTurnDelay;

    Sprite characterSprite;
    string characterName;

    Sprite playerSprite;

    public PlayerStatus CurrentPlayer {
        get {return currentPlayer;}
    }

    void Awake(){
        turnText = GameObject.FindWithTag("TurnText");
        character = GameObject.FindWithTag("Character");
        player = GameObject.FindWithTag("Player");
        characterImage = character.GetComponent<Image>();
        playerImage = player.GetComponent<Image>();
        characterAnimator = character.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        turnTextAnimator = turnText.GetComponent<Animator>();
        characterText = character.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        playerText = player.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        initialComputerTurnDelay = gameObject.AddComponent<Timer>();
        initialComputerTurnDelay.Duration = 2;

        if(Opponent.CharacterToPlayWith != null){
            characterSprite = Opponent.CharacterToPlayWith;
        }

        if(Opponent.CharacterName != null){
            characterName = Opponent.CharacterName;
        }
        
        playerSprite = Player.PlayerCharacter;      

        int currentPlayerIndex = Random.Range(1, 10) % 2;
        currentPlayer = (PlayerStatus)currentPlayerIndex;

        textComponent = turnText.GetComponent<TextMeshProUGUI>();
        turnTextAnimator.enabled = true;
        if (currentPlayer == PlayerStatus.You) {
            textComponent.text = "You Play First!";
        } else {
            textComponent.text = characterName + " Plays First";
        }
        turnTextAnimator.Play("TurnText", 0, 0f);
        
        characterImage.enabled = true;
        characterText.text = characterName;
        playerText.text = "You";
        characterImage.sprite = characterSprite;
        playerImage.enabled = true;
        playerImage.sprite = playerSprite; 
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
        character.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        player.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    private void PlayTurn(){
         if (currentPlayer == PlayerStatus.You) {
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
        if (currentPlayer == PlayerStatus.Computer) {
            currentPlayer = PlayerStatus.You;
        }else {
            currentPlayer = PlayerStatus.Computer;
        }
        PlayTurn();
    }
}
