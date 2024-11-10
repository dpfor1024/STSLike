using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;

public class CardSelectionNoArrow : CardSelectionBase
{
    private Vector3 originalCardPosition;
    private Quaternion originalCardRotation;
    private int originalcardSortingOrder;

    private void Update()
    {
        if (cardDisplayManager.IsMoving())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            DetectCardSelection();
        }
        UpdateSelectedCard();

        if (Input.GetMouseButtonUp(0))
        {
            DetectCardUnSelection();
        }

    }

    private void DetectCardSelection()
    {
        if (selectedCard != null)
            return;
        //检查玩家是否在卡牌的上方作了点击操作
        var mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, cardLayer);
        if (hitInfo.collider != null)
        {
            selectedCard = hitInfo.collider.gameObject;
            originalCardPosition = selectedCard.transform.position;
            originalCardRotation = selectedCard.transform.rotation;
            originalcardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
        }
    }

    private void DetectCardUnSelection()
    {
        if (selectedCard == null)
            return;
        var curObj = selectedCard;
        selectedCard = null;
        DOTween.Sequence()
            .Append(curObj.transform.DOMove(originalCardPosition, 0.2f))
            .Join(curObj.transform.DORotate(originalCardRotation.eulerAngles, 0.2f)).SetEase(Ease.OutBack).OnComplete(() => {
                curObj.GetComponent<SortingGroup>().sortingOrder = originalcardSortingOrder;
            });
    }

    private void UpdateSelectedCard()
    {
        if (selectedCard != null)
        {
            var mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedCard.transform.position= mousePosition;
        }
    }

}
