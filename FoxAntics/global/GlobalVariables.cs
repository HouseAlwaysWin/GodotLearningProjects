using System;

public enum PLAYER_STATE
{
    IDLE,
    RUN,
    JUMP,
    FALL,
    HURT
};

public enum FACING
{
    LEFT = -1,
    RIGHT = 1
}

public enum ObjectType
{
    EXPLOSION,
    PICKUP,
    BULLET_PLAYER,
    BULLET_ENEMY
}

public class Constants
{
    public const string PLAYER_GROUP = "Player";
    public const string MOVEABLES_GROUP = "Moveables";
    public const string EXPLOSION = "Explode";
}