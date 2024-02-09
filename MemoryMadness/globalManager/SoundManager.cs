using Godot;
using Godot.Collections;
using System;

public enum SoundType
{
    SOUND_MAIN_MENU,
    SOUND_IN_GAME,
    SOUND_SUCCESS,
    SOUND_GAME_OVER,
    SOUND_SELECT_TITLE,
    SOUND_SELECT_BUTTON,
}

public partial class SoundManager : Node
{
    // public static string SOUND_MAIN_MENU = "SOUND_MAIN_MENU";
    // public static string SOUND_IN_GAME = "SOUND_IN_GAME";
    // public static string SOUND_SUCCESS = "SOUND_SUCCESS";
    // public static string SOUND_GAME_OVER = "SOUND_GAME_OVER";
    // public static string SOUND_SELECT_TITLE = "title";
    // public static string SOUND_SELECT_BUTTON = "button";

    public static Dictionary<SoundType, Resource> SOUNDS = new()
    {
        { SoundType.SOUND_MAIN_MENU,  GD.Load("res://assets/sounds/bgm_action_3.mp3")},
        {SoundType.SOUND_IN_GAME,  GD.Load("res://assets/sounds/bgm_action_4.mp3")},
        {SoundType.SOUND_SUCCESS,  GD.Load("res://assets/sounds/sfx_sounds_fanfare3.wav")},
        {SoundType.SOUND_GAME_OVER,  GD.Load("res://assets/sounds/sfx_sounds_powerup12.wav")},
        {SoundType.SOUND_SELECT_TITLE,  GD.Load("res://assets/sounds/sfx_sounds_impact1.wav")},
        {SoundType.SOUND_SELECT_BUTTON,  GD.Load("res://assets/sounds/sfx_sounds_impact7.wav")},
    };

    public static void PlaySound(AudioStreamPlayer player, SoundType key)
    {
        if (!SOUNDS.ContainsKey(key))
        {
            return;
        }

        player.Stop();
        player.Stream = (AudioStream)SOUNDS[key];
        player.Play();
    }

    public static void PlayButtonClick(AudioStreamPlayer player)
    {
        PlaySound(player, SoundType.SOUND_SELECT_BUTTON);
    }

}
