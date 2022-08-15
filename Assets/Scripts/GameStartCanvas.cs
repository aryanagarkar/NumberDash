using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStartCanvas : MonoBehaviour
{
   GameObject characterPanel;

   public void Start(){
    characterPanel = Resources.Load<GameObject>("CharacterPanel");
   }

   public void play(){
    Instantiate(characterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
   }

   public void goToMainGame(){
    SceneManager.LoadScene(1);
   }
}

