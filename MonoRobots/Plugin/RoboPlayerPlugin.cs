using SeeSharpSoft.Plugin;
using System;
using System.Collections.Generic;

namespace SeeSharpSoft.MonoRobots.Plugin
{
    public class RoboPlayerPlugin : RoboPluginBase
    {
        internal PlayCards PlayCardsCallback { set; get; }
        protected RoboBoard Board { set; get; }
        public RoboPlayer Player { set; get; }
        public String Name { set; get; }

        public RoboPlayerPlugin()
        {
            Name = this.GetType().GetPluginName();
        }

        public virtual void StartGame(RoboBoard board)
        {
            Board = board;
        }

        public virtual void StartRound(RoboPosition position, ICollection<RoboCard> cards, IEnumerable<RoboPosition> allPlayers)
        {

        }

        protected void PlayCards(IEnumerable<RoboCard> cards)
        {
            PlayCards(cards, null);
        }

        protected void PlayCards(IEnumerable<RoboCard> cards, Exception exception)
        {
            PlayCardsCallback.BeginInvoke(this, cards, exception, null, null);
        }

        protected RoboBoard GetBlockingBoard(RoboPosition ownPosition, IEnumerable<RoboPosition> allPlayers)
        {
            RoboBoard board = Board.CopyBoard();

            if ((PluginSettings as RoboPlayerPluginSettings).PlayerCollision)
            {
                foreach (RoboPosition otherPosition in allPlayers)
                {
                    if (ownPosition.Equals(otherPosition) ||
                        board.GetField(otherPosition).IsDestination ||
                        otherPosition.IsDead) continue;

                    board.Fields[otherPosition.X, otherPosition.Y] =
                        RoboField.CreateField(FieldType.WallDown | FieldType.WallUp | FieldType.WallLeft | FieldType.WallRight);
                }
            }

            return board;
        }
    }
}