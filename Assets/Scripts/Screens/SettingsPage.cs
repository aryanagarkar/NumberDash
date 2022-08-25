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

   public void Start(){
      SetVolumeSliderToLastSavedValue();
   }

   public void UpdateVolumeValueText(){
    volumeValueText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
   }

   public void SetVolumeSliderToLastSavedValue(){
      if(PlayerPrefs.HasKey("VolumeValue")){
         float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
         volumeSlider.value = volumeValue;
         volumeValueText.text = (volumeValue * 100).ToString("0.0") + "%";
      }
      else{
         volumeSlider.value = 0;
         volumeValueText.text = "0.0%";
      }
   }

   public void BackButtonClicked(){
      float volumeValue = volumeSlider.value;
      PlayerPrefs.SetFloat("VolumeValue", volumeValue);
      AudioListener.volume = volumeValue;
   }
}
