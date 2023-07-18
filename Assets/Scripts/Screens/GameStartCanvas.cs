using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartCanvas : MonoBehaviour
{
   public void play(){
      ScreenManager.GetInstance().InstantiateScreen(CanvasName.TypeOfGamePanel, transform);
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

   public void goToUrl(string url){
      Application.OpenURL(url);
   }
}

