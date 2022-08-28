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
    Animator turnTextAnimator;
    TextMeshProUGUI textComponent;
    Image characterImage;
    TextMeshProUGUI characterText;
    //Animator characterAnimator;
    Timer initialComputerTurnDelay;
    GameObject clock;
    Animator clockAnim;
    GameObject youPlayText;

    Sprite characterSprite;
    string characterName;

    Sprite playerSprite;

    public PlayerStatus CurrentPlayer {
        get {return currentPlayer;}
    }

    void Awake(){
        turnText = GameObject.FindWithTag("TurnText");
        character = GameObject.FindWithTag("Character");
        characterImage = character.GetComponent<Image>();
        //characterAnimator = character.GetComponent<Animator>();
        turnTextAnimator = turnText.GetComponent<Animator>();
        characterText = character.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        clock = character.transform.GetChild(1).gameObject;
        youPlayText = character.transform.GetChild(2).gameObject;
        clockAnim = clock.transform.GetChild(0).gameObject.GetComponent<Animator>();
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

        characterImage.enabled = true;

        textComponent = turnText.GetComponent<TextMeshProUGUI>();
        turnTextAnimator.enabled = true;
        if (currentPlayer == PlayerStatus.You) {
            youPlayText.SetActive(true);
            textComponent.text = "You Play First!";
            //characterText.text = playerSprite.name;
            characterImage.sprite = playerSprite;
        } else {
            clock.SetActive(true);
            clockAnim.enabled = true;
            clockAnim.Play("Clock", 0, 0f);
            textComponent.text = characterName + " Plays First";
            //characterText.text = characterName;
            characterImage.sprite = characterSprite;
        }
        turnTextAnimator.Play("TurnText", 0, 0f);
        
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
        //characterAnimator.enabled = false;
        //character.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    private void PlayTurn(){
         if (currentPlayer == PlayerStatus.You) {
            youPlayText.SetActive(true);
            clock.SetActive(false);
            clockAnim.enabled = false;
            //characterAnimator.enabled = true;
            //characterAnimator.Play("character", 0, 0f);
            characterImage.sprite = playerSprite;
            //characterText.text = "You";
            
        }else {
            //characterAnimator.Play("character", 0, 0f);
            youPlayText.SetActive(false);
            clock.SetActive(true);
            clockAnim.enabled = true;
            clockAnim.Play("Clock", 0, 0f);
            characterImage.sprite = characterSprite;
            //characterText.text = characterName;
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
