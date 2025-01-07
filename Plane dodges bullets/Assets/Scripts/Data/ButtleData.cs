using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ButtleData
{
    public List<ButtleInfo> buttles = new List<ButtleInfo>();
}
public class ButtleInfo
{
    [XmlAttribute]
    public int id;//id
    [XmlAttribute]
    public int type;//飞行类型
    [XmlAttribute]
    public float XmoveSpeed;//x轴移动速度
    [XmlAttribute]
    public float ZmoveSpeed;//轴移动速度
    [XmlAttribute]
    public float rotateSpeed;
    [XmlAttribute]
    public string resName;//资源路径
    [XmlAttribute]
    public string deadEff;//死亡特效路径
    [XmlAttribute]
    public float lifeTime;//生命周期
}
