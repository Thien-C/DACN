using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private GameObject[] spawn;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private ParticleManager particleManager;
    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private Stats stats;

    public void Die()
    {
        string typeEnemyName = core.transform.parent.name.ToLower();

        // Hiệu ứng particle khi chết
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticle(particle);
        }

        // Logic spawn khi chết cua boss slime
        if (typeEnemyName.Contains("slime"))
        {
            Slime s = core.transform.parent.GetComponent<Slime>();
            foreach (var spawns in spawn)
            {
                s.SpawnSlime(spawns.GetComponent<Slime>(), 3, 1);
            }
        }
        else
        {
            foreach (var spawns in spawn)
            {
                ParticleManager.SpawnObjectsInMultipleDirections(spawns, 3, 1);
            }
        }

        // Kiểm tra nếu là Player
        if (core.transform.parent.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                core.transform.parent.gameObject.SetActive(false);
                GameManager.Instance.PlayerDie();
            }
        }
        else
        {
            //Quest.Instance.EnemyKilled();
            Destroy(core.transform.parent.gameObject); // Nếu là enemy, chỉ destroy
        }
    }

    private void OnEnable()
    {
        Stats.Health.OnCurrentValueZero += Die;
    }

    private void OnDisable()
    {
        Stats.Health.OnCurrentValueZero -= Die;
    }
}