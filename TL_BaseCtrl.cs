using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class TL_BaseCtrl : MonoBehaviour
{
    public bool DontFollow_Player;
    public Transform Ctrler_Pos;

    [Header("타임라인")]
    public PlayableDirector TimeLine;

    [Header("재생/일시정지 버튼")]
    public List<Button> PlayBtn;

    [Header("재생시간 슬라이더 / 시간txt")]
    public Slider TL_Slider;
    public TMP_Text PlayTime_txt;

    public float Delay_Lim;
    public float Delay;
    // Start is called before the first frame update

    private void Awake()
    {
        Ctrler_Pos = GameObject.Find("TL_Ctrl Pos").GetComponent<Transform>();
    }

    void Start()
    {
        PlayBtn[0].onClick.AddListener(Play_Btn);
        PlayBtn[1].onClick.AddListener(Pause_Btn);
    }

    // Update is called once per frame
    void Update()
    {
        Delay += Time.deltaTime;

        if (Delay >= Delay_Lim)
        {
            TL_Slider.value = (float)(TimeLine.time / TimeLine.duration);
            PlayTime_txt.text = TimeLine.time.ToString("N1") + " / " + TimeLine.duration.ToString("N1") + "Sec";
            Delay = 0;
        }


        if (DontFollow_Player == false)
        {
            if (Ctrler_Pos != null)
            {
                transform.position = Ctrler_Pos.position;
                transform.eulerAngles = Ctrler_Pos.eulerAngles;
            }
        }
    }

    // 플레이 버튼
    void Play_Btn()
    { TimeLine.Play(); }
    void Pause_Btn()
    { TimeLine.Pause(); }
    public void Click_Time()
    {
        TimeLine.time = (TL_Slider.value / TL_Slider.maxValue) * TimeLine.duration;
    }
}
