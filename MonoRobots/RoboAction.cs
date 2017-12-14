using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpSoft.MonoRobots
{
    /// <summary>
    /// Performs an action of the roboter.
    /// </summary>
    public class RoboAction
    {
        public static readonly RoboAction EMPTY = new RoboAction();
        /// <summary>
        /// Performs an action - override to implement action.
        /// </summary>
        /// <param name="position">Position of the robot before the action.</param>
        /// <param name="board">Board to perform action on.</param>
        /// <returns>Position of robot after performing the action.</returns>
        public virtual RoboPosition PerformAction(RoboPosition position, RoboBoard board)
        {
            return PerformAction(position);
        }
        /// <summary>
        /// Performs an action - override to implement action.
        /// </summary>
        /// <param name="position">Position of the robot before the action.</param>
        /// <returns>Position of robot after performing the action.</returns>
        public virtual RoboPosition PerformAction(RoboPosition position)
        {
            return position;
        }
        /// <summary>
        /// Actions are equal if their type is equal.
        /// </summary>
        /// <param name="obj">Comparing object.</param>
        /// <returns>True on equal, false else.</returns>
        public override bool Equals(object obj)
        {
            return obj != null &&
                obj.GetType().GetHashCode() == this.GetType().GetHashCode() &&
                obj.GetHashCode() == this.GetHashCode();
        }
		/// <summary>
		/// Returns the hashcode of its underlying type.
		/// </summary>
		/// <returns>The hashcode of its underlying type.</returns>
		public override int GetHashCode()
		{
			return this.GetType().GetHashCode();
		}
    }
	
    /// <summary>
    /// Action that leads to the dead of the robot.
    /// </summary>
    //public class RoboDie : RoboAction
    //{
    //    /// <summary>
    //    /// Performs a dead of the robot.
    //    /// </summary>
    //    /// <param name="position"></param>
    //    /// <returns></returns>
    //    public override RoboPosition PerformAction(RoboPosition position)
    //    {
    //        return new RoboPosition(position.X, position.Y, position.Direction, true);
    //    }
    //}
    /// <summary>
    /// Action that performs a robot move.
    /// </summary>
	public class RoboMovement : RoboAction
    {
        public static readonly RoboMovement UP = new RoboMovement(Direction.Up);
        public static readonly RoboMovement DOWN = new RoboMovement(Direction.Down);
        public static readonly RoboMovement LEFT = new RoboMovement(Direction.Left);
        public static readonly RoboMovement RIGHT = new RoboMovement(Direction.Right);

        private Direction _direction;
        /// <summary>
        /// Set or get the direction to move.
        /// </summary>
        public Direction Direction {
        	get {
        		return _direction;
        	}
			set {
				_direction = value;
			}
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="direction">Direction to move.</param>
        public RoboMovement(Direction direction)
        {
            Direction = direction;
        }
        /// <summary>
        /// Performs a robot movement.
        /// </summary>
        /// <param name="position">Position of the robot before the action.</param>
        /// <param name="board">Board to perform action on.</param>
        /// <returns>Position of robot after performing the action.</returns>
        public override RoboPosition PerformAction(RoboPosition position, RoboBoard board)
        {
            RoboField field = board.GetField(position);

            if (!field.CanLeave(this.Direction)) return position;

            RoboPosition result = PerformAction(position);

            RoboField neighbor = board.GetField(result);

            if (neighbor == null || !neighbor.CanEnter(RoboRotation.Rotate(this.Direction, Rotation.Around))) return position;

            return result;
        }
        /// <summary>
        /// Performs a robot movement.
        /// </summary>
        /// <param name="position">Position of the robot before the action.</param>
        /// <returns>Position of robot after performing the action.</returns>
        public override RoboPosition PerformAction(RoboPosition position)
        {
            switch (Direction)
            {
                case Direction.Up:
                    return new RoboPosition(position.X, position.Y - 1, position.Direction);
                case Direction.Left:
                    return new RoboPosition(position.X - 1, position.Y, position.Direction);
                case Direction.Right:
                    return new RoboPosition(position.X + 1, position.Y, position.Direction);
                case Direction.Down:
                    return new RoboPosition(position.X, position.Y + 1, position.Direction);
            }
            return position;
        }

		public override int GetHashCode()
		{
			return (int)Direction;
		}
    }
	/// <summary>
	/// Action that performs a robot rotation.
	/// </summary>
	public class RoboRotation : RoboAction
    {
        public static readonly RoboRotation NONE = new RoboRotation(Rotation.None);
        public static readonly RoboRotation LEFT = new RoboRotation(Rotation.Left);
        public static readonly RoboRotation RIGHT = new RoboRotation(Rotation.Right);
        public static readonly RoboRotation AROUND = new RoboRotation(Rotation.Around);

        private Rotation _rotation;
        /// <summary>
        /// Get or set the rotation type.
        /// </summary>
        public Rotation Rotation {
        	get {
        		return _rotation;
        	}
			set {
				_rotation = value;
			}
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rotation">Type of rotation.</param>
        public RoboRotation(Rotation rotation)
        {
            Rotation = rotation;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previous">Direction of robot before rotation.</param>
        /// <param name="next">Direction of robot after rotation.</param>
        public RoboRotation(Direction previous, Direction next)
            : this(GetRotation(previous, next))
        {
        }
        /// <summary>
        /// Performs a rotation.
        /// </summary>
        /// <param name="direction">Direction before rotation.</param>
        /// <param name="rotation">Type of rotation.</param>
        /// <returns>Direction after rotation.</returns>
        public static Direction Rotate(Direction direction, Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.Around:
                    return (Direction)(((int)direction + 2) % 4);
                case Rotation.Left:
                    return (Direction)(((int)direction + 3) % 4);
                case Rotation.Right:
                    return (Direction)(((int)direction + 1) % 4);
                default:
                    return direction;
            }
        }
        /// <summary>
        /// Get the type of rotation by both directions - before and after the rotation.
        /// </summary>
        /// <param name="previousDirection">Direction before rotation.</param>
        /// <param name="newDirection">Direction after rotation.</param>
        /// <returns>The type of the rotation.</returns>
        public static Rotation GetRotation(Direction previousDirection, Direction newDirection)
        {
            if (previousDirection == newDirection) return Rotation.None;
            if (IsOpposite(previousDirection, newDirection)) return Rotation.Around;
            if ((int)previousDirection == (int)(newDirection + 3) % 4) return Rotation.Right;
            return Rotation.Left;
        }
        /// <summary>
        /// Returns whether given directions are opposite directions.
        /// </summary>
        /// <param name="a">First direction.</param>
        /// <param name="b">Second direction.</param>
        /// <returns>True if a is the opposite to b, false else.</returns>
        public static bool IsOpposite(Direction a, Direction b)
        {
            return ((int)a - (int)b) % 2 == 0;
        }
        /// <summary>
        /// Performs a rotation.
        /// </summary>
        /// <param name="direction">Direction before rotation.</param>
        /// <returns>Direction after rotation.</returns>
        public Direction Rotate(Direction direction)
        {
            return Rotate(direction, this.Rotation);
        }

		public override int GetHashCode()
		{
			return (int)Rotation;
		}

        /// <summary>
        /// Performs a robot rotation.
        /// </summary>
        /// <param name="position">Position of the robot before the action.</param>
        /// <returns>Position of robot after performing the action.</returns>
        public override RoboPosition PerformAction(RoboPosition position)
        {
            return new RoboPosition(position.X, position.Y, Rotate(position.Direction));
        }
    }

}