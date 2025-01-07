using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RoleData
{
   public List<RoleInfo> roles = new List<RoleInfo>(); 
}
public class RoleInfo
{
    [XmlAttribute]
   public int Hp;
    [XmlAttribute]
    public int Speed;
    [XmlAttribute]
    public int volume;
    [XmlAttribute]
    public string rPos;
    [XmlAttribute]
    public float scale;
}
