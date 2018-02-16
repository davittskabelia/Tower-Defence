using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController _instance;

    public List<GameObject> ListOfSpawnPointers;

    // This is termine of dota :D (cd)
    private float cd = 0;
    private float maxCooldown = 0;
    private int toSpawn = 0;

    private GameObject localSkeleton;


    private float waveCooldown;
    private bool spawn;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Update()
    {
        if (!spawn)
        {
            waveCooldown -= Time.deltaTime;
            if (waveCooldown <= 0)
            {
                spawn = true;
            }
        }
        else
        {
            if (toSpawn > 0)
            {
                if (cd <= 0.000000001)
                {
                    toSpawn--;
                    cd = Random.Range(0, maxCooldown);
                    Instantiate(localSkeleton, ListOfSpawnPointers[toSpawn % ListOfSpawnPointers.Count].transform);
                }
                else
                {
                    cd -= Time.deltaTime;
                }
            }
        }

    }

    // Spawn skeletons
    public void Spawn(int count, float cooldown, GameObject skeleton, float waveCooldown)
    {
        spawn = false;
        this.waveCooldown = waveCooldown;

        toSpawn = count;
        maxCooldown = cooldown;
        localSkeleton = skeleton;
    }
}