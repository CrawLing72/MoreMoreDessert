using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonController : MonoBehaviour
{
    public Slider Slider;

    public void SettingBGM()
    {
        GameSoundManager.instance.BGM_Source.volume = Slider.value;
    }

    public void SettingUI()
    {
        GameSoundManager.instance.UI_Source.volume = Slider.value;
        GameSoundManager.instance.Effect_Source.volume = Slider.value;
    }

    public void SettingVoice()
    {
        GameSoundManager.instance.Voice_Source.volume = Slider.value;
    }
    private void Awake()
    {
        Slider = gameObject.transform.GetChild(2).GetComponent<Slider>();
    }
}
