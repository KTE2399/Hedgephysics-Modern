﻿using UnityEngine;
using System.Collections;

public class EnemySpawnerEternal : MonoBehaviour {

    public float Distance;
    Transform Player;

    public float RespawnTime;
    float counter;

    public GameObject Enemy;
    public bool HasSpawned { get; set; }
    public GameObject TeleportSparkle;


    void Start()
    {
        HasSpawned = false;
        Destroy(GetComponent<MeshRenderer>());
        counter = RespawnTime;
    }

    void LateUpdate()
    {
        if (!Player)
        {
            Player = FindObjectOfType<PlayerBhysics>().transform;
            return;
        }
        counter += Time.deltaTime;
        if (Vector3.Distance(Player.position, transform.position) < Distance)
        {
            if (!HasSpawned)
            {
                if (counter > RespawnTime)
                {
                    SpawnInNormal();
                }
            }
        }
    }

    void SpawnInNormal()
    {
        HasSpawned = true;
        Instantiate(TeleportSparkle, transform.position, transform.rotation);
        GameObject em = (GameObject)Instantiate(Enemy, transform.position, transform.rotation,transform);
        em.GetComponent<EnemyHealth>().SpawnReference = this;
        HomingAttackControl.UpdateHomingTargets();
    }

    public void ResartSpawner()
    {
        HasSpawned = false;
        counter = 0;
    }
}
