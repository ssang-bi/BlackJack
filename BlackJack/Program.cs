using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Add code -> When the dealer is blackjack, Running out of cards, Dealers give delays when pulling cards.
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
        public void InitialBoard()
        {
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("<Dealer>");
            Console.WriteLine("[{0}]  [****]", this.dealer.Hands[0]);

            Console.WriteLine("\n<{0}>        Money : {1:c}", this.player.Name, this.player.Money);
            foreach (card a in this.player.Hands) Console.Write("[{0}]  ", a);
            Console.WriteLine("\nscore : {0}      Bet : {1:c}\n", this.player.score, this.player.Bet);
        }
        public void Board()
        {
            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("<Dealer>");
            foreach (card a in this.dealer.Hands) Console.Write("[{0}]  ", a);
            Console.WriteLine("\nscore : {0}", this.dealer.score);

            Console.WriteLine("\n<{0}>        Money : {1:c}", this.player.Name, this.player.Money);
            foreach(card a in this.player.Hands) Console.Write("[{0}]  ", a);
            Console.WriteLine("\nscore : {0}       Bet : {1:c}\n", this.player.score, this.player.Bet);
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
            if (this.Bet > this.Money)
            {
                Console.WriteLine("It's more than you have now.");
                this.Betting();
            }
            else this.Money -= this.Bet;
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
        public void AddMoney(string str)
        {
            if (str == "blackjack") this.Money += this.Bet * 2 + this.Bet / 2;
            else this.Money += this.Bet * 2;
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

            game.Board();

            player.Betting();

            player.Draw(cardDeck);
            dealer.Draw(cardDeck);
            player.Draw(cardDeck);
            dealer.Draw(cardDeck);

            game.InitialBoard();

            if (player.score == 21)
            {
                Console.WriteLine("Black Jack!");
                player.AddMoney("blackjack");
            }

            Console.Write("HIT or STAY : ");
            switch (Console.ReadLine())
            {
                case "HIT":
                    player.Hit(cardDeck);
                    break;
                case "hit":
                    player.Hit(cardDeck);
                    break;
                case "STAY":
                    break;
                case "stay":
                    break;
            }
            game.Board();
            if (player.score > 21)
            {
                Console.WriteLine("BUST!");
            }
            else
            {
                while (dealer.score <= 16)
                {
                    dealer.Draw(cardDeck);
                    game.Board();
                }
            }
        }
    }
}
