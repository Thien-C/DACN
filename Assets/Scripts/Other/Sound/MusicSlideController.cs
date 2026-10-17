using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlideController : MonoBehaviour
{
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
    }

    public void ChangeMusicVolume()
    {
        if (volumeSlider != null) //CheckNull
            SoundManager.Instance.ChangeMusicVolume(volumeSlider.value);
    }
}
