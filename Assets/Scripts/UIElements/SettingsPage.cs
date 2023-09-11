using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Players;

namespace UIElements
{
   /// <summary>
   /// Manages the settings page's functionalities.
   /// Allows user to adjust their settings for the game session.
   /// </summary>

   public class SettingsPage : MonoBehaviour
   {
      [SerializeField]
      Slider volumeSlider;  // Slider to control the volume level.

      [SerializeField]
      TextMeshProUGUI volumeValueText;  // Text display of the current volume value.

      [SerializeField]
      GameObject avatarPanel;  // The main panel for user avatar selection.

      // Panel containing individual avatar images.
      GameObject avatarImagesPanel;

      /// <summary>
      /// Initializes the avatar selection and volume settings on start.
      /// </summary>

      public void Start()
      {
         avatarImagesPanel = avatarPanel.transform.GetChild(1).gameObject;
         SetSelectedAvatar();
         SetVolumeSlider();
      }

      /// <summary>
      /// Updates the displayed volume value as the slider is moved.
      /// </summary>

      public void OnSliderMove()
      {
         volumeValueText.text = (volumeSlider.value * 100).ToString("0.0") + "%";
      }

      /// <summary>
      /// Sets the initial value and display of the volume slider based on saved preferences.
      /// Sets to default if saved preferences are not found.
      /// </summary>

      public void SetVolumeSlider()
      {
         if (PlayerPrefs.HasKey("VolumeValue"))
         {
            float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
            updateVolumeSlider(volumeValue, (volumeValue * 100).ToString("0.0") + "%");
         }
         else
         {
            updateVolumeSlider(0.4f, "40%");
         }
      }

      /// <summary>
      /// Sets the initial avatar selection based on saved preferences highlights it.
      /// </summary>

      public void SetSelectedAvatar()
      {
         string avatarName = "PB";
         if (PlayerPrefs.HasKey("Avatar"))
         {
            avatarName = PlayerPrefs.GetString("Avatar");
         }

         foreach (Transform child in avatarImagesPanel.transform)
         {
            if (child.gameObject.name == avatarName)
            {
               child.GetComponent<Player>().ChangeSelection(true);
            }
            else
            {
               child.GetComponent<Player>().ChangeSelection(false);
            }
         }
      }

      /// <summary>
      /// Updates the avatar selection state when a new avatar is chosen.
      /// </summary>
      /// <param name="name">The name of the selected avatar.</param>

      public void PlayerAvatarSelected(string name)
      {
         foreach (Transform child in avatarImagesPanel.transform)
         {
            if (child.gameObject.name != name)
            {
               child.GetComponent<Player>().ChangeSelection(false);
            }
         }
      }

      /// <summary>
      /// On leaving the settings page by pressing the back button,
      /// save the volume setting and adjust the global audio volume.
      /// </summary>

      public void BackButtonClicked()
      {
         float volumeValue = volumeSlider.value;
         PlayerPrefs.SetFloat("VolumeValue", volumeValue);
         AudioListener.volume = volumeValue;
      }

      /// <summary>
      /// Private helper method to update the volume slider's value and displayed text.
      /// </summary>
      /// <param name="value">The volume value to set the slider to.</param>
      /// <param name="text">The current volume level text to display.</param>

      private void updateVolumeSlider(float value, string text)
      {
         volumeSlider.value = value;
         volumeValueText.text = text;
      }
   }
}