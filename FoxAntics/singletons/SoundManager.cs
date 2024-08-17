using Godot;
using Godot.Collections;
using System;

public partial class SoundManager : Node
{

    public const string SOUND_LASER = "laser";
    public const string SOUND_CHECKPOINT = "checkpoint";
    public const string SOUND_DAMAGE = "damage";
    public const string SOUND_KILL = "kill";
    public const string SOUND_GAMEOVER = "gameover1";
    public const string SOUND_IMPACT = "impact";
    public const string SOUND_LAND = "land";
    public const string SOUND_MUSIC1 = "music1";
    public const string SOUND_MUSIC2 = "music2";
    public const string SOUND_PICKUP = "pickup";
    public const string SOUND_BOSS_ARRIVE = "boss_arrive";
    public const string SOUND_JUMP = "jump";
    public const string SOUND_WIN = "win";

    Dictionary<string, Resource> SOUNDS = new()
    {
        {SOUND_CHECKPOINT, GD.Load("res://assets/sound/checkpoint.wav")},
        {SOUND_DAMAGE, GD.Load("res://assets/sound/damage.wav")},
        {SOUND_KILL, GD.Load("res://assets/sound/pickup5.ogg")},
        {SOUND_GAMEOVER, GD.Load("res://assets/sound/game_over.ogg")},
        {SOUND_IMPACT, GD.Load("res://assets/sound/impact.wav")},
        {SOUND_JUMP, GD.Load("res://assets/sound/jump.wav")},
        {SOUND_LAND, GD.Load("res://assets/sound/land.wav")},
        {SOUND_LASER, GD.Load("res://assets/sound/laser.wav")},
        {SOUND_MUSIC1, GD.Load("res://assets/sound/Farm Frolics.ogg")},
        {SOUND_MUSIC2, GD.Load("res://assets/sound/Flowing Rocks.ogg")},
        {SOUND_PICKUP, GD.Load("res://assets/sound/pickup5.ogg")},
        {SOUND_BOSS_ARRIVE, GD.Load("res://assets/sound/boss_arrive.wav")},
        {SOUND_WIN, GD.Load("res://assets/sound/you_win.ogg")}
    };

    public void PlayClip(AudioStreamPlayer2D player, string clipKey)
    {
        if (!SOUNDS.ContainsKey(clipKey)) return;

        player.Stream = (AudioStream)SOUNDS[clipKey];
        player.Play();
    }
}
