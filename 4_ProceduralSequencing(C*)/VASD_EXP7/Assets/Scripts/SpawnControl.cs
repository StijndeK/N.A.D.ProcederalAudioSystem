using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// spawns the boxes from which layer boxes can be spawned
public class SpawnControl : MonoBehaviour
{
    public GameObject toBeSpawned;
    float spawnPositionY = -70;

    public void SpawnBox()
    {
        GameObject Box = Instantiate(toBeSpawned, new Vector3(-150, spawnPositionY - 40, 0), Quaternion.identity) as GameObject;

        // set parent to canvas
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("spawncomponent").transform, false);

        // update spawn position
        spawnPositionY += 40;
    }
}
