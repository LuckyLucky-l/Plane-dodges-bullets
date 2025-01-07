using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkSound : MonoBehaviour
{
    private static BkSound instance;
    public static BkSound Instance => instance;
    public AudioSource soundSource;
    private void Awake()
    {
        instance = this;
        //初始化
        soundSource = this.GetComponent<AudioSource>();

        changeSound(GameDataMgr.Instance.musicData.changSound);
        isOpenSound(GameDataMgr.Instance.musicData.isSoundOpen);
    }
    private BkSound()
    {

    }
    /// <summary>
    /// 改变音效大小
    /// </summary>
    public void changeSound(float value)
    {
        soundSource.volume = value;
    }
    /// <summary>
    /// 音效开关
    /// </summary>
    public void isOpenSound(bool value)
    {
        soundSource.mute = !value;
    }
}
