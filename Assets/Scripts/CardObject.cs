using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _cost;
    [SerializeField]
    private TextMeshPro _name;
    [SerializeField]
    private TextMeshPro _description;
    [SerializeField]
    private TextMeshPro _type;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public CardBase Card;
    public RuntimeCard RuntimeCard;

    private void Start()
    {
        RuntimeCard = new RuntimeCard()
        {
            CardBase = Card,
        };
        SetInfo(RuntimeCard);
    }


    public void SetInfo(RuntimeCard runtime)
    {
        RuntimeCard = runtime;
        Card = runtime.CardBase;
        _cost.text=Card.Cost.ToString();
        _name.text = Card.Name;
        _type.text=Card.CardType.TypeName;
        _spriteRenderer.sprite = Card.Sprite;
    }
}
