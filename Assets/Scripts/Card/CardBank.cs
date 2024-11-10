using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CardBank", menuName = "CardGame/Templates/CardBank")]
public class CardBank : ScriptableObject
{
    public string Name;
    public List<CardBankItem> Items=new List<CardBankItem>();
}
