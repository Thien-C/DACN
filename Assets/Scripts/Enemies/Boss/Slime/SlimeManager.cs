//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SlimeManager : MonoBehaviour
//{
//    [SerializeField] SlimeCPManager slimeCPManager;
//    private List<Slime> slimeList;
//    private Timer timer = new Timer(5);

//    private void OnEnable()
//    {
//        timer.OnTimerDone += HopNhatSlime;
//    }

//    private void OnDisable()
//    {
//        timer.OnTimerDone -= HopNhatSlime;
//    }

//    public void AddSmallSlime(Slime slime)
//    {
//        slimeList.Add(slime);
//    }


//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    private void HopNhatSlime()
//    {
//        if (slimeList == null)
//            return;
//        if (slimeList.Find(s => s.gameObject.activeInHierarchy == true) != null)
//        {
//            slimeList.ForEach(s => s.gameObject.SetActive(false));
//            slimeList.Clear();
//            slimeCPManager.Spawn();
//        }
//    }
//}
