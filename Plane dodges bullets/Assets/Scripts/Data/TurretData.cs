using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
public class TurretData
{
    public List<TurreInfo> turreInfos = new List<TurreInfo>();
}
public class TurreInfo
{
    //id
    //开火类型
    //子弹数量
    //开火间隙
    //组间休息
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;//1散弹 2顺序
    [XmlAttribute]
    public float cd;//每颗子弹间隔时间
    [XmlAttribute]
    public int num;//数量，该组子弹有多少颗
    [XmlAttribute]
    public float delay;//组间 间隔时间
    [XmlAttribute]
    public string ids;//关联子弹的id
}
