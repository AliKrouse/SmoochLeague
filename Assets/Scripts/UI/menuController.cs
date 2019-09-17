using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    private Player p1;
    private Player[] allPlayers;
    public GameObject title, titleText, titleBG, setPlayers;
    private Image titleI, titleBGI;
    private bool titleScreen, movingToMenu, menuScreen;

    public float fadeSpeed;

    private bool acceptInput;

    public RectTransform[] menuOptions;
    public RectTransform selector;
    public int currentSelection;
    
	void Start ()
    {
        p1 = ReInput.players.GetPlayer(0);
        allPlayers = new Player[8];
        for (int i = 0; i < 8; i++)
            allPlayers[i] = ReInput.players.GetPlayer(i);

        titleScreen = true;
        title.SetActive(true);
        titleText.SetActive(true);
        titleBG.SetActive(true);
        titleI = title.GetComponent<Image>();
        titleBGI = titleBG.GetComponent<Image>();

        setPlayers.SetActive(false);

        selector.anchoredPosition = new Vector2(20, menuOptions[currentSelection].anchoredPosition.y);

        acceptInput = true;
	}
	
	void Update ()
    {
        if (p1.GetAnyButtonDown() && titleScreen)
        {
            titleText.SetActive(false);
            title.GetComponent<Animator>().enabled = true;
            titleScreen = false;
            movingToMenu = true;
        }

        if (movingToMenu)
        {
            float s = title.transform.localScale.x;
            s += Time.deltaTime * (s*s);

            if (title.transform.rotation.z != 0)
            {
                float a = titleBGI.color.a;
                a -= Time.deltaTime * fadeSpeed;

                titleBGI.color = new Color(titleBGI.color.r, titleBGI.color.g, titleBGI.color.b, a);

                if (a <= 0)
                {
                    titleBG.SetActive(false);
                    movingToMenu = false;
                    menuScreen = true;
                }
            }
        }

        if (menuScreen)
        {
            if (p1.GetAxis("Vertical selection") < -float.Epsilon && acceptInput)
            {
                if (currentSelection < (menuOptions.Length - 1))
                    currentSelection++;

                StartCoroutine(freezeInput());
            }
            if (p1.GetAxis("Vertical selection") > float.Epsilon && acceptInput)
            {
                if (currentSelection > 0)
                    currentSelection--;

                StartCoroutine(freezeInput());
            }

            selector.anchoredPosition = new Vector2(20, menuOptions[currentSelection].anchoredPosition.y);

            if (p1.GetButtonDown("Select"))
            {
                MakeSelection();
            }
        }
	}

    void MakeSelection()
    {
        if (currentSelection == 0)
        {
            menuScreen = false;
            setPlayers.SetActive(true);
        }

        if (currentSelection == 1)
        {

        }

        if (currentSelection == 2)
        {

        }

        if (currentSelection == 3)
        {
            Application.Quit();
        }
    }

    private IEnumerator freezeInput()
    {
        acceptInput = false;
        yield return new WaitForSeconds(0.15f);
        acceptInput = true;
    }
}
