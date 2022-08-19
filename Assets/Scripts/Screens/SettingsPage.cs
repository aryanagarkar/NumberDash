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

   public void UpdateVolumeValueText(){
    volumeValueText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
   }

   public void BackButtonClicked(){
      float volumeValue = volumeSlider.value;
      PlayerPrefs.SetFloat("VolumeValue", volumeValue);
      AudioListener.volume = volumeValue;
   }
}
