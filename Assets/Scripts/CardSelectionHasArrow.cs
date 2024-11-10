using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CardSelectionHasArrow : CardSelectionBase
{
    private Vector3 previousClickPosition;
    private const float CardDetectionOffset = 2.2f;
    private const float CardAnimationTime = 0.2f;
    private const float SelectedCardYOffset = -1.0f;
    private const float AttackCardInMiddlePositionX = -15f;
    Sequence sq;
    public AttackArrow attackArrow;

    private void Update()
    {
        if (cardDisplayManager.IsMoving())
            return;
        if (Input.GetMouseButtonDown(0))
            DetectCardSelection();
        UpdateCardAndTargetingArrow();
        if (Input.GetMouseButtonUp(0))
        {
            DetectCardUnSelection();
        }
    }


    private void DetectCardSelection()
    {
        //检查玩家是否在卡牌的上方作了点击操作
        var mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward,Mathf.Infinity, cardLayer);
        if (hitInfo.collider != null)
        {
            selectedCard = hitInfo.collider.gameObject;
            selectedCard.GetComponent<SortingGroup>().sortingOrder += 10;//使该卡牌显示在最高层
            previousClickPosition =mousePosition;
        }
    }

    private void DetectCardUnSelection()
    {
        if (selectedCard == null)
            return;
        var curObj = selectedCard;
        selectedCard = null;
        attackArrow.UnableArrow();
        curObj.GetComponent<CardObject>().Reset(() => { sq = null; });
    }

    private void UpdateCardAndTargetingArrow()
    {
        if (selectedCard == null)
            return;
        var mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        float diffY = mousePosition.y- previousClickPosition.y;
        if(diffY > CardDetectionOffset&&sq==null)//拖拽大于一定距离激活,sq确保动画只播放一次
        {
            var position =selectedCard.transform.position;
            sq=DOTween.Sequence()
                .Append(selectedCard.transform.DOMove(new Vector3(AttackCardInMiddlePositionX, SelectedCardYOffset, position.z), CardAnimationTime))
                .Append(selectedCard.transform.DORotate(Vector3.zero, duration: CardAnimationTime))
                .OnComplete(() => { attackArrow.EnableArrow(selectedCard.transform.position); });
        }

        

    }
}
