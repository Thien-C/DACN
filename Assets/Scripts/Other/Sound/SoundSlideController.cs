using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlideController : MonoBehaviour
{
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
    }

    public void ChangeSoundVolume()
    {
        if (volumeSlider != null) //CheckNull
            SoundManager.Instance.ChangeSoundVolume(volumeSlider.value);
    }
}
