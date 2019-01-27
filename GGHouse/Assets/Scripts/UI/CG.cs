using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG : MonoBehaviour
{
    public bool start = false;
    private bool next_scene;

    private int cg_num;
    private int cg_toshow;
    private Transform[] cgs;
    private load_scene load;
    public TimeManagement timer;
    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioSource cg_bgm;

    private Vector2 disable = new Vector2(0, 0);
    private Vector2 enable = new Vector2(1, 1);
    public bool state = false;
    private bool first = true;

    private void show()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cg_toshow >= cg_num)
            {
                if (!next_scene)
                {
                    cgs[cg_num - 1].localScale = disable;
                }
                state = true;
                return;
            }
            if (cg_toshow != 0)
            {
                cgs[cg_toshow - 1].localScale = disable;
            }
            if (cg_toshow < cg_num) {
                cgs[cg_toshow].localScale = enable;
            }
            cg_toshow++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
        GameObject canvas = GameObject.Find("Canvas");
        next_scene = !start;
        if (next_scene)
        {
            load = canvas.transform.Find("RawImage").GetComponent<load_scene>();
        }
        timer = canvas.GetComponent<TimeManagement>();
        cg_bgm = canvas.GetComponent<AudioSource>();
        bgm = GameObject.Find("Background").GetComponent<AudioSource>();
        //cg_bgm.Pause();
        if (start)
        {
            timer.pause=true;
        }
        cg_toshow = 0;
        cg_num = transform.childCount;
        cgs = new Transform[cg_num];
        for(int i = 0; i < cg_num; i++)
        {
            cgs[i] = transform.GetChild(i);
            cgs[i].localScale = disable;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!state && start)
        {
            if (first)
            {
                cgs[0].parent.localScale = enable;
                cgs[0].localScale = enable;
                cg_toshow = 1;
                first = false;
                Debug.Log(gameObject.name + "pause");
                timer.pause = true;
                bgm.Pause();
                cg_bgm.UnPause();
            }
            show();
        }
        else
        {
            if (next_scene)
            {
                load.sceneEnding = true;
            }
            else
            {
                timer.pause = false;
                cg_bgm.Pause();
                if (!bgm.isPlaying)
                {
                    bgm.Play();
                }
                this.enabled = false;
            }
            
        }
    }
}
