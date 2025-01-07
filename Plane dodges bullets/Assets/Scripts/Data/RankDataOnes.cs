using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankDataOnes : MonoBehaviour
{
    public UILabel LabpaiMing;
    public UILabel Labname;
    public UILabel Labtime;
    //提供给外部修改信息的接口
    public void setInfo(int paiMing,string name,int time)
    {
        print(time);
        LabpaiMing.text = paiMing.ToString();
        Labname.text = name;
        string str = "";
        if (time/3600>0)
        {
            str+= time / 3600+"h";
        }
        if (time % 3600/60 > 0 || str!= "")
        {
            str+= time % 3600/60 + "m";
        }
        if (time % 60>0)
        {
            str += time % 60 + "s";
        }

        Labtime.text = str;
    }
}
