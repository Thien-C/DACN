using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlimeCPManager : MonoBehaviour
{
    [SerializeField] private Slime big_SlimePrefab;

    [SerializeField] private float slimeTimeSpawn;

    public List<GameObject> slimeList;
    private Timer timer = new Timer(5);

    void Start()
    {
        Spawn();
    }
    private void Update()
    {
        timer.Tick();
    }

    public void OpenSpawnSmallSlime()
    {
        timer.StartTimer();
    }

    public void AddSmallSlime(GameObject slime)
    {
        slimeList.Add(slime);
    }
    // Update is called once per frame

    public void Spawn() 
    {
        Slime slime = Instantiate(big_SlimePrefab, transform.position, Quaternion.identity);
        slime.Init(this);
    }
    private void HopNhatSlime()
    {
        if (slimeList == null)
            return;

        if (slimeList.Find(s => s.gameObject.activeInHierarchy == true) != null)
        {
            slimeList.ForEach(s => s.gameObject.SetActive(false));
            slimeList.Clear();
            Spawn();
        }
        
    }
}
