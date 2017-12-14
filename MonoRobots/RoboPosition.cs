using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SeeSharpSoft.MonoRobots
{
    /// <summary>
    /// Class that represents a position/status of the robot.
    /// </summary>
    public class RoboPosition
    {
        public event EventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(EventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        /// <param name="direction">Direction robot is pointing.</param>
        /// <param name="isDead">True if robot is dead, false else.</param>
        public RoboPosition(int x, int y, Direction direction, bool isDead)
            : this(x, y, direction)
        {
            IsDead = isDead;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        /// <param name="direction">Direction robot is pointing.</param>
        public RoboPosition(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Position-template.</param>
        public RoboPosition(RoboPosition position) : this(position.X, position.Y, position.Direction, position.IsDead) { }


        /// <summary>
        /// Load position of the robot out of a file.
        /// </summary>
        /// <param name="filename">Filename.</param>
        /// <returns>RobotPosition defined in given file.</returns>
		public static RoboPosition LoadPosition(String filename)
		{
			StreamReader reader = new StreamReader(filename);
			RoboPosition result = DecodePosition(reader.ReadLine());
			reader.Close();
			return result;
		}
        public static void SavePosition(String filename, RoboPosition position)
        {
            StreamWriter writer = new StreamWriter(filename, false);
            writer.Write((position.X - 1) + " " + (position.Y - 1) + " " +
                position.Direction.ToString().Substring(0,1));
            writer.Close();
        }
		/// <summary>
		/// Parses a string and returns corresponding RoboPosition.
		/// </summary>
		/// <param name="position">RoboPosition encoded as a string.</param>
		/// <returns>RoboPosition decoded from given string.</returns>
		public static RoboPosition DecodePosition(String position)
		{
			String[] parts = position.Split(' ');
			Direction direction = Direction.Up;
			switch(parts[2])
			{
			    case "L":
				    direction = Direction.Left;
				    break;
			    case "R":
				    direction = Direction.Right;
				    break;
			    case "D":
				    direction = Direction.Down;
				    break;
			    case "U":
				    direction = Direction.Up;
				    break;
			}
			return new RoboPosition(int.Parse(parts[0]), int.Parse(parts[1]), direction); 
		}
		
        private int _x, _y;
        private Direction _direction;
        private bool _isDead = false;
        /// <summary>
        /// Set or get x-coordinate.
        /// </summary>
        [Assignable]
        public int X {
        	get {
        		return _x;
        	}
			set {
                if (_x == value) return;
				_x = value;
                OnPropertyChanged(EventArgs.Empty);
			}
        }
        /// <summary>
        /// Set or get y-coordinate.
        /// </summary>
        [Assignable]
        public int Y {
        	get {
        		return _y;
        	}
			set {
                if (_y == value) return;
				_y = value;
                OnPropertyChanged(EventArgs.Empty);
			}
        }
        /// <summary>
        /// Set or get direction robot is pointing.
        /// </summary>
        [Assignable]
        public Direction Direction {
        	get {
        		return _direction;
        	}
			set {
                if (_direction == value) return;
				_direction = value;
                OnPropertyChanged(EventArgs.Empty);
			}
        }
        /// <summary>
        /// Get or set whether robot is dead :( .
        /// </summary>
        [Assignable]
        public bool IsDead {
        	get {
        		return _isDead;
        	}
			set {
                if (_isDead == value) return;
				_isDead = value;
                OnPropertyChanged(EventArgs.Empty);
			}
        }

        public override string ToString()
        {
            return String.Format("Position: [{0}, {1}]; Direction: {2}; IsDead: {3}", X, Y, Direction, IsDead);
        }

        public override bool Equals(object obj)
        {
            RoboPosition pos = obj as RoboPosition;
            return pos != null && pos.X == this.X && pos.Y == this.Y && pos.Direction == this.Direction && pos.IsDead == this.IsDead;
        }
		
		public override int GetHashCode()
		{
			return EncodePosition(this.X, this.Y, this.Direction);
		}

        /// <summary>
        /// Encodes given coordinates and direction as integer.
        /// </summary>
        /// <param name="positionX">X-coordinate.</param>
        /// <param name="positionY">Y-coordinate.</param>
        /// <param name="direction">Direction robot is pointing.</param>
        /// <returns>Encoded position.</returns>
        public static int EncodePosition(int positionX, int positionY, Direction direction)
        {
            return ((int)direction << 24) | (positionX << 12) | positionY;
        }
        /// <summary>
        /// Decodes given integer as RobotPosition.
        /// </summary>
        /// <param name="position">Encoded position.</param>
        /// <returns>RoboPosition decoded from integer.</returns>
        public static RoboPosition DecodePosition(int position)
        {
            return new RoboPosition(((0x00FFF000 & position) >> 12), (0x00000FFF & position), (Direction)((0x0F000000 & position) >> 24));
        }

        #region ICloneable Member

        public object Clone()
        {
            return new RoboPosition(this);
        }

        #endregion
    }
}