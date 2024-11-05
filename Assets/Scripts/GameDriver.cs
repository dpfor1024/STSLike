using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    public CardBank StartDeck;//ÆðÊ¼ÅÆ

    [Header("Manager")]
    [SerializeField] private CardManager cardManager;
    
    private List<CardBase> _playerDeck = new List<CardBase>();

    private void Start()
    {
        cardManager.Init();
        CreatePlayer();
        _playerDeck.Shuffle();
    }

    private void CreatePlayer()
    {
        foreach (var item in StartDeck.Items) { 
            for(int i = 0; i < item.Amount; i++)
            {
                _playerDeck.Add(item.CardBase);
            }
        }
    }
}
