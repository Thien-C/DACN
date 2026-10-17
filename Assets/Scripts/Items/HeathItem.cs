using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Stats playerheal = collision.gameObject.GetComponentInChildren<Stats>();
            playerheal.Health.Increase(20);
        }
    }
}
