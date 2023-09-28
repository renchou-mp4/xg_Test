using Tools;
using UnityEngine;

public class TEST : MonoBehaviour
{
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //LogTool.LogClear();
        if (i >= 10) return;
        for (; i < 10; i++)
        {
            FileLogTool.Log(i.ToString());
        }
        FileLogTool.LogFormat("第{0}次输出！", i);
        FileLogTool.LogDispose();
    }
}
