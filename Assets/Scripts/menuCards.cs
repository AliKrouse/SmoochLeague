using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class menuCards : MonoBehaviour
{
    public int PLAYERNUMBER;
    public float pointerSpeed;
    private Player p;
    private GameObject pointer;
    private Text characterName;
    private GameObject cover;

    public bool isActive;

    public int character;

    public menuCharacters[] charList;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(PLAYERNUMBER);
        pointer = transform.GetChild(6).gameObject;
        characterName = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        cover = transform.GetChild(5).gameObject;
	}
	
	void Update ()
    {
        if (isActive)
        {
            cover.SetActive(false);
            pointer.SetActive(true);
        }
        else
        {
            cover.SetActive(true);
            pointer.SetActive(false);
        }

        if (isActive)
        {
            Vector2 stickMovement = new Vector2(p.GetAxis("Horizontal selection"), p.GetAxis("Vertical selection")) * pointerSpeed;
            pointer.transform.position = (Vector2)pointer.transform.position + stickMovement;

            if (p.GetButtonDown("A"))
            {
                for (int i = 0; i < charList.Length; i++)
                {
                    if (charList[i].GetComponent<Collider2D>().bounds.Contains(pointer.transform.position))
                    {
                        if (character != 0)
                        {
                            charList[character - 1].isSelected = false;
                        }
                        charList[i].isSelected = true;
                        character = i + 1;
                        characterName.text = charList[i].characterName;
                    }
                }
            }
        }
	}
}
