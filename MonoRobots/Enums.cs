using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpSoft.MonoRobots
{
    /// <summary>
    /// Difficulty of the game.
    /// </summary>
	public enum Difficulty
	{
		Easy,
		Normal,
		Hard
	}
	/// <summary>
	/// Phase of the games where actions will be performed.
	/// </summary>
    public enum ActionPhase
    {
        ActionCard,
        ActionConveyor,
        ActionPusher,
        ActionRotator
    }
    /// <summary>
    /// Type of rotation.
    /// </summary>
    public enum Rotation
    {
        None = 0,
        Left = 5,
        Right = 6,
        Around = 7
    }
    /// <summary>
    /// Type of direction.
    /// </summary>
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }
    /// <summary>
    /// Type of card.
    /// </summary>
    public enum CardType
    {
        Undefined = 0,
        MoveForwardOne = 1,
        MoveForwardTwo = 2,
        MoveForwardThree = 3,
        MoveBackwardOne = 4,
        TurnLeft = 5,
        TurnRight = 6,
        TurnAround = 7
    }

    /// <summary>
    /// Fieldtype.
    /// </summary>
    [Flags]
    public enum FieldType
    {
        Empty = 0,

        WallLeft = 1,
        WallRight = 2,
        WallUp = 4,
        WallDown = 8,

        Hole = 16,
        Oil = 32, //5
        RotatorLeft = 64,
        RotatorRight = 128,

        ConveyorLeft = 256,
        ConveyorRight = 512,
        ConveyorUp = 1024, //10
        ConveyorDown = 2048,

        PusherLeft = 4096,
        PusherRight = 8192,
        PusherUp = 16384,
        PusherDown = 32768, //15
        
        ScrapLeftRight = 65536,
        ScrapUpDown = 131072,
        Destination = 262144,

        //19 -> free

        StartLeft = 1048576, // 20
        StartRight = 2097152, 
        StartUp = 4194304,
        StartDown = 8388608
    }
}