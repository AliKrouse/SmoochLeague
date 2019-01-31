using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    private Player p1;
    private Player[] allPlayers;
    public GameObject titleScreen, menuScreen, instScreen, playerScreen;

    public GameObject select1, select2;
    public Vector2[] selectorPoints;
    private int currentSelection;

    private bool acceptInput;
    
	void Start ()
    {
        p1 = ReInput.players.GetPlayer(0);
        allPlayers = new Player[8];
        for (int i = 0; i < 8; i++)
            allPlayers[i] = ReInput.players.GetPlayer(i);

        titleScreen.SetActive(true);
        menuScreen.SetActive(false);
        instScreen.SetActive(false);
        playerScreen.SetActive(false);
	}
	
	void Update ()
    {
        if (titleScreen.activeSelf)
        {
            if (p1.GetButtonDown("Select"))
            {
                menuScreen.SetActive(true);
                titleScreen.SetActive(false);
                StartCoroutine(freezeInput());
            }
        }

        if (menuScreen.activeSelf)
        {
            if (p1.GetAxis("Vertical selection") > float.Epsilon && acceptInput)
            {
                currentSelection--;
                StartCoroutine(freezeInput());
            }
            if (p1.GetAxis("Vertical selection") < -float.Epsilon && acceptInput)
            {
                currentSelection++;
                StartCoroutine(freezeInput());
            }
            if (currentSelection < 0)
                currentSelection = selectorPoints.Length - 1;
            if (currentSelection >= selectorPoints.Length)
                currentSelection = 0;

            select1.transform.localPosition = selectorPoints[currentSelection];
            select2.transform.localPosition = new Vector2(-selectorPoints[currentSelection].x, selectorPoints[currentSelection].y);

            if (p1.GetButtonDown("Select") && acceptInput)
            {
                if (currentSelection == 0)
                {
                    playerScreen.SetActive(true);
                    menuScreen.SetActive(false);
                    StartCoroutine(freezeInput());
                }
                if (currentSelection == 1)
                {
                    instScreen.SetActive(true);
                    menuScreen.SetActive(false);
                    StartCoroutine(freezeInput());
                }
                if (currentSelection == 2)
                {
                    Application.Quit();
                }

                StartCoroutine(freezeInput());
            }
        }

        if (instScreen.activeSelf)
        {

        }

        if (playerScreen.activeSelf)
        {
            if (Input.GetKey(KeyCode.Alpha2))
                activatePlayers.numberOfPlayers = 1;
            if (Input.GetKey(KeyCode.Alpha3))
                activatePlayers.numberOfPlayers = 2;
            if (Input.GetKey(KeyCode.Alpha4))
                activatePlayers.numberOfPlayers = 3;
            if (Input.GetKey(KeyCode.Alpha5))
                activatePlayers.numberOfPlayers = 4;
            if (Input.GetKey(KeyCode.Alpha6))
                activatePlayers.numberOfPlayers = 5;
            if (Input.GetKey(KeyCode.Alpha7))
                activatePlayers.numberOfPlayers = 6;
            if (Input.GetKey(KeyCode.Alpha8))
                activatePlayers.numberOfPlayers = 7;

            if (p1.GetButtonDown("Select") && acceptInput)
                SceneManager.LoadScene(1);
        }
	}

    private IEnumerator freezeInput()
    {
        acceptInput = false;
        yield return new WaitForSeconds(0.1f);
        acceptInput = true;
    }
}
