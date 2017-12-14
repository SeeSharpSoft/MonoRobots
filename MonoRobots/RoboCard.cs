using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpSoft.MonoRobots
{
    /// <summary>
    /// Represents a card that can be played to navigate robot.
    /// </summary>
    public class RoboCard : IComparable<RoboCard>
    {
		public static Dictionary<String, CardType> CARDCODING = new Dictionary<String, CardType>();
		
		static RoboCard()
		{
			CARDCODING.Add("MF 1", CardType.MoveForwardOne);
			CARDCODING.Add("MF 2", CardType.MoveForwardTwo);
            CARDCODING.Add("MF 3", CardType.MoveForwardThree);
            CARDCODING.Add("MB", CardType.MoveBackwardOne);
            CARDCODING.Add("RL", CardType.TurnLeft);
            CARDCODING.Add("RR", CardType.TurnRight);
            CARDCODING.Add("RU", CardType.TurnAround);
		}
		
		public static int GetActionCount(CardType cardType)
		{
			switch(cardType)
			{
				case CardType.MoveForwardTwo:
					return 2;
				case CardType.MoveForwardThree:
					return 3;
				default:
					return 1;
			}
		}
		
		public static RoboCard DecodeCard(String encoded)
		{
			encoded = encoded.Trim();
			return new RoboCard(CARDCODING[encoded]);
		}
		
		public static string EncodeCard(RoboCard card)
		{
			return EncodeCard(card.CardType);
		}		
		
		public static string EncodeCard(CardType cardType)
		{
			foreach(KeyValuePair<String, CardType> ct in CARDCODING)
			{
				if(ct.Value == cardType) return ct.Key; 
			}
			//this shouldnt happen!
			return "???";
		}
		
        private CardType _cardType = CardType.Undefined;

        public CardType CardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">Type of card.</param>
        public RoboCard(CardType type)
        {
            CardType = type;
        }
        /// <summary>
        /// Returns all robot actions that will be performed by this card in correct order.
        /// </summary>
        /// <param name="direction">Actual direction of robot to define actions.</param>
        /// <returns>Well ordered actions performed by this card.</returns>
        public IEnumerable<RoboAction> GetActionList(Direction direction)
        {
            switch (CardType)
            {
                case CardType.MoveBackwardOne:
                    return new RoboAction[] { new RoboMovement(RoboRotation.Rotate(direction, Rotation.Around)) };
                case CardType.MoveForwardOne:
                    return new RoboAction[] { new RoboMovement(direction) };
                case CardType.MoveForwardTwo:
                    return new RoboAction[] { new RoboMovement(direction), new RoboMovement(direction) };
                case CardType.MoveForwardThree:
                    return new RoboAction[] { new RoboMovement(direction), new RoboMovement(direction), new RoboMovement(direction) };
                case CardType.TurnLeft:
                    return new RoboAction[] { new RoboRotation(Rotation.Left) };
                case CardType.TurnRight:
                    return new RoboAction[] { new RoboRotation(Rotation.Right) };
                case CardType.TurnAround:
                    return new RoboAction[] { new RoboRotation(Rotation.Around) };
                case CardType.Undefined:
                    return new RoboAction[] { RoboAction.EMPTY };
            }
            return new RoboAction[] { RoboAction.EMPTY };
        }
        /// <summary>
        /// Returns all robot actions that will be performed by this card in correct order.
        /// </summary>
        /// <param name="position">Actual position of robot to define actions.</param>
        /// <returns>Well ordered actions performed by this card.</returns>
        public IEnumerable<RoboAction> GetActionList(RoboPosition position)
        {
            return GetActionList(position.Direction);
        }

        public override string ToString()
        {
            return CardType.ToString();
        }

        public int CompareTo(RoboCard other)
        {
            //return other == null || !(other is RoboCard)
            //    ? 1
            //    : (int)this.CardType - (int)((RoboCard)other).CardType;
            return this.GetHashCode() - other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (int)CardType;
        }

        /// <summary>
        /// Encode a set of 5 cards as integer.
        /// </summary>
        /// <param name="cards">Array of length 5 or greater.</param>
        /// <returns>Unique code of given cards.</returns>
        public static int EncodeCards(RoboCard[] cards)
        {
            int result = 0;
            for (int i = 0; i < cards.Length && i < 5; i++) result |= ((int)cards[i].CardType << (i * 3));
            return result;
        }

        /// <summary>
        /// Decode cards encoded by <code>EncodeCards</code>.
        /// </summary>
        /// <param name="cards">Encoded cards as integer.</param>
        /// <returns>Decoded array of cards.</returns>
        public static RoboCard[] DecodeCards(int cards)
        {
            RoboCard[] result = new RoboCard[5];
            for (int i = 0; i < 5; i++) result[i] = new RoboCard((CardType)((cards >> (i * 3)) & 7));
            return result;
        }
    }
}