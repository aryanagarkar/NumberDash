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

    Sprite characterSprite;
    string characterName;

    Sprite playerSprite;

    public PlayerStatus CurrentPlayer {
        get {return currentPlayer;}
    }

    void Awake(){
        turnText = transform.parent.Find("TurnText").gameObject;
        pickTileText = transform.parent.Find("PickTile").gameObject;
        dropTileText = transform.parent.Find("DropTile").gameObject;
        pointAtcomputerText = transform.parent.Find("PointAtComputer").gameObject;
        pickTileAgainText = transform.parent.Find("PickTileAgain").gameObject;
        dropTileStrategyText = transform.parent.Find("DropTileStrategy").gameObject;

        character = GameObject.FindWithTag("Character");
        characterImage = character.GetComponent<Image>();
        turnTextAnimator = turnText.GetComponent<Animator>();
        characterAnimator = character.GetComponent<Animator>();
        characterText = character.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        clock = character.transform.GetChild(1).gameObject;
        youPlayText = character.transform.GetChild(2).gameObject;
        clockAnim = clock.transform.GetChild(0).gameObject.GetComponent<Animator>();
        textComponent = turnText.GetComponent<TextMeshProUGUI>();

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
        if(!PlayerPrefs.HasKey("FirstGame")){
            currentPlayerIndex = 1;
            runTutorial = true;
        } 
        currentPlayer = (PlayerStatus)currentPlayerIndex;
        PlayTurn(true);
    }

    public void TilePickedbyPlayer(){
        if (pickTileText.activeSelf) {
            pickTileText.SetActive(false);
            dropTileText.SetActive(true);
        } else if (pickTileAgainText.activeSelf) {
            pickTileAgainText.SetActive(false);
            dropTileStrategyText.SetActive(true);
        }
    }

    public void TileDroppedByPlayer(){
         if (dropTileText.activeSelf){
            dropTileText.SetActive(false);
            pointAtcomputerText.SetActive(true);
        } else if (dropTileStrategyText.activeSelf){
            dropTileStrategyText.SetActive(false);
        }
    }

    private void PlayTurn(bool firstMoveOfTheGame){
         if (currentPlayer == PlayerStatus.You) {
            ActivatePlayerSpecificUISettings();
            textComponent.text = "You Play First!";  
        } else {
            ActivateComputerSpecificUISettings();
            textComponent.text = characterName + " Plays First";
            Invoke("PlayComputersTurn", 2);
        }
        if(firstMoveOfTheGame) {
            pickTileText.SetActive(runTutorial); 
            turnText.SetActive(true);
            turnTextAnimator.enabled = true;
            turnTextAnimator.Play("TurnText", 0, 0f);
        }
    }

    /*private void playFadeInAnimation() {
        characterAnimator.Play("FadeIn", 0, 0f);
    }*/

    private void PlayComputersTurn() {
        GetComponent<GameBoard>().PlayComputersTurn();
    }
    
    private void ChangeTurnDelay() {
        characterAnimator.Play("FadeIn", 0, 0f);
        //characterImage.enabled = true;  
        if (currentPlayer == PlayerStatus.Computer) {
            if (pointAtcomputerText.activeSelf) {
                pointAtcomputerText.SetActive(false);
                pickTileAgainText.SetActive(true);
            }
            currentPlayer = PlayerStatus.You;
            
        }else {
            currentPlayer = PlayerStatus.Computer;
            
        }
        PlayTurn(false);
    }

    private void ActivateComputerSpecificUISettings() {
        DeactivatePlayerSpecificUISettings();
        characterImage.sprite = characterSprite;
        clock.SetActive(true);
        clockAnim.enabled = true;
    }

    private void DeactivateComputerSpecificUISettings() {
        clock.SetActive(false);
        clockAnim.enabled = false;
    }

    private void DeactivatePlayerSpecificUISettings() {
        youPlayText.SetActive(false);
    }

    private void ActivatePlayerSpecificUISettings() {
        DeactivateComputerSpecificUISettings();
        characterImage.sprite = playerSprite;
        youPlayText.SetActive(true);
    }

    public void ChangeTurn(){
        characterAnimator.enabled = true;
        characterAnimator.Play("FadeOut", 0, 0f);
        Invoke("ChangeTurnDelay", 1);
    }

    public void GameOver() {
        if(!PlayerPrefs.HasKey("FirstGame")) {
            PlayerPrefs.SetInt("FirstGame", 0);
        }
        GameObject.Find("PersistentObject").GetComponent<InterstitialAds>().LoadAd();
        ResetAnimations();
    }

    private void ResetAnimations() {
        clockAnim.enabled = false;
    }
}
