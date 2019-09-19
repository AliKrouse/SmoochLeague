using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePlayers : MonoBehaviour
{
    public playerController[] players;
    public List<Transform> spawnPoints;
    
	void Start ()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i <= playerData.activePlayers)
            {
                players[i].gameObject.SetActive(true);
                players[i].characterNumber = playerData.characterChoice[i];
                players[i].abilityNumber = playerData.abilityChoice[i];
                int p = Random.Range(0, spawnPoints.Count);
                players[i].transform.position = spawnPoints[p].position;
                spawnPoints.Remove(spawnPoints[p]);
            }
        }
	}
}
