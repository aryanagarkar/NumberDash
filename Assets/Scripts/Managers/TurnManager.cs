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
    GameObject clock;
    Animator clockAnim;
    Animator characterAnimator;
    
    
    GameObject youPlayText;
    GameObject pickTileText;
    GameObject dropTileText;
    GameObject pointAtcomputerText;
    GameObject pickTileAgainText;
    GameObject dropTileStrategyText;

    bool runTutorial = false;

    Sprite computerSprite;
    string computerName;

    Sprite playerSprite;

    public PlayerStatus CurrentPlayer {
        get {return currentPlayer;}
    }

    void Awake(){
        // Objects for Tutorial
        pickTileText = transform.parent.Find("PickTile").gameObject;
        dropTileText = transform.parent.Find("DropTile").gameObject;
        pointAtcomputerText = transform.parent.Find("PointAtComputer").gameObject;
        pickTileAgainText = transform.parent.Find("PickTileAgain").gameObject;
        dropTileStrategyText = transform.parent.Find("DropTileStrategy").gameObject;

        // Objects needed for animation to display the initial turn
        turnText = transform.parent.Find("TurnText").gameObject;
        textComponent = turnText.GetComponent<TextMeshProUGUI>();
        turnTextAnimator = turnText.GetComponent<Animator>();

        // Objects needed for animations during play for both players (Player and Computer)
        character = GameObject.FindWithTag("Character");
        characterImage = character.GetComponent<Image>();
        characterAnimator = character.GetComponent<Animator>();
        characterText = character.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        // Objects to animate on top of characters (Clock on top of computer and "You Play" text on top of player)
        clock = character.transform.Find("Clock").gameObject;
        clockAnim = clock.transform.GetChild(0).gameObject.GetComponent<Animator>();

        // Setting sprites for player and computer
        computerSprite = Opponent.CharacterToPlayWith;
        computerName = Opponent.CharacterName;
        
        if(Player.PlayerCharacter == null && !PlayerPrefs.HasKey("Avatar")){
            playerSprite = AssetLoader.GetInstance().DefaultPlayer;
        }
        else
        {
            playerSprite = AssetLoader.GetInstance().GetCharacterSpriteByName(PlayerPrefs.GetString("Avatar"));      
        }

        // Enabling the visuals for playing character
        characterImage.enabled = true;  
    }

    // Functions to handle a game's lifecycle

    /**
    * A function to be called by GameBoard to start the game. This function
    * picks a random first player and plays it's turn.
    * If the tutorial Mode is on, it automatically picks the 'Player' as the first
    * character to play
    */
    public void StartGame() {
        PlayTurn(true);
    }

     public void GameSetUp() {
        int currentPlayerIndex = Random.Range(1, 10) % 2;
        if(!PlayerPrefs.HasKey("PlayTutorial") || PlayerPrefs.GetInt("PlayTutorial") == 1){
            currentPlayerIndex = 1;
            runTutorial = true;
        } 
        currentPlayer = (PlayerStatus)currentPlayerIndex;
        SetCharacter();
    }

    public void EndGame() {
        PlayerPrefs.SetInt("PlayTutorial", 0);
        
        // Just loading the ads in anticipation of playing it, if the user chooses to play more
        ResetAnimations();
    }
 
    // Functions to handle turn changes
    public void ChangeTurn(){
        characterAnimator.enabled = true;
        characterAnimator.Play("FadeOut", 0, 0f);
        if (currentPlayer == PlayerStatus.Computer) {
            currentPlayer = PlayerStatus.You;
        } else {
            currentPlayer = PlayerStatus.Computer;
        }
        Invoke("PlayTurnDelay", 1);
    }

    private void SetCharacter(){
        if (currentPlayer == PlayerStatus.You) {
            ActivatePlayerSpecificUISettings(); 
        } else {
            ActivateComputerSpecificUISettings();
        }
    }

    private void PlayTurn(bool firstMoveOfTheGame){
        SetCharacter();
        if (currentPlayer == PlayerStatus.You) {
            textComponent.text = "You Play First!";  
        } else {
            textComponent.text = computerName + " Plays First";
            Invoke("PlayComputersTurnDelay", 1);
        }
        if(firstMoveOfTheGame) {
            pickTileText.SetActive(runTutorial); 
            turnText.SetActive(true);
            turnTextAnimator.enabled = true;
            turnTextAnimator.Play("TurnText", 0, 0f);
        }
    }

    private void PlayComputersTurnDelay() {
        GetComponent<GameBoard>().PlayComputersTurn();
    }
    
    private void PlayTurnDelay() {
        characterAnimator.Play("FadeIn", 0, 0f);
        PlayTurn(false);
    }

    // Functions to reset/Change the visuals

    private void ResetAnimations() {
        clockAnim.enabled = false;
    }

    private void ActivateComputerSpecificUISettings() {
        characterImage.sprite = computerSprite;
        clock.SetActive(true);
        clockAnim.enabled = true;
    }

    private void DeactivateComputerSpecificUISettings() {
        clock.SetActive(false);
        clockAnim.enabled = false;
    }

    private void ActivatePlayerSpecificUISettings() {
        DeactivateComputerSpecificUISettings();
        characterImage.sprite = playerSprite;
    }

    // Events For Running Tutorial

    public void TilePickedbyPlayer(){
        if (pickTileText.activeSelf) {
            pickTileText.SetActive(false);
            dropTileText.SetActive(true);
        } else if (pickTileAgainText.activeSelf) {
            pickTileAgainText.SetActive(false);
            dropTileStrategyText.SetActive(true);
        }
    }

    public void TileDropped(){
        if (dropTileText.activeSelf){
            dropTileText.SetActive(false);
            pointAtcomputerText.SetActive(true);
        } else if (dropTileStrategyText.activeSelf){
            dropTileStrategyText.SetActive(false);
        } else if(pointAtcomputerText.activeSelf) {
            pointAtcomputerText.SetActive(false);
            pickTileAgainText.SetActive(true);
        }
    }
}
