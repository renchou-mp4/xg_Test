using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string s = PathTool.GetFileNameByPath("D:/SeaFileDownload/Seafile/动作/Spine/hero/赛季英雄/X赛季英雄/alvaro.png");
        Debug.Log(s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
