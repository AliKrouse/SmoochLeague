using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePlayers : MonoBehaviour
{
    public static int numberOfPlayers;
    public GameObject[] players;
    public List<Transform> spawnPoints;
    
	void Start ()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i <= numberOfPlayers)
            {
                players[i].SetActive(true);
                int p = Random.Range(0, spawnPoints.Count);
                players[i].transform.position = spawnPoints[p].position;
                spawnPoints.Remove(spawnPoints[p]);
            }
        }
	}
}
