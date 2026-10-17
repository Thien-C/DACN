using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource sound;
    [SerializeField] private float lifetime = 0f;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayAndDisable());
    }

    private IEnumerator PlayAndDisable()
    {
        sound.PlayOneShot(sound.clip);
        if (lifetime == 0f)
        {
            yield return new WaitForSeconds(sound.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(lifetime);
        }
        gameObject.SetActive(false);
    }
}
