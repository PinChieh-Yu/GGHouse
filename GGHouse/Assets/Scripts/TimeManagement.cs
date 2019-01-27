using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManagement : MonoBehaviour
{

    public Text TimeCountDown;  //UI 的關卡時間倒數
    public float Time_Level1;  //關卡的時間限制
    public Slider Slider_Time;
    public Image Image_Color;
    public GameObject GameOverImage;
    public bool pause;

    public Animation anim;

    public float WarningTime;   //剩下幾秒時開始有黃色提示
    public float EmergencyTime;//剩下幾秒時開始有洪色提示

    private void Start()
    {
        Slider_Time.maxValue = Time_Level1;
        anim = TimeCountDown.GetComponent<Animation>();

    }

    void Update()
    {
        
        if(Time_Level1 > 0)
        {
            if (!pause)
            {
                Time_Level1 -= Time.deltaTime;
            }
            
        }
        else
        {
            GameOverImage.SetActive(true);
            //anim.Stop("Text_Flashing");
        }
        
        TimeCountDown.text = Mathf.RoundToInt(Time_Level1) + "";

        Slider_Time.value = Time_Level1;

        if (Time_Level1 < EmergencyTime)
        {
            TimeCountDown.color = Color.red;
            Image_Color.color = Color.red;
            //anim.Play("Text_Flashing");
        }
        else if(Time_Level1 < WarningTime)
        {
            Image_Color.color = Color.yellow;
        }

    }
}
