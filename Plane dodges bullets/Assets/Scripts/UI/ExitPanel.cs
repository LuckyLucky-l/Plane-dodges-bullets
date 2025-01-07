using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitPanel : BasePanel<ExitPanel>
{
    public UIButton btnRight;
    public UIButton btnWrong;

    public override void Init()
    {
        btnWrong.onClick.Add(new EventDelegate(()=> 
        {
            HideMe();
            Time.timeScale = 1;
        }));
        btnRight.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("BeginScence");
            Time.timeScale = 1;
        }));
        HideMe();
    }
}
