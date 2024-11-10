using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionBase : MonoBehaviour
{
    protected Camera m_Camera;
    public LayerMask cardLayer;

    public CardDisplayManager cardDisplayManager;

    protected GameObject selectedCard;

    private void Start()
    {
        m_Camera = Camera.main;
    }
}
