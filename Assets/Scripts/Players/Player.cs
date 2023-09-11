using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Managers;
using Sound;
using UIElements;
using HelperEnums;

namespace Players
{
    /// <summary>
    /// Represents the user (player) in the game. 
    /// Allows the selection of the character for this player for gameplay.
    /// </summary>

    public class Player : MonoBehaviour, IPointerClickHandler
    {
        static Sprite playerCharacter;   // The sprite representing the user's character selected for gameplay.
        bool selected = false;           // Indicates whether this character is selected.

        /// <summary>
        /// Property to get the currently selected user's character sprite.
        /// </summary>

        public static Sprite PlayerCharacter
        {
            get
            {
                return playerCharacter;
            }
        }

        void Awake()
        {
            playerCharacter = LoadPlayerCharacter();
        }

        /// <summary>
        /// Loads the player character from saved preferences.
        /// If there is no saved preference, the character is set to a default.
        /// </summary>
        /// <returns>Sprite representing the user's character.</returns>

        private static Sprite LoadPlayerCharacter()
        {
            //No saved preference.
            if (!PlayerPrefs.HasKey("Avatar"))
            {
                PlayerPrefs.SetString("Avatar", "PB");
                return AssetLoader.GetInstance().DefaultPlayer;
            }

            //There is a saved preference.
            return AssetLoader.GetInstance().GetCharacterSpriteByName(PlayerPrefs.GetString("Avatar"));
        }

        /// <summary>
        /// Checks if this character is selected.
        /// </summary>
        /// <returns>True if selected, otherwise false.</returns>

        public bool isSelected()
        {
            return selected;
        }

        /// <summary>
        /// Changes the selection status of this character.
        /// Sets the appropriate image based on the selection status.
        /// </summary>
        /// <param name="select">True if selected, false if deselected.</param>

        public void ChangeSelection(bool select)
        {
            this.selected = select;
            SetImage();
        }

        /// <summary>
        /// Sets the appropriate image for this character based on its selection status.
        /// <returns>sprite associated with this character's name</returns>
        /// </summary>

        private void SetImage()
        {
            //Get the current sprite name of this character's image.
            string imageName = name;
            if (selected)
            {
                //New sprite name for selected image.
                imageName = name + "Selected";
                //Update the stored name of user's character.
                PlayerPrefs.SetString("Avatar", name);

                playerCharacter = AssetLoader.GetInstance().GetCharacterSpriteByName(PlayerPrefs.GetString("Avatar"));

                //Update this character's selection state.
                GameObject.FindWithTag("Settings").GetComponent<SettingsPage>().PlayerAvatarSelected(name);
            }
            else
            {
                //New sprite name for deselected image.
                imageName = name + "Circle";
            }

            GetComponent<Image>().sprite = AssetLoader.GetInstance().GetCharacterSpriteByName(imageName);
        }

        /// <summary>
        /// Detects when this character is clicked as the user's player. Changes selection if not already selected.
        /// </summary>
        /// <param name="eventData">Data associated with the pointer event.</param>

        public void OnPointerClick(PointerEventData eventData)
        {
            if (selected)
            {
                return;
            }

            SoundManager.PlayClipByName(AudioClipName.ButtonClick);
            ChangeSelection(true);
        }
    }
}