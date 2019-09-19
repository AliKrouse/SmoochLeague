using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public class playerCards : MonoBehaviour
{
    private Player p;

    public RectTransform[] cards;
    public Vector2[] cardPositions;

    public float cardWidth;
    public float cardSpeed;

    public RectTransform canvas;

    public bool[] playerIsReady;

    public GameObject gameStartBox;
    public Text countdown;

    private bool gameIsStarting;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(0);

        if (playerData.activePlayers < 2)
            playerData.activePlayers = 2;
        SetSizeAndPosition();
	}
	
	void Update ()
    {
        if (p.GetButtonDown("Add player") && playerData.activePlayers < 8 && !gameIsStarting)
        {
            playerData.activePlayers++;
            SetSizeAndPosition();
        }

        if (p.GetButtonDown("Drop player") && playerData.activePlayers > 2 && !gameIsStarting)
        {
            playerData.activePlayers--;
            SetSizeAndPosition();

            CheckForReadyPlayers();
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(startGame());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameIsStarting)
        {
            StopAllCoroutines();
            gameStartBox.SetActive(false);
            countdown.text = "3";
        }
    }

    public void CheckForReadyPlayers()
    {
        float numberReady = 0;

        for (int i = 0; i < 9; i++)
        {
            if ((playerData.activePlayers - 1) < i)
                playerIsReady[i] = false;

            if ((playerData.activePlayers - 1) >= i)
            {
                if (playerIsReady[i])
                    numberReady++;
            }
        }

        if (numberReady == playerData.activePlayers)
            StartCoroutine(startGame());
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

    private IEnumerator startGame()
    {
        gameIsStarting = true;
        gameStartBox.SetActive(true);
        gameStartBox.GetComponent<Animator>().enabled = true;
        countdown.text = "3";
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        yield return new WaitForSeconds(1);
        countdown.text = "STARTING...";
        SceneManager.LoadScene(1);
    }
}
