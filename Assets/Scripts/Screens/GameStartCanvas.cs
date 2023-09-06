using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartCanvas : MonoBehaviour
{
   public void play(){
      ScreenManager.GetInstance().InstantiateScreen(UIElementName.TypeOfGamePanel, transform);
   }

   public void goToOptionsMenu(){
       ScreenManager.GetInstance().InstantiateScreen(UIElementName.Settings, transform);
   }

   public void goTohowToPage(){
       ScreenManager.GetInstance().InstantiateScreen(UIElementName.InstructionsPage, transform);
   }

   public void Quit(){
      Application.Quit();
   }

   public void goToUrl(string url){
      Application.OpenURL(url);
   }
}

