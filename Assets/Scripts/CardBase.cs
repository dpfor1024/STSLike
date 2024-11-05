using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card",menuName ="CardGame/Templates/Card",order =0)]
public class CardBase:ScriptableObject
{
    public int ID;
    public string Name;
    public int Cost;
    public Sprite Sprite;
    public CardType CardType;
}
