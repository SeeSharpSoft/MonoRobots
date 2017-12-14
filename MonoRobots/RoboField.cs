using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SeeSharpSoft.MonoRobots
{
    public class RoboField
    {
        protected RoboField() { }
        public RoboField(FieldType fieldType)
            : this()
        {
            FieldType = fieldType;
        }

        #region Static

        public static RoboField CreateField(FieldType type, RoboBoard board)
        {
            RoboField field = CreateField(type);
            field.Board = board;
            return field;
        }

        public static RoboField CreateField(FieldType type)
        {
            RoboField result = Activator.CreateInstance(GetTypeOf(type), type) as RoboField;
            return result;
        }

        public static Type GetTypeOf(FieldType type)
        {
            if ((type & FieldType.Hole) == FieldType.Hole) return typeof(RoboFieldHole);
            if ((type & FieldType.Oil) == FieldType.Oil) return typeof(RoboFieldOil);
            if (((int)type & 0x000000F0) > 0) return typeof(RoboFieldRotator);
            if (((int)type & 0x00000F00) > 0) return typeof(RoboFieldConveyor);
            if (((int)type & 0x0000F000) > 0) return typeof(RoboFieldPusher);
            //196608 = 65536 + 131072
            if (((int)type & 196608) > 0) return typeof(RoboFieldScrap);
            return typeof(RoboField);
        }

        public static Dictionary<char, FieldType> FIELDCODING = new Dictionary<char, FieldType>();
		
		static RoboField()
		{
			FIELDCODING.Add(' ', FieldType.Empty);
			FIELDCODING.Add('S', FieldType.StartLeft);
            FIELDCODING.Add('T', FieldType.StartRight);
            FIELDCODING.Add('U', FieldType.StartUp);
            FIELDCODING.Add('V', FieldType.StartDown);
            FIELDCODING.Add('H', FieldType.Hole);
            FIELDCODING.Add('O', FieldType.Oil);
            FIELDCODING.Add('Z', FieldType.Destination);
            FIELDCODING.Add('L', FieldType.RotatorLeft);
            FIELDCODING.Add('R', FieldType.RotatorRight);
            FIELDCODING.Add('<', FieldType.ConveyorLeft);
            FIELDCODING.Add('>', FieldType.ConveyorRight);
            FIELDCODING.Add('^', FieldType.ConveyorUp);
            FIELDCODING.Add('v', FieldType.ConveyorDown);
            FIELDCODING.Add('C', FieldType.PusherLeft);
            FIELDCODING.Add('D', FieldType.PusherRight);
            FIELDCODING.Add('E', FieldType.PusherUp);
            FIELDCODING.Add('F', FieldType.PusherDown);
            FIELDCODING.Add('M', FieldType.ScrapLeftRight);
            FIELDCODING.Add('N', FieldType.ScrapUpDown);
            FIELDCODING.Add('b', FieldType.WallLeft);
            FIELDCODING.Add('c', FieldType.WallRight);
            FIELDCODING.Add('e', FieldType.WallUp);
            FIELDCODING.Add('i', FieldType.WallDown);
		}

        public static RoboField DecodeField(char encoded)
        {
            if (RoboField.FIELDCODING.ContainsKey(encoded)) return RoboField.CreateField(RoboField.FIELDCODING[encoded]);

            return RoboField.CreateField((FieldType)((int)encoded - (int)'a'));
        }

        public static char EncodeField(FieldType fieldType)
        {
			foreach(KeyValuePair<char, FieldType> ft in FIELDCODING)
			{
				if(ft.Value == fieldType) return ft.Key; 
			}
			
			return ((char)((int)'a' + (int)fieldType));
        }

        #endregion

        public bool IsStart
        {
            get { return ((int)FieldType & 0x00F00000) > 0; }
        }

        public Direction StartDirection
        {
            get
            {
                return (FieldType & FieldType.StartDown) == FieldType.StartDown ? Direction.Down :
                    (FieldType & FieldType.StartUp) == FieldType.StartUp ? Direction.Up :
                    (FieldType & FieldType.StartLeft) == FieldType.StartLeft ? Direction.Left :
                    Direction.Right;
            }
        }

		private FieldType _fieldType;

        public bool IsDestination
        {
            get { return (FieldType & FieldType.Destination) == FieldType.Destination; }
        }

        public char EncodedField
        {
            get
            {
                return RoboField.EncodeField(this.FieldType);
            }
        }

        public FieldType FieldType
        {
            get
            {
                return _fieldType;
            }
            set
            {
                _fieldType = value;
            }
        }

        private int _x, _y;
        private RoboBoard _board;

        /// <summary>
        /// Get or set x-coordinate.
        /// </summary>
		public int X {
        	get {
				return _x;
			}
			set {
				_x = value;
			}
        }

        /// <summary>
        /// Get or set y-coordinate.
        /// </summary>
        public int Y {
        	get {
				return _y;
        	}
			set {
				_y = value;
			}
        }

        /// <summary>
        /// Get or set board field belongs to.
        /// </summary>
		public RoboBoard Board {
        	get {
				return _board;
        	}
			set
			{
				_board = value;
			}
        }

        public virtual void Rotate()
        {
            if (IsDestination) return;

            int fieldType = (int)FieldType;

            //fieldType = fieldType & 0x00F0000F;

            int fieldTypeA = (fieldType & GetUpDownCut()) << 1;
            fieldTypeA = ((fieldTypeA & GetUpDownCut()) | ((fieldTypeA & GetLeftRightCut()) >> 2)) >> 2;

            int fieldTypeB = (fieldType & GetLeftRightCut()) << 2;

            fieldType = ((fieldTypeA | fieldTypeB));// & 0x00F0000F);

            FieldType = (FieldType)fieldType;
        }

        public virtual void MirrorVertical()
        {
            if (IsDestination) return;

            int fieldType = (int)FieldType;

            fieldType = fieldType & GetUpDownCut();

            fieldType = fieldType << 1;

            fieldType = ((fieldType | (fieldType >> 2)) & GetUpDownCut()) | ((int)FieldType & GetLeftRightCut());

            FieldType = (FieldType)fieldType;
        }

        public virtual void MirrorHorizontal()
        {
            if (IsDestination) return;

            int fieldType = (int)FieldType;

            fieldType = fieldType & GetLeftRightCut();

            fieldType = fieldType << 1;

            fieldType = ((fieldType | (fieldType >> 2)) & GetLeftRightCut()) | ((int)FieldType & GetUpDownCut());

            FieldType = (FieldType)fieldType;
        }

        protected int GetLeftRightCut()
        {
            int result = 0x0F0F0F0F;
            return result ^ (result << 2);
        }

        protected int GetUpDownCut()
        {
            return GetLeftRightCut() << 2;
        }
		
		/// <summary>
		/// Main function for handling robot action - splits behavior in enter (move in), rotation and stay to ease up programming...
		/// </summary>
		/// <param name="actionPhase">
		/// A <see cref="ActionPhase"/>
		/// </param>
		/// <param name="action">
		/// A <see cref="RoboAction"/>
		/// </param>
		/// <param name="position">
		/// A <see cref="RoboPosition"/>
		/// </param>
		/// <returns>
		/// A <see cref="RoboAction"/>
		/// </returns>
        public virtual RoboAction OnRobotAction(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            if (position.IsDead) return null;
            if (action is RoboMovement) return OnRobotEnter(actionPhase, action as RoboMovement, position);
            if (action is RoboRotation) return OnRobotRotate(actionPhase, action as RoboRotation, position);
            return OnRobotStay(actionPhase, action, position);
        }

        public virtual RoboAction OnRobotStay(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            return null;
        }

        public virtual RoboAction OnRobotEnter(ActionPhase actionPhase, RoboMovement movement, RoboPosition position)
        {
            return null;
        }

        public virtual RoboAction OnRobotRotate(ActionPhase actionPhase, RoboRotation rotation, RoboPosition position)
        {
            return null;
        }
		
		/// <summary>
		/// These switches also could be better(?) made in field-subclasses... 
		/// </summary>
		/// <param name="fromDirection">
		/// A <see cref="Direction"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public virtual bool CanEnter(Direction fromDirection)
        {
            switch (fromDirection)
            {
                case Direction.Left:
                    if ((FieldType & FieldType.WallLeft) == FieldType.WallLeft ||
                        (FieldType & FieldType.PusherRight) == FieldType.PusherRight ||
                        (FieldType & FieldType.ScrapLeftRight) == FieldType.ScrapLeftRight)
                        return false;
                    break;
                case Direction.Right:
                    if ((FieldType & FieldType.WallRight) == FieldType.WallRight ||
                        (FieldType & FieldType.PusherLeft) == FieldType.PusherLeft ||
                        (FieldType & FieldType.ScrapLeftRight) == FieldType.ScrapLeftRight)
                        return false;
                    break;
                case Direction.Up:
                    if ((FieldType & FieldType.WallUp) == FieldType.WallUp ||
                        (FieldType & FieldType.PusherDown) == FieldType.PusherDown ||
                        (FieldType & FieldType.ScrapUpDown) == FieldType.ScrapUpDown)
                        return false;
                    break;
                case Direction.Down:
                    if ((FieldType & FieldType.WallDown) == FieldType.WallDown ||
                        (FieldType & FieldType.PusherUp) == FieldType.PusherUp ||
                        (FieldType & FieldType.ScrapUpDown) == FieldType.ScrapUpDown)
                        return false;
                    break;
            }

            return true;
        }
		
		/// <summary>
		/// These switches also could be better(?) made in field-subclasses... 
		/// </summary>
		/// <param name="toDirection">
		/// A <see cref="Direction"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public virtual bool CanLeave(Direction toDirection)
        {
            //Boxes can be left -> do it for multi robo game
            if (((int)FieldType & 0x0000000F) == 0x0000000F) return true;

            switch (toDirection)
            {
                case Direction.Left:
                    if ((FieldType & FieldType.WallLeft) == FieldType.WallLeft ||
                        (FieldType & FieldType.PusherRight) == FieldType.PusherRight ||
                        (FieldType & FieldType.ScrapLeftRight) == FieldType.ScrapLeftRight)
                        return false;
                    break;
                case Direction.Right:
                    if ((FieldType & FieldType.WallRight) == FieldType.WallRight ||
                        (FieldType & FieldType.PusherLeft) == FieldType.PusherLeft ||
                        (FieldType & FieldType.ScrapLeftRight) == FieldType.ScrapLeftRight)
                        return false;
                    break;
                case Direction.Up:
                    if ((FieldType & FieldType.WallUp) == FieldType.WallUp ||
                        (FieldType & FieldType.PusherDown) == FieldType.PusherDown ||
                        (FieldType & FieldType.ScrapUpDown) == FieldType.ScrapUpDown)
                        return false;
                    break;
                case Direction.Down:
                    if ((FieldType & FieldType.WallDown) == FieldType.WallDown ||
                        (FieldType & FieldType.PusherUp) == FieldType.PusherUp ||
                        (FieldType & FieldType.ScrapUpDown) == FieldType.ScrapUpDown)
                        return false;
                    break;
            }
            return true;
        }

        public virtual bool CanLeave()
        {
            return CanLeave(Direction.Up) || CanLeave(Direction.Left) || CanLeave(Direction.Down) || CanLeave(Direction.Right);
        }

        public override string ToString()
        {
            return RoboField.EncodeField(FieldType).ToString();
        }
    }

    public class RoboFieldOil : RoboField
    {
        public RoboFieldOil(FieldType type)
            : base(type)
        { }

        public override void MirrorHorizontal()
        {
            //base.MirrorHorizontal();
        }

        public override void MirrorVertical()
        {
            //base.MirrorVertical();
        }

        public override void Rotate()
        {
            //base.Rotate();
        }

        public override RoboAction OnRobotEnter(ActionPhase actionPhase, RoboMovement movement, RoboPosition position)
        {
            RoboPosition nextPosition = movement.PerformAction(position, this.Board);
            //Moving possible?
            if (!nextPosition.Equals(position)) return movement;

            return base.OnRobotEnter(actionPhase, movement, position);
        }

		public override RoboAction OnRobotRotate(ActionPhase actionPhase, RoboRotation rotation, RoboPosition position)
		{
			//TODO: this is not done well -> should return rotation, but doing so leads to infinite call of this behavior 
            position.Direction = rotation.Rotate(position.Direction);
            return base.OnRobotRotate(actionPhase, rotation, position);
        }
    }

    public class RoboFieldPusher : RoboField
    {
        public RoboFieldPusher(FieldType type)
            : base(type)
        { }

        public override RoboAction OnRobotStay(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            switch (actionPhase)
            {
                case ActionPhase.ActionPusher:
                    return new RoboMovement
                        (
                            (FieldType & FieldType.PusherDown) == FieldType.PusherDown ? Direction.Down :
                            (FieldType & FieldType.PusherLeft) == FieldType.PusherLeft ? Direction.Left :
                            (FieldType & FieldType.PusherRight) == FieldType.PusherRight ? Direction.Right :
                            Direction.Up
                        );
            }

			return base.OnRobotStay(actionPhase, action, position);
		}
    }

    public class RoboFieldConveyor : RoboField
    {
        public RoboFieldConveyor(FieldType type)
            : base(type)
        { }

        public override RoboAction OnRobotEnter(ActionPhase actionPhase, RoboMovement movement, RoboPosition position)
        {
            switch (actionPhase)
            {
                case ActionPhase.ActionConveyor:
                    return new RoboRotation
                        (
                            (movement.Direction == Direction.Up && (FieldType & FieldType.ConveyorLeft) == FieldType.ConveyorLeft) ||
                            (movement.Direction == Direction.Right && (FieldType & FieldType.ConveyorUp) == FieldType.ConveyorUp) ||
                            (movement.Direction == Direction.Down && (FieldType & FieldType.ConveyorRight) == FieldType.ConveyorRight) ||
                            (movement.Direction == Direction.Left && (FieldType & FieldType.ConveyorDown) == FieldType.ConveyorDown) ?
							Rotation.Left :
							((movement.Direction == Direction.Up && (FieldType & FieldType.ConveyorRight) == FieldType.ConveyorRight) ||
                            (movement.Direction == Direction.Right && (FieldType & FieldType.ConveyorDown) == FieldType.ConveyorDown) ||
                            (movement.Direction == Direction.Down && (FieldType & FieldType.ConveyorLeft) == FieldType.ConveyorLeft) ||
					 		(movement.Direction == Direction.Left && (FieldType & FieldType.ConveyorUp) == FieldType.ConveyorUp) ?
							Rotation.Right : Rotation.None)
                        );
            }
            return base.OnRobotEnter(actionPhase, movement, position);
        }

        public override RoboAction OnRobotStay(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            switch (actionPhase)
            {
                case ActionPhase.ActionConveyor:
                    return new RoboMovement
                        (
                            (FieldType & FieldType.ConveyorDown) == FieldType.ConveyorDown ? Direction.Down :
                            (FieldType & FieldType.ConveyorLeft) == FieldType.ConveyorLeft ? Direction.Left :
                            (FieldType & FieldType.ConveyorRight) == FieldType.ConveyorRight ? Direction.Right :
                            Direction.Up
                        );
            }
            return base.OnRobotStay(actionPhase, action, position);
        }
    }

    public class RoboFieldHole : RoboField
    {
        public RoboFieldHole(FieldType type)
            : base(type)
        { }

        public override void MirrorHorizontal()
        {
            //base.MirrorHorizontal();
        }

        public override void MirrorVertical()
        {
            //base.MirrorVertical();
        }

        public override void Rotate()
        {
            //base.Rotate();
        }

        public override RoboAction OnRobotAction(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            position.IsDead = true;
            return null;
        }

        public override bool CanLeave(Direction toDirection)
        {
            return false;
        }

        public override bool CanLeave()
        {
            return false;
        }
    }

    public class RoboFieldScrap : RoboField
    {
        public RoboFieldScrap(FieldType type)
            : base(type)
        { }

        public override void MirrorHorizontal()
        {
            //base.MirrorHorizontal();
        }

        public override void MirrorVertical()
        {
            //base.MirrorVertical();
        }

        public override void Rotate()
        {
            base.MirrorHorizontal();
        }

        public override RoboAction OnRobotStay(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            switch (actionPhase)
			{
				case ActionPhase.ActionPusher:
                    position.IsDead = true;
                    break;

            }
            return base.OnRobotStay(actionPhase, action, position);
        }
    }

    public class RoboFieldRotator : RoboField
    {
        public RoboFieldRotator(FieldType type)
            : base(type)
        { }

        public override void MirrorHorizontal()
        {
            //base.MirrorHorizontal();
        }

        public override void MirrorVertical()
        {
            //base.MirrorVertical();
        }

        public override void Rotate()
        {
            //base.Rotate();
        }

        public override RoboAction OnRobotStay(ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            switch (actionPhase)
            {
                case ActionPhase.ActionRotator:
                    return new RoboRotation(
                            (FieldType & FieldType.RotatorLeft) == FieldType.RotatorLeft ? Rotation.Left : Rotation.Right
                        );
            }
            return base.OnRobotStay(actionPhase, action, position);
        }
    }
}