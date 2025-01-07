using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic instance;
    public static BkMusic Instance => instance;
    public AudioSource audioSource;
    /// <summary>
    /// 第一次打开时初始化音乐
    /// </summary>
    private void Awake()
    {
        instance = this;
        audioSource = this.GetComponent<AudioSource>();
        ismuteMusic(GameDataMgr.Instance.musicData.isMusicOpen);
        changValueMusic(GameDataMgr.Instance.musicData.changMusic);
    }
    private BkMusic()
    {

    }
    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="ismute"></param>
    public void ismuteMusic(bool ismute)
    {
        audioSource.mute = !ismute;
    }
    public void changValueMusic(float value)
    {
        audioSource.volume = value;
    }
}
