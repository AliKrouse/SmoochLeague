using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCards : MonoBehaviour
{
    public RectTransform[] cards;
    public Vector2[] cardPositions;

    public float cardWidth;
    public float cardSpeed;

    public RectTransform canvas;
    
	void Start ()
    {
        if (playerData.activePlayers < 2)
            playerData.activePlayers = 2;
        SetSizeAndPosition();
	}
	
	void Update ()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && playerData.activePlayers < 8)
        {
            playerData.activePlayers++;
            SetSizeAndPosition();
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && playerData.activePlayers > 2)
        {
            playerData.activePlayers--;
            SetSizeAndPosition();
        }

        for (int i = 0; i < 8; i++)
        {
            cards[i].sizeDelta = Vector2.MoveTowards(cards[i].sizeDelta, new Vector2(cardWidth, cards[i].sizeDelta.y), Time.deltaTime * cardSpeed);
            cards[i].transform.GetChild(5).GetComponent<RectTransform>().sizeDelta = cards[i].sizeDelta;

            if (i == 0)
                cards[i].localPosition = Vector2.MoveTowards(cards[i].localPosition, cardPositions[i], Time.deltaTime * (cardSpeed * i));
            if (i > 0)
                cards[i].localPosition = Vector2.MoveTowards(cards[i].localPosition, new Vector2(cardPositions[i].x - 400, cardPositions[i].y), Time.deltaTime * (cardSpeed * i));
        }
    }

    void SetSizeAndPosition()
    {
        cardWidth = canvas.sizeDelta.x / playerData.activePlayers;

        cardPositions = new Vector2[8];
        for (int i = 0; i < 8; i++)
        {
            cardPositions[i] = new Vector2(cardWidth * i, cards[i].localPosition.y);
        }
    }
}
