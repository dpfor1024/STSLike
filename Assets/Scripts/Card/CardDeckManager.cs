using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{

    public CardDisplayManager CardDisplayManager;

    private List<RuntimeCard> _deck;
    private const int DeckCapacity = 30;
    private void Awake()
    {
        _deck = new List<RuntimeCard>(30);
    }

    public int LoadDeck(List<CardBase> list)
    {
        int count = 0;
        foreach (var item in list)
        {
            if (item == null)
                continue;
            var card = new RuntimeCard()
            {
                CardBase = item,
            };
            _deck.Add(card);
            count++;
        }
        return count;
    }

    public void ShuffleDeck()
    {
        _deck.Shuffle();
    }

    /// <summary>
    /// ³éÅÆ
    /// </summary>
    /// <param name="Amount"></param>
    public void DrawCard(int Amount)
    {
        if (_deck.Count >= Amount)//ÊýÁ¿×ã¹»
        {
            var drawCards=new List<RuntimeCard>(Amount);
            for(int i = 0; i < Amount; i++)
            {
                drawCards.Add(_deck[0]);
                _deck.RemoveAt(0);
            }

            CardDisplayManager.CreateHandcards(drawCards);
        }
    }
}
