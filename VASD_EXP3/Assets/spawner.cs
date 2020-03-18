using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject toBeSpawned;
    int spawnPositionX = -700;

    // TODO: scaling

    public void SpawnBox()
    {
        GameObject Box = Instantiate(toBeSpawned, new Vector3(spawnPositionX, -300, 0), Quaternion.identity) as GameObject;
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        spawnPositionX = spawnPositionX + 200;
    }
}
