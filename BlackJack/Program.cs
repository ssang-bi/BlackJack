﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public enum card
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    } // A ~ K
    static class Tool
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static int Score(card card, int score)
        {
            switch (card)
            {
                case (card.Ace):
                    if (score <= 10) score += 11;
                    else score += 1;
                    break;
                case (card.Two):
                    score += 2;
                    break;
                case (card.Three):
                    score += 3;
                    break;
                case (card.Four):
                    score += 4;
                    break;
                case (card.Five):
                    score += 5;
                    break;
                case (card.Six):
                    score += 6;
                    break;
                case (card.Seven):
                    score += 7;
                    break;
                case (card.Eight):
                    score += 8;
                    break;
                case (card.Nine):
                    score += 9;
                    break;
                case (card.Ten):
                    score += 10;
                    break;
                case (card.Jack):
                    score += 10;
                    break;
                case (card.Queen):
                    score += 10;
                    break;
                case (card.King):
                    score += 10;
                    break;
            }  // score + card number
            return score;
        }
    }
    class Game
    {
        Dealer dealer;
        Player player;

        public Game(Dealer _dealer, Player _player)
        {
            this.dealer = _dealer;
            this.player = _player;
        }
        public void Board()
        {
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("<Dealer>");
            foreach (card a in this.dealer.Hands) Console.Write("[{0}]  ", a);
            Console.WriteLine("\nscore : {0}", this.dealer.score);

            Console.WriteLine("\nscore : {0}         Bet : {1:c}", this.player.score, this.player.Bet);
            foreach (card a in this.player.Hands) Console.Write("[{0}]  ", a);
            Console.WriteLine("\n<{0}>         Money : {1:c}\n", this.player.Name, this.player.Money);
        }
    }
    class CardDeck
    {
        public List<card> Deck = new List<card>();
        public Stack<card> GameDeck = new Stack<card>();
        public void Create()  // Create game Deck with stack
        {
            for (int i = 0; i < 4; i++)
            {
                Deck.Add(card.Ace);
                Deck.Add(card.Two);
                Deck.Add(card.Three);
                Deck.Add(card.Four);
                Deck.Add(card.Five);
                Deck.Add(card.Six);
                Deck.Add(card.Seven);
                Deck.Add(card.Eight);
                Deck.Add(card.Nine);
                Deck.Add(card.Ten);
                Deck.Add(card.Jack);
                Deck.Add(card.Queen);
                Deck.Add(card.King);
            }
            Tool.Shuffle(Deck);
            foreach (card a in this.Deck) this.GameDeck.Push(a);
        }
    }
    class Dealer
    {
        public int score = new int();
        public List<card> Hands = new List<card>();
        public void Draw(CardDeck cardDeck)
        {
            card card = cardDeck.GameDeck.Pop();
            this.Hands.Add(card);
            this.score = Tool.Score(card, this.score);
        }
        public void ExtraCard(CardDeck cardDeack)
        {
            while (this.score <= 16) this.Draw(cardDeack);            
        }
    }
    class Player
    {
        public string Name;
        public int Money = new int();
        public int Bet = new int();
        public int score = new int();
        public List<card> Hands = new List<card>();
        public Player(string _name, int _money)
        {
            this.Name = _name;
            this.Money = _money;
        }
        public void Betting()
        {
            Console.Write("Betting Amount(Enter in multiples of 10) : ");
            this.Bet = Convert.ToInt32(Console.ReadLine());
            this.Money -= this.Bet;
        }
        public void Draw(CardDeck cardDeck)
        {
            card card = cardDeck.GameDeck.Pop();
            this.Hands.Add(card);
            this.score = Tool.Score(card, this.score);
        }
        public void Hit(CardDeck cardDeck)
        {
            this.Draw(cardDeck);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CardDeck cardDeck = new CardDeck();
            Dealer dealer = new Dealer();

            Console.Write("Enter your name : ");
            string name = Console.ReadLine();
            Console.Write("Insert your money(Enter in multiples of 100) : ");
            Player player = new Player(name, Convert.ToInt32(Console.ReadLine()));

            Game game = new Game(dealer, player);
            
            cardDeck.Create();

            // test
            dealer.Draw(cardDeck);
            dealer.Draw(cardDeck);
            
            player.Draw(cardDeck);
            player.Draw(cardDeck);

            game.Board();

            /*    test -> draw more card 
            Console.WriteLine("Do you want draw more card?(1 : yes    2 : no");
            int answer = (Convert.ToInt32(Console.ReadLine()));
            if (answer == 1) player.Draw(cardDeck);

            game.Board();

            //*/
        }
    }
}
