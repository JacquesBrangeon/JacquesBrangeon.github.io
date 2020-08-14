using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject EnemyArea;
    public GameObject ZoomCard;

    public bool CanZoom = true;

    private GameObject newZoomCard;

    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
        EnemyArea = GameObject.Find("EnemyArea");
    }

    public void OnHoverEnter()
    {
        if (!CanZoom) return;
        // Allow hovering only when not in enemy hand
        if (gameObject.transform.parent != EnemyArea.transform)
        {
            Card cardHovered = gameObject.GetComponent<CardDisplay>().card;

            newZoomCard = Instantiate(ZoomCard, new Vector2(-650, 0), Quaternion.identity) as GameObject;
            newZoomCard.transform.SetParent(Canvas.transform, false);
            newZoomCard.layer = LayerMask.NameToLayer("Zoom");

            newZoomCard.GetComponent<PopulateZoom>().Fill(cardHovered);
        }
    }

    public void OnHoverExit()
    {
        Destroy(newZoomCard);
    }
}
