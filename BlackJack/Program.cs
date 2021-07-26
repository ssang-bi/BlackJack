using System;
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
    }
    class CardDeck
    {
        public List<card> deck = new List<card>();
        public Stack<card> GameDeck = new Stack<card>();
        public void Build()
        {
            for (int i = 0; i < 4; i++)
            {
                deck.Add(card.Ace);
                deck.Add(card.Two);
                deck.Add(card.Three);
                deck.Add(card.Four);
                deck.Add(card.Five);
                deck.Add(card.Six);
                deck.Add(card.Seven);
                deck.Add(card.Eight);
                deck.Add(card.Nine);
                deck.Add(card.Ten);
                deck.Add(card.Jack);
                deck.Add(card.Queen);
                deck.Add(card.King);
            }
            Tool.Shuffle(deck);
        }
        public void MakeGameDeck()
        {
            for (int i = 0; i < deck.Count; i++) GameDeck.Push(deck[i]);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CardDeck deck = new CardDeck();

            deck.Build();    
            foreach (card a in deck.deck) Console.WriteLine(a);
            Console.WriteLine("----------stack version------------");
            deck.MakeGameDeck();
        }
    }
}
