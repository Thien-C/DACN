using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newChaseStateData", menuName = "Data/State Data/Chase State")]

public class D_ChaseState : ScriptableObject
{
    public float chaseSpeed = 6f;
    public float chaseTime = 2f;
}