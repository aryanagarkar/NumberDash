using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartCanvas : MonoBehaviour
{
   public static Level level;

   public void Awake(){
      LoadPlayerPrefValues();
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
      ScreenManager.GetInstance().InstantiateScreen(CanvasName.LevelPanel, transform);
   }

   public void goToMainGame(){
    ScreenManager.GetInstance().GoToScene(SceneName.MainGameBoard);
   }

   public void goToOptionsMenu(){
       ScreenManager.GetInstance().InstantiateScreen(CanvasName.Settings, transform);
   }

   public void goTohowToPage(){
       ScreenManager.GetInstance().InstantiateScreen(CanvasName.InstructionsPage, transform);
   }

   public void Quit(){
      Application.Quit();
   }

   public void EasyButtonClicked(){
      level = Level.Easy;
       ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, transform);
   }

   public void MediumButtonClicked(){
      level = Level.Medium;
      ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, transform);
   }

   public void goToUrl(string url){
      Application.OpenURL(url);
   }
}

