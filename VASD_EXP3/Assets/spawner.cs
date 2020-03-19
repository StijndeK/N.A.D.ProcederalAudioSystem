using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    public GameObject toBeSpawned;
    int spawnPositionY = -300;

    // TODO: scaling

    public void SpawnBox()
    {
        GameObject Box = Instantiate(toBeSpawned, new Vector3(-500, spawnPositionY, 0), Quaternion.identity) as GameObject;
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        spawnPositionY = spawnPositionY + 200;
    }
}
