using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    public GameObject toBeSpawned;
    float spawnPositionY, spawnPositionX;

    public void SpawnBox()
    {
        GameObject Box = Instantiate(toBeSpawned, new Vector3(spawnPositionX, spawnPositionY - 40, 0), Quaternion.identity) as GameObject;

        // set parent to canvas
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("spawncomponent").transform, false);

        // update spawn position
        spawnPositionY += 40;
    }
}
