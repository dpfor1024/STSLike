using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDisplayManager : MonoBehaviour
{
    private const int PositionNumber = 20;
    private const int RotationNumber = 20;
    private const int SortingOrdersNumber = 20;

    private CardManager cardManager;


    private List<Vector3> _pos;
    private List<Quaternion> _rot;
    private List<int> _sort;

    private readonly List<GameObject> _handCards = new(PositionNumber);

    private const float Radius = 16.0f;

    private readonly Vector3 _centerPos = new Vector3(-15.0f, -18.5f, 0);
    private readonly Vector3 _originCardScale = new Vector3(0.5f, 0.5f, 1f);


    private bool isCardMoveing;

    public void Init(CardManager manager)
    {
        cardManager = manager;
    }

    private void Start()
    {
        _pos = new List<Vector3>(PositionNumber);
        _rot = new List<Quaternion>(RotationNumber);
        _sort = new List<int>(SortingOrdersNumber);
    }



    public void CreateHandcards(List<RuntimeCard> cardsInHand)
    {
        var drawnCards = new List<GameObject>(cardsInHand.Count);
        foreach (var card in cardsInHand)
        {
            var cardGameObject = CreateCardGameObject(card);
            drawnCards.Add(cardGameObject);
            _handCards.Add(cardGameObject);
        }
        PutDeckCardToHand(drawnCards);

    }

    private GameObject CreateCardGameObject(RuntimeCard runtimeCard)
    {
        var gameObject = cardManager.GetInstance();
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.localScale = Vector3.zero;
        var obj = gameObject.GetComponent<CardObject>();
        obj.SetInfo(runtimeCard);
        return gameObject;
    }

    private void PutDeckCardToHand(List<GameObject> drawCard)
    {
        isCardMoveing = true;
        OrganizeHandCards();
        float interval = 0.0f;
        const float time = 0.5f;
        for (int i = 0; i < _handCards.Count; i++)
        {
            int j = i;
            var card = _handCards[i];
            if (drawCard.Contains(card))
            {
                var obj = card.GetComponent<CardObject>();
                var seq = DOTween.Sequence();
                seq.AppendInterval(interval);
                seq.AppendCallback(() =>
                {
                    var move = card.transform.DOMove(_pos[j], time);
                    card.transform.DORotateQuaternion(_rot[j], time);
                    card.transform.DOScale(_originCardScale, time);
                    if (j == _handCards.Count - 1)
                    {
                        move.OnComplete(() =>
                        {
                            isCardMoveing = false;
                        });
                    }
                });

                card.GetComponent<SortingGroup>().sortingOrder = _sort[i];
                interval += 0.2f;
            }
        }

        

    }



    private void OrganizeHandCards()
    {
        _pos.Clear();
        _rot.Clear();
        _sort.Clear();
        const float angle = 5.0f;
        float cardAngle = (_handCards.Count - 1) * angle / 2;
        var z = 0.0f;
        for (int i = 0; i < _handCards.Count; i++)
        {

            // Rotate
            var rotation = Quaternion.Euler(0, 0, cardAngle - i * angle);
            _rot.Add(rotation);

            // Move
            z -= 0.1f;
            var position = CalculateCardPosition(cardAngle - i * angle);
            position.z = z;
            _pos.Add(position);

            _sort.Add(i);
        }

    }

    private Vector3 CalculateCardPosition(float angle)
    {
        return new Vector3(
                _centerPos.x - Radius * Mathf.Sin(Mathf.Deg2Rad * angle),
                _centerPos.y + Radius * Mathf.Cos(Mathf.Deg2Rad * angle),
                0
            );
    }
}
