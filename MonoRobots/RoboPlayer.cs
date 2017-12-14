using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SeeSharpSoft.MonoRobots
{
    /// <summary>
    /// Base class for players - might be useful for later implementation of human players.
    /// </summary>
    public class RoboPlayer : INotifyPropertyChanged
    {
        private String _name;
        public String Name
        {
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _name; }
        }

        private RoboCard[] _cards;
        /// <summary>
        /// Get or set handcards/played cards.
        /// </summary>
        public RoboCard[] Cards
        {
            private set
            {
                if (_cards != value)
                {
                    _cards = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _cards; }
        }

        private List<RoboCard> _cardPile;
        /// <summary>
        /// Get or set cards player wants to play.
        /// </summary>
        public List<RoboCard> CardPile
        {
            set
            {
                if (_cardPile != value)
                {
                    _cardPile = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _cardPile; }
        }

        private RoboPosition _position;
        /// <summary>
        /// Get or set position of players robot on board.
        /// </summary>
        public RoboPosition Position
        {
            private set
            {
                if (_position != value)
                {
                    _position = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _position; }
        }

        private int _round;
        /// <summary>
        /// Get or set the round player is actual in.
        /// </summary>
        public int Round
        {
            private set
            {
                if (_round != value)
                {
                    _round = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _round; }
        }

        private int _totalPlayedCards;
        public int TotalPlayedCards
        {
            set
            {
                if (_totalPlayedCards != value)
                {
                    _totalPlayedCards = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _totalPlayedCards; }
        }

        private DateTime _timeStartRound;
        public DateTime TimeStartRound
        {
            private set
            {
                if (_timeStartRound != value)
                {
                    _timeStartRound = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _timeStartRound; }
        }

        private DateTime _timeEndRound;
        public DateTime TimeEndRound
        {
            private set
            {
                if (_timeEndRound != value)
                {
                    _timeEndRound = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _timeEndRound; }
        }

        private TimeSpan _totalTimeElapsed;
        public TimeSpan TotalTimeElapsed
        {
            private set
            {
                if (_totalTimeElapsed != value)
                {
                    _totalTimeElapsed = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _totalTimeElapsed; }
        }

        private RoboPlayerState _playerState;
        public RoboPlayerState PlayerState
        {
            set
            {
                if (_playerState != value)
                {
                    _playerState = value;
                    NotifyPropertyChanged();
                }
            }

            get { return _playerState; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RoboPlayer()
        {
            Cards = new RoboCard[8];
            Position = new RoboPosition(0, 0, Direction.Up);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void StartGame()
        {
            Round = 0;
            TotalPlayedCards = 0;
            TotalTimeElapsed = new TimeSpan();
        }

        public virtual void EndGame()
        {

        }

        public virtual bool StartRound()
        {
            Round++;
            if (!DrawCards()) return false;
            TimeStartRound = DateTime.Now;
            return true;
        }

        public virtual void EndRound(IEnumerable<RoboCard> playedCards)
        {
            TimeEndRound = DateTime.Now;

            TotalTimeElapsed += (TimeEndRound - TimeStartRound);

            Cards = playedCards.ToArray();
        }

        private bool DrawCards()
        {
            Cards = new RoboCard[8];
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    Cards[i] = CardPile[8 * (Round - 1) + i];
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Read in handcards.
        /// </summary>
        /// <param name="filename">Filename.</param>
        /// <returns>True on successfull load, false else.</returns>
        public void ReadCardsFromFile(String filename)
        {
            Cards = RoboUtils.LoadCards(filename, 8);
        }

    }
}