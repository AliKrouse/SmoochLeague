using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class pCardIndividual : MonoBehaviour
{
    private Player p;
    public int PLAYERNUMBER;
    public int characterChoice;
    private Image i;
    private Text charName;
    private Animator upArrow, downArrow;

    private bool acceptInput;

    public string[] names;
    public Sprite[] chars;

    private GameObject abilitySelect;
    private Image aImage;
    private Text aName, aDescription;
    private Animator aUp, aDown;
    public int abilityChoice;

    public string[] abilityNames;
    public string[] abilityDescriptions;
    public Sprite[] abilities;

    private GameObject ready;
    private Text abilityChosen;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(PLAYERNUMBER);

        i = transform.GetChild(2).GetComponent<Image>();
        charName = transform.GetChild(3).GetChild(2).GetComponent<Text>();
        upArrow = transform.GetChild(3).GetChild(1).GetComponent<Animator>();
        downArrow = transform.GetChild(3).GetChild(0).GetComponent<Animator>();

        acceptInput = true;

        characterChoice = Random.Range(0, 8);
        ChangeCharacter();

        abilitySelect = transform.GetChild(4).gameObject;
        aImage = abilitySelect.transform.GetChild(1).GetComponent<Image>();
        aName = abilitySelect.transform.GetChild(2).GetComponent<Text>();
        aDescription = abilitySelect.transform.GetChild(3).GetComponent<Text>();
        aUp = abilitySelect.transform.GetChild(4).GetComponent<Animator>();
        aDown = abilitySelect.transform.GetChild(5).GetComponent<Animator>();

        ChangeAbility();

        ready = transform.GetChild(1).gameObject;
        abilityChosen = transform.GetChild(3).GetChild(3).GetComponent<Text>();
	}
	
	void Update ()
    {
        if (p.GetAxis("Vertical selection") < -float.Epsilon && acceptInput && !ready.activeSelf)
        {
            if (!abilitySelect.activeSelf)
            {
                characterChoice++;
                if (characterChoice > 7)
                    characterChoice = 0;

                downArrow.SetTrigger("activate");
                ChangeCharacter();
            }
            else
            {
                abilityChoice++;
                if (abilityChoice >= abilities.Length)
                    abilityChoice = 0;

                aDown.SetTrigger("activate");
                ChangeAbility();
            }

            StartCoroutine(freezeInputs());
        }

        if (p.GetAxis("Vertical selection") > float.Epsilon && acceptInput && !ready.activeSelf)
        {
            if (!abilitySelect.activeSelf)
            {
                characterChoice--;
                if (characterChoice < 0)
                    characterChoice = 7;

                upArrow.SetTrigger("activate");
                ChangeCharacter();
            }
            else
            {
                abilityChoice--;
                if (abilityChoice < 0)
                    abilityChoice = abilities.Length - 1;

                aUp.SetTrigger("activate");
                ChangeAbility();
            }

            StartCoroutine(freezeInputs());
        }

        if (p.GetButtonDown("Select") && !ready.activeSelf)
        {
            if (!abilitySelect.activeSelf)
            {
                playerData.characterChoice[PLAYERNUMBER] = characterChoice;
                upArrow.gameObject.SetActive(false);
                downArrow.gameObject.SetActive(false);
                abilitySelect.SetActive(true);
            }
            else
            {
                playerData.abilityChoice[PLAYERNUMBER] = abilityChoice;
                abilitySelect.SetActive(false);
                abilityChosen.text = abilityNames[abilityChoice];
                abilityChosen.gameObject.SetActive(true);
                ready.SetActive(true);
                transform.parent.GetComponent<playerCards>().CheckForReadyPlayers();
            }
        }
	}

    void ChangeCharacter()
    {
        charName.text = names[characterChoice];
        i.sprite = chars[characterChoice];
    }

    void ChangeAbility()
    {
        aName.text = abilityNames[abilityChoice];
        aDescription.text = abilityDescriptions[abilityChoice];
        aImage.sprite = abilities[abilityChoice];
    }

    private IEnumerator freezeInputs()
    {
        acceptInput = false;
        yield return new WaitForSeconds(0.25f);
        acceptInput = true;
    }
}
