using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SnapTo : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject panel;

    public AudioSource[] placeSounds;


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + board.GetComponent<RectTransform>().anchoredPosition + panel.GetComponent<RectTransform>().anchoredPosition;

            System.Random rand = new System.Random();
            placeSounds[rand.Next(0, placeSounds.Length)].Play();
        }
    }
}
