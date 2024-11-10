using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

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
    [SerializeField]
    private SortingGroup _sortingGroup;

    public CardBase Card;
    public RuntimeCard RuntimeCard;


    private Vector3 _savedPosition;
    private Quaternion _savedRotation;
    private int _savedSortingOrder;

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


    public void SaveTransform(Vector3 position, Quaternion rotation)
    {
        _savedPosition = position;
        _savedRotation = rotation;
        _savedSortingOrder=_sortingGroup.sortingOrder;
    }

    public void Reset(Action action=null)
    {
        DOTween.Sequence()
            .Append(transform.DOMove(_savedPosition, 0.2f))
            .Join(transform.DORotate(_savedRotation.eulerAngles, 0.2f)).SetEase(Ease.OutBack).OnComplete(() => {
                _sortingGroup.sortingOrder = _savedSortingOrder;
                action?.Invoke();
            });
    }
}
