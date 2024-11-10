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
        //�������Ƿ��ڿ��Ƶ��Ϸ����˵������
        var mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward,Mathf.Infinity, cardLayer);
        if (hitInfo.collider != null)
        {
            selectedCard = hitInfo.collider.gameObject;
            selectedCard.GetComponent<SortingGroup>().sortingOrder += 10;//ʹ�ÿ�����ʾ����߲�
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
        if(diffY > CardDetectionOffset&&sq==null)//��ק����һ�����뼤��,sqȷ������ֻ����һ��
        {
            var position =selectedCard.transform.position;
            sq=DOTween.Sequence()
                .Append(selectedCard.transform.DOMove(new Vector3(AttackCardInMiddlePositionX, SelectedCardYOffset, position.z), CardAnimationTime))
                .Append(selectedCard.transform.DORotate(Vector3.zero, duration: CardAnimationTime))
                .OnComplete(() => { attackArrow.EnableArrow(selectedCard.transform.position); });
        }

        

    }
}
