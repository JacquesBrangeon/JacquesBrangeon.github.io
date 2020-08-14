using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    public GameObject Canvas;
    public GameObject PlayerDropZone;
    public PlayerManager PlayerManager;
    public GameObject Player;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isDraggable = true;
    private GameObject playerDropZone;
    private GameObject startParent;
    private Vector2 startPosition;

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        PlayerDropZone = GameObject.Find("PlayerDropZone");

        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        playerDropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        playerDropZone = null;
    }

    public void StartDrag()
    {
        if (!isDraggable) return;

        startPosition = transform.position;
        startParent = transform.parent.gameObject;
        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;

        isDragging = false;

        Player = PlayerManager.thisPlayer;

        if (Player.GetComponent<PlayerDisplay>().currentMana < gameObject.GetComponent<CardDisplay>().manaCost)
        {
            Debug.Log("You don't have enough mana to play this card yet");

            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);

            return;
        }

        if (isOverDropZone && PlayerManager.IsMyTurn)
        {
            transform.SetParent(PlayerDropZone.transform, false);
            isDraggable = false;

            PlayerManager.PlayCard(gameObject);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }

        Player.GetComponent<PlayerDisplay>().ManaChange(gameObject.GetComponent<CardDisplay>().manaCost);
    }
}
