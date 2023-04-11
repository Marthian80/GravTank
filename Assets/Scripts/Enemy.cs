using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject DeathFX;
    [SerializeField] GameObject HitVFX;    
    [SerializeField] int hitScore;
    [SerializeField] int hitPoints;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidbody();
        parentGameObject = GameObject.FindGameObjectWithTag("SpawnAtRuntime");        
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {        
        EnemyHit();
        
        if (hitPoints < 1)
        {
            DestroyEnemy();
        }        
    }

    private void UpdateScore()
    {
        scoreBoard.IncreaseScore(hitScore);
    }

    private void EnemyHit()
    {
        hitPoints--;
        PlayVFX(HitVFX);
    }

    private void DestroyEnemy()
    {
        UpdateScore();
        PlayVFX(DeathFX);
        Destroy(gameObject);
    }

    private void PlayVFX(GameObject vfxToPlay)
    {
        GameObject vfx = Instantiate(vfxToPlay, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }
}
