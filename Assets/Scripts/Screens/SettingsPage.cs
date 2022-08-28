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

   Image avatar;


   public void Start(){ 
      SetAvatar();
      SetVolumeSlider();
   }

   public void OnSliderMove(){
    volumeValueText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
   }

   public void SetVolumeSlider(){
      if(PlayerPrefs.HasKey("VolumeValue")){
         float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
         volumeSlider.value = volumeValue;
         volumeValueText.text = (volumeValue * 100).ToString("0.0") + "%";
      } else {
         volumeSlider.value = 0;
         volumeValueText.text = "0.0%";
      }
   }

    public void SetAvatar(){
      avatar = avatarPanel.transform.GetChild(1).gameObject.GetComponent<Image>();
      string avatarName = "Mummy";
      if(PlayerPrefs.HasKey("Avatar")){
         avatarName = PlayerPrefs.GetString("Avatar");
      }
       avatar.sprite = Resources.Load<Sprite>("Sprites/" + avatarName);
   }

   public void PlayerChosen(Sprite playerChosen){
      transform.GetChild(3).gameObject.SetActive(false);
      avatarPanel.SetActive(true);
      avatar.sprite = playerChosen;
   }

   public void EditAvatarButtonClicked(){
      transform.GetChild(3).gameObject.SetActive(true);
      avatarPanel.SetActive(false);
   }

   public void BackButtonClicked(){
      PlayerPrefs.SetString("Avatar", avatar.sprite.name);
      //Debug.Log(previouslyChosenPlayer);
      float volumeValue = volumeSlider.value;
      PlayerPrefs.SetFloat("VolumeValue", volumeValue);
      AudioListener.volume = volumeValue;
   }
}
