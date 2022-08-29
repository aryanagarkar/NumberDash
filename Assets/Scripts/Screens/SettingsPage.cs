using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPage : MonoBehaviour
{
   [SerializeField]
   Slider volumeSlider; 

   [SerializeField]
   TextMeshProUGUI volumeValueText; 

   [SerializeField]
   GameObject avatarPanel; 

   GameObject avatarImagesPanel;

   public void Start(){
      avatarImagesPanel = avatarPanel.transform.GetChild(1).gameObject;
      SetSelectedAvatar();
      SetVolumeSlider();
   }

   public void OnSliderMove(){
    volumeValueText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
   }

   public void SetVolumeSlider(){
      if(PlayerPrefs.HasKey("VolumeValue")){
         float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
         updateVolumeSlider(volumeValue, (volumeValue * 100).ToString("0.0") + "%");
      } else {
         updateVolumeSlider(0.4f, "40%");
      }
   }

   public void SetSelectedAvatar(){
      string avatarName = "PB";
      if(PlayerPrefs.HasKey("Avatar")){
         avatarName = PlayerPrefs.GetString("Avatar");
      }

      foreach (Transform child in avatarImagesPanel.transform) {
         if (child.gameObject.name == avatarName) {
            child.GetComponent<Player>().ChangeSelection(true);
         } else {
            child.GetComponent<Player>().ChangeSelection(false);
         }
      }
   }

   public void PlayerAvatarSelected(string name){
      foreach (Transform child in avatarImagesPanel.transform) {
         if (child.gameObject.name != name) {
            child.GetComponent<Player>().ChangeSelection(false);
         }
      }
   }

   public void BackButtonClicked(){
      float volumeValue = volumeSlider.value;
      PlayerPrefs.SetFloat("VolumeValue", volumeValue);
      AudioListener.volume = volumeValue;
   }

   private void updateVolumeSlider(float value, string text) {
      volumeSlider.value = value;
      volumeValueText.text = text;
   }
}
