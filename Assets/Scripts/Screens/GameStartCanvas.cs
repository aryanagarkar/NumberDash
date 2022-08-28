using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartCanvas : MonoBehaviour
{
   GameObject characterPanel;
   GameObject levelPanel;
   GameObject instructionPage;
   GameObject optionsMenu;

   public static Level level;

   public void Awake(){
      LoadPlayerPrefValues();
   }

   public void Start(){
    characterPanel = Resources.Load<GameObject>("Prefabs/Canvases/CharacterPanel");
    levelPanel = Resources.Load<GameObject>("Prefabs/Canvases/LevelPanel");
    instructionPage = Resources.Load<GameObject>("Prefabs/Canvases/InstructionsPage");
    optionsMenu = Resources.Load<GameObject>("Prefabs/Canvases/Settings");
   }

   private void LoadPlayerPrefValues(){
      if(PlayerPrefs.HasKey("VolumeValue")){
         float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
         AudioListener.volume = volumeValue;
      }
      else{
         AudioListener.volume = 1;
      }
   }

   public void play(){
    Instantiate(levelPanel, transform);
   }

   public void goToMainGame(){
    SceneManager.LoadScene(1);
   }

   public void goToOptionsMenu(){
      Instantiate(optionsMenu, transform);
   }

   public void goTohowToPage(){
      Instantiate(instructionPage, transform);
   }

   public void Quit(){
      Application.Quit();
   }

   public void EasyButtonClicked(){
      level = Level.Easy;
      Instantiate(characterPanel, transform);
   }

   public void MediumButtonClicked(){
      level = Level.Medium;
      Instantiate(characterPanel, transform);
   }

   public void goToUrl(string url){
      Application.OpenURL(url);
   }
}

