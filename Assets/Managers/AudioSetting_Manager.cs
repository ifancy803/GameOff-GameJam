using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
/// <summary>
/// 设置音频管理
/// </summary>
public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("音频混响器")]
    public AudioMixer mixer;

    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGM", value);
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", value);
    }
}
