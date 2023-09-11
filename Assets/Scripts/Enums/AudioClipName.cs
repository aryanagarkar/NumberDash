/// <summary>
/// Represents the audio clips used in the game.
/// </summary>

namespace HelperEnums
{
    public enum AudioClipName
    {
        None,        // No audio clip.
        PlayerLost,  // Audio for when the player loses.
        PlayerWon,   // Audio for when the player wins.
        Tied,        // Audio for a tie game outcome.
        TilePlaced,  // Audio for tile placement.
        ButtonClick, // Audio for button clicks.
        Swoosh,      // 'Swoosh' sound effect
        Alarm        // Alarm sound effect.
    }
}