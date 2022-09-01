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
    Animator characterAnimator;
    TextMeshProUGUI characterText;
    Timer turnDelay;
    GameObject clock;
    Animator clockAnim;
    GameObject youPlayText;
    GameObject pickTileText;
    GameObject dropTileText;

    bool firstTurnForYou = false;

    Sprite characterSprite;
    string characterName;

    Sprite playerSprite;

    public PlayerStatus CurrentPlayer {
        get {return currentPlayer;}
    }

    void Awake(){
        turnText = GameObject.FindWithTag("TurnText");
        pickTileText = transform.parent.Find("PickTile").gameObject;
        dropTileText = transform.parent.Find("DropTile").gameObject;
        character = GameObject.FindWithTag("Character");
        characterImage = character.GetComponent<Image>();
        characterAnimator = character.GetComponent<Animator>();
        turnTextAnimator = turnText.GetComponent<Animator>();
        characterText = character.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        clock = character.transform.GetChild(1).gameObject;
        youPlayText = character.transform.GetChild(2).gameObject;
        clockAnim = clock.transform.GetChild(0).gameObject.GetComponent<Animator>();
        turnDelay = gameObject.AddComponent<Timer>();
        turnDelay.Duration = 2;

        if(Opponent.CharacterToPlayWith != null){
            characterSprite = Opponent.CharacterToPlayWith;
        }

        if(Opponent.CharacterName != null){
            characterName = Opponent.CharacterName;
        }

        if(Player.PlayerCharacter == null && !PlayerPrefs.HasKey("Avatar")){
            playerSprite = AssetLoader.GetInstance().DefaultPlayer;
        }
        else
        {
            playerSprite = AssetLoader.GetInstance().GetSpriteByName(PlayerPrefs.GetString("Avatar"));      
        }
        characterImage.enabled = true;  
    }

    public void StartGame() {
        int currentPlayerIndex = Random.Range(1, 10) % 2;
        currentPlayer = (PlayerStatus)currentPlayerIndex;
        textComponent = turnText.GetComponent<TextMeshProUGUI>();
        turnTextAnimator.enabled = true;

        if (currentPlayer == PlayerStatus.You) {
            youPlayText.SetActive(true);
            textComponent.text = "You Play First!";
            characterImage.sprite = playerSprite;
            firstTurnForYou = true;
            pickTileText.SetActive(true);
        } else {
            clock.SetActive(true);
            clockAnim.enabled = true;
            clockAnim.Play("Clock", 0, 0f);
            textComponent.text = characterName + " Plays First";
            characterImage.sprite = characterSprite;
            turnDelay.Run();
        }
        turnTextAnimator.Play("TurnText", 0, 0f);
    }

    public void TilePickedbyPlayer(){
        if(firstTurnForYou == true){
            pickTileText.SetActive(false);
            dropTileText.SetActive(true);
        }
    }

    public void TileDroppedByPlayer(){
         if(firstTurnForYou == true){
            dropTileText.SetActive(false);
            firstTurnForYou = false;
        }
    }


   public void Update(){
         if (turnDelay.Finished)
        {
            PlayTurn();
            turnDelay.Stop();
        }
    } 

    private void PlayTurn(){
         if (currentPlayer == PlayerStatus.You) {
            youPlayText.SetActive(true);
            clock.SetActive(false);
            clockAnim.enabled = false;
            characterImage.sprite = playerSprite;
        } else {
            youPlayText.SetActive(false);
            clock.SetActive(true);
            characterAnimator.enabled = true;
            clockAnim.enabled = true;
            clockAnim.Play("Clock", 0, 0f);
            characterImage.sprite = characterSprite;
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

    public void GameOver() {
        ResetAnimations();
    }

    private void ResetAnimations() {
        clockAnim.enabled = false;
    }
}
