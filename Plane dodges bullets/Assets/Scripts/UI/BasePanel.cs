using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;
    public static T Instance => instance;
    private void Awake()
    {
        instance = this as T;
    }
    public virtual void Start()
    {
        Init();
    }
    public abstract void Init();
    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
