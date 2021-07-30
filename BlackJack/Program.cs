using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
        public static void Clear<T>(this IList<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++) list.RemoveAt(0);
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
            this.Deck = new List<card>();
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
            this.GameDeck = new Stack<card>(Deck);
            //foreach (card a in this.Deck) this.GameDeck.Push(a);
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
        public string Name = "Player";
        public int Money = new int();
        public int Bet = new int();
        public int score = new int();
        public List<card> Hands = new List<card>();
        public Player(string _name, int _money)
        {
            if (_name != "") this.Name = _name;
            this.Money = _money;
        }
        public void Betting()
        {
            Console.Write("Betting Amount(more than 2) : ");
            this.Bet = Convert.ToInt32(Console.ReadLine());
            if (this.Bet > this.Money)
            {
                Console.WriteLine("It's more than you have now.");
                this.Betting();
            }
            else if (this.Bet < 2)
            {
                Console.WriteLine("You need to bet at least 2");
                this.Betting();
            }
            else this.Money -= this.Bet;
        }
        public void Draw(CardDeck cardDeck)
        {
            card card = cardDeck.GameDeck.Pop();
            this.Hands.Add(card);
            this.score = Tool.Score(card, this.score);
            if (this.score > 21)
            {
                for (int i = 0; i < this.Hands.Count; i++)
                {
                    if (this.Hands[i] == card.Ace) this.score -= 10;
                }
            }
        }
        public void Hit(CardDeck cardDeck)
        {
            this.Draw(cardDeck);
        }
        public void AddMoney(string str)
        {
            switch (str)
            {
                case "blackjack":
                    this.Money += this.Bet * 2 + this.Bet / 2;
                    break;
                case "win":
                    this.Money += this.Bet * 2;
                    break;
                case "push":
                    this.Money += this.Bet;
                    break;
            }
        }
        public void result()
        {
            Console.WriteLine("\nyour money : {0:c}", this.Money);
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
            Console.Write("Insert your money : ");
            Player player = new Player(name, Convert.ToInt32(Console.ReadLine()));

            Game game = new Game(dealer, player);

            string run;
            cardDeck.Create();

            // test

           while (player.Money > 0)
            {
                if (cardDeck.GameDeck.Count <= 4) cardDeck.Create();

                Tool.Clear(player.Hands);
                Tool.Clear(dealer.Hands);

                player.Bet = player.score = dealer.score = 0;

                game.Board();

                player.Betting();

                player.Draw(cardDeck);
                dealer.Draw(cardDeck);
                player.Draw(cardDeck);
                dealer.Draw(cardDeck);

                game.InitialBoard();

                if (dealer.score == 21)
                {
                    if (player.score == 21) player.Money += player.Bet;
                    else Console.WriteLine("LOSE!");

                    player.result();

                    if (player.Money == 0) Console.WriteLine("You lost all your money.");
                    else
                    {
                        Console.WriteLine("\nDo you want play more?(yes or no)");
                        run = Console.ReadLine();
                        if (run == "no" || run == "NO") break;
                    }
                    continue;
                }

                if (player.score == 21)
                {
                    Console.WriteLine("Black Jack!");
                    player.AddMoney("blackjack");

                    player.result();

                    if (player.Money == 0) Console.WriteLine("You lost all your money.");
                    else
                    {
                        Console.WriteLine("\nDo you want play more?(yes or no)");
                        run = Console.ReadLine();
                        if (run == "no" || run == "NO") break;
                    }
                    continue;
                }
                Console.WriteLine("\n!Player turn!\n");

                while (true)
                {
                    Console.Write("HIT or STAY : ");
                    string str = Console.ReadLine();
                    switch (str)
                    {
                        case "HIT":
                            player.Hit(cardDeck);
                            game.InitialBoard();
                            break;
                        case "hit":
                            player.Hit(cardDeck);
                            game.InitialBoard();
                            break;
                        case "STAY":
                            break;
                        case "stay":
                            break;
                    }
                    if (player.score > 21 || str == "STAY" || str == "stay") break;
                }
                
                if (player.score > 21)
                {
                    Console.WriteLine("PLAYER BUST!");

                    player.result();

                    if (player.Money == 0) Console.WriteLine("You lost all your money.");
                    else
                    {
                        Console.WriteLine("\nDo you want play more?(yes or no)");
                        run = Console.ReadLine();
                        if (run == "no" || run == "NO") break;
                    }
                    continue;
                }
                else
                {
                    Console.WriteLine("\n!Dealer turn!\n");
                    Thread.Sleep(1000);
                    game.Board();
                    while (dealer.score <= 16)
                    {
                        dealer.Draw(cardDeck);
                        Thread.Sleep(1000);
                        game.Board();
                    }
                }

                if (dealer.score > 21)
                {
                    Console.WriteLine("DEALER BUST!");
                    player.AddMoney("win");
                }
                else
                {
                    if (dealer.score == player.score)
                    {
                        Console.WriteLine("PUSH!");
                        player.AddMoney("push");
                    }
                    else if (dealer.score > player.score) Console.WriteLine("LOSE!");
                    else
                    {
                        Console.WriteLine("WIN!");
                        player.AddMoney("win");
                    }
                }
                
                player.result();

                if (player.Money == 0) Console.WriteLine("You lost all your money.");
                else
                {
                    Console.WriteLine("\nDo you want play more?(yes or no)");
                    run = Console.ReadLine();
                    if (run == "no" || run == "NO") break;
                }
            }
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
