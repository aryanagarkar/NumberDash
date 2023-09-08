using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the turns and gameplay visuals for both the computer and the player.
/// It handles the initiation, transition, and completion of turns, as well as provides
/// specific UI settings and animations based on the current player's turn.
/// </summary>

public class TurnManager : MonoBehaviour
{
    public static PlayerStatus currentPlayer;

    // UI components to handle turn displays and animations.
    GameObject turnText;
    GameObject character;
    Animator turnTextAnimator;
    TextMeshProUGUI textComponent;
    Image characterImage;
    TextMeshProUGUI characterText;
    GameObject clock;
    Animator clockAnim;
    Animator characterAnimator;
    
    // UI components specific to the tutorial
    GameObject youPlayText;
    GameObject pickTileText;
    GameObject dropTileText;
    GameObject pointAtcomputerText;
    GameObject pickTileAgainText;
    GameObject dropTileStrategyText;

    //Whether to run the tutorial or not.
    bool runTutorial = false;

     // Sprites and names representing the user and the computer.
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

    /// <summary>
    /// Starts the game and plays the first turn.
    /// </summary>

    public void StartGame() {
        PlayTurn(true);
    }

    /// <summary>
    /// Sets up the game.
    /// Determines which player goes first and displays the appropriate character.
    /// </summary>

     public void GameSetUp() {
        int currentPlayerIndex = Random.Range(1, 10) % 2;
        if(!PlayerPrefs.HasKey("PlayTutorial") || PlayerPrefs.GetInt("PlayTutorial") == 1){
            currentPlayerIndex = 1;
            runTutorial = true;
        } 
        currentPlayer = (PlayerStatus)currentPlayerIndex;
        SetCharacter();
    }

    /// <summary>
    /// Ends the game and resets relevant settings.
    /// </summary>

    public void EndGame() {
        PlayerPrefs.SetInt("PlayTutorial", 0);
        ResetAnimations();
    }
 
    /// <summary>
    /// Changes the turn to the next player.
    /// </summary>

    public void ChangeTurn(){
        characterAnimator.enabled = true;
        characterAnimator.Play("FadeOut", 0, 0f);
        if (currentPlayer == PlayerStatus.Computer) {
            currentPlayer = PlayerStatus.You;
        } else {
            currentPlayer = PlayerStatus.Computer;
        }
        //Delay for animation to complete before next turn.
        Invoke("PlayTurnDelay", 1);
    }

    // Events For Running Tutorial

    /// <summary>
    /// Handles the tutorial UI when a tile is picked up by the player.
    /// Based on the current state of the tutorial, this function toggles relevant tutorial prompts.
    /// </summary>

    public void TilePickedbyPlayer(){
        if (pickTileText.activeSelf) {
            pickTileText.SetActive(false);
            dropTileText.SetActive(true);
        } else if (pickTileAgainText.activeSelf) {
            pickTileAgainText.SetActive(false);
            dropTileStrategyText.SetActive(true);
        }
    }

    /// <summary>
    /// Handles the tutorial UI changes when a tile is dropped by the player.
    /// Based on the current state of the tutorial, this function toggles relevant tutorial prompts.
    /// </summary>

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

    /// <summary>
    /// Sets the appropriate character visuals based on the current player.
    /// </summary>

    private void SetCharacter(){
        if (currentPlayer == PlayerStatus.You) {
            ActivatePlayerSpecificUISettings(); 
        } else {
            ActivateComputerSpecificUISettings();
        }
    }

    /// <summary>
    /// Executes the current player's turn.
    /// </summary>

    private void PlayTurn(bool firstMoveOfTheGame){
        SetCharacter();
        
        // Displays text for current player.
        if (currentPlayer == PlayerStatus.You) {
            textComponent.text = "You Play First!";  
        } else {
            textComponent.text = computerName + " Plays First";
            Invoke("PlayComputersTurnDelay", 1);
        }
   
        if(firstMoveOfTheGame) {
            // Sets the tutorial text active based on whether the tutorial is needed.
            pickTileText.SetActive(runTutorial); 

            //Set turnText and related components.
            turnText.SetActive(true);
            turnTextAnimator.enabled = true;
            turnTextAnimator.Play("TurnText", 0, 0f);
        }
    }

    /// <summary>
    /// Executes computer's turn after a delay.
    /// </summary>

    private void PlayComputersTurnDelay() {
        GetComponent<GameBoard>().PlayComputersTurn();
    }

    /// <summary>
    /// Executes user's turn after the character animation.
    /// </summary>

    private void PlayTurnDelay() {
        characterAnimator.Play("FadeIn", 0, 0f);
        PlayTurn(false);
    }

    /// <summary>
    /// Reset animations.
    /// </summary>

    private void ResetAnimations() {
        clockAnim.enabled = false;
    }

    /// <summary>
    /// Display UI elements for the computer.
    /// </summary>

    private void ActivateComputerSpecificUISettings() {
        characterImage.sprite = computerSprite;
        clock.SetActive(true);
        clockAnim.enabled = true;
    }

    /// <summary>
    /// Hide UI elements for the computer.
    /// </summary>

    private void DeactivateComputerSpecificUISettings() {
        clock.SetActive(false);
        clockAnim.enabled = false;
    }

    /// <summary>
    /// Display UI elements for the user.
    /// </summary>

    private void ActivatePlayerSpecificUISettings() {
        DeactivateComputerSpecificUISettings();
        characterImage.sprite = playerSprite;
    }
}
