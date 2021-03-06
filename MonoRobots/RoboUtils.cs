﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using SeeSharpSoft.MonoRobots.Plugin.Impl;
using SeeSharpSoft.MonoRobots.Plugin;

namespace SeeSharpSoft.MonoRobots
{
    public static class RoboUtils
    {
        public const int CARD_DECK_SIZE = 5600;

        #region Static base methods

        public static RoboPosition PerformAction(RoboBoard board, ActionPhase actionPhase, RoboAction action, RoboPosition position)
        {
            if (action == null) return position;

            position.Assign(action.PerformAction(position, board));
            position.Assign(PerformAction(board, actionPhase, board.GetField(position).OnRobotAction(actionPhase, action, position), position));
            return position;
        }

        public static RoboPosition PlayCard(RoboBoard board, RoboCard card, RoboPosition position)
        {
            return PlayCardCore(board, card, new RoboPosition(position));
        }

        public static RoboPosition PlayCardCore(RoboBoard board, RoboCard card, RoboPosition position)
        {
            foreach (RoboAction action in card.GetActionList(position))
            {
                if (position.IsDead || board.GetField(position).IsDestination) return position;
                position.Assign(PerformAction(board, ActionPhase.ActionCard, action, position));
            }

            position.Assign(PerformAction(board, ActionPhase.ActionConveyor, RoboAction.EMPTY, position));
            position.Assign(PerformAction(board, ActionPhase.ActionPusher, RoboAction.EMPTY, position));
            position.Assign(PerformAction(board, ActionPhase.ActionRotator, RoboAction.EMPTY, position));

            return position;
        }

        public static RoboCard[] CreateCardPile()
        {
            List<RoboCard> temp = CreateCardPile(CARD_DECK_SIZE);
            Random randomizer = new Random();

            RoboCard[] pile = new RoboCard[CARD_DECK_SIZE];
            for (int k = 0; k < CARD_DECK_SIZE; k++)
            {
                int randomIndex = randomizer.Next(temp.Count);
                pile[k] = temp[randomIndex];
                temp.RemoveAt(randomIndex);
            }

            return pile;
        }

        /// <summary>
        /// Creates a cardpile with respect to the probabilities of the seven different cardtypes.
        /// </summary>
        /// <param name="nrOfCards">Size of the pile - pile can be have some more cards to keep correct distribution of the cards.</param>
        /// <returns>Cardpile.</returns>
        public static List<RoboCard> CreateCardPile(int nrOfCards)
        {
            List<RoboCard> cards = new List<RoboCard>(nrOfCards);
            int max = (int)Math.Ceiling(nrOfCards / 14d);
            for (int i = 0; i < max; i++)
            {
                cards.Add(new RoboCard(CardType.MoveForwardThree));
                cards.Add(new RoboCard(CardType.MoveBackwardOne));
                cards.Add(new RoboCard(CardType.TurnAround));

                cards.Add(new RoboCard(CardType.MoveForwardTwo));
                cards.Add(new RoboCard(CardType.MoveForwardTwo));

                cards.Add(new RoboCard(CardType.MoveForwardOne));
                cards.Add(new RoboCard(CardType.MoveForwardOne));
                cards.Add(new RoboCard(CardType.MoveForwardOne));

                cards.Add(new RoboCard(CardType.TurnLeft));
                cards.Add(new RoboCard(CardType.TurnLeft));
                cards.Add(new RoboCard(CardType.TurnLeft));

                cards.Add(new RoboCard(CardType.TurnRight));
                cards.Add(new RoboCard(CardType.TurnRight));
                cards.Add(new RoboCard(CardType.TurnRight));
            }

            return cards;
        }

        public static RoboCard[] LoadCardDeck(String filename)
        {
            return LoadCards(filename, CARD_DECK_SIZE);
        }

        public static RoboCard[] LoadCards(String filename, int amount)
        {
            RoboCard[] cards = new RoboCard[amount];
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filename, Encoding.UTF8);

                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i] = RoboCard.DecodeCard(reader.ReadLine());
                }
            }
            catch
            {

            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return cards;
        }

        /// <summary>
        /// Save cards to file.
        /// </summary>
        /// <param name="filename">File to save to.</param>
        /// <param name="cards">Cards to save.</param>
        public static void SaveCardsToFile(String filename, IEnumerable<RoboCard> cards)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                foreach (RoboCard card in cards)
                    writer.WriteLine(RoboCard.EncodeCard(card));
                writer.Close();
            }
        }
        #endregion

        private static String FindPluginName(RoboManager roboManager, String initialName)
        {
            IEnumerable<String> pluginNames = roboManager.AvailablePlugins.Select(plugin => plugin.Name);
            String pluginName = initialName;
            int nameDifferentiater = 1;
            while (pluginNames.Contains(pluginName))
            {
                nameDifferentiater++;
                pluginName = initialName + nameDifferentiater;
            }
            return pluginName;
        }

        public static RoboPlayerPlugin RegisterPlugin(RoboManager roboManager, String executablePath)
        {
            return RegisterPlugin(roboManager, executablePath, false);
        }

        public static RoboPlayerPlugin RegisterPlugin(RoboManager roboManager, String executablePath, bool isRelativePath)
        {
            String pluginName = Path.GetFileName(executablePath);
            pluginName = FindPluginName(roboManager, pluginName.Substring(0, pluginName.IndexOf('.')));
            RoboPlayerFileIOWrapper roboPlugin = new RoboPlayerFileIOWrapper();
            roboPlugin.ExecutablePath = (isRelativePath ? Directory.GetCurrentDirectory() + "/" : "") + executablePath;
            roboPlugin.Name = pluginName;
            roboManager.AvailablePlugins.Add(roboPlugin);

            Console.WriteLine("Initialized KI: " + roboPlugin.ExecutablePath);

            return roboPlugin;
        }

        public static RoboBoard LoadBoard(String boardPath, Difficulty difficulty)
        {
            return LoadBoard(boardPath, false, difficulty);
        }

        public static RoboBoard LoadBoard(String boardPath, bool isRelativePath, Difficulty difficulty)
        {
            String path = (isRelativePath ? Directory.GetCurrentDirectory() + "/" : "") + boardPath;
            Console.WriteLine("Loading Board: " + path);
            RoboBoard board = new RoboBoard();
            board.Load(path, difficulty);
            return board;
        }

        public static ICollection<int> Permutation(RoboCard[] cards, int start, int n)
        {
            HashSet<int> result = new HashSet<int>();

            if (start == n)
            {
                result.Add(RoboCard.EncodeCards(cards));
            }
            else
            {
                for (int i = start; i < cards.Length; i++)
                {
                    RoboCard tmp = cards[i];

                    cards[i] = cards[start];
                    cards[start] = tmp;

                    result.AddRange(Permutation(cards, start + 1, n));

                    cards[start] = cards[i];
                    cards[i] = tmp;
                }
            }

            return result;
        }
    }
}