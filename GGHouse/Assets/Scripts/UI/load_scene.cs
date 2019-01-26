using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class load_scene : MonoBehaviour
{
    //public int level_to_load;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;
    public bool sceneEnding = false;
    public bool exit = false;
    public bool restart = false;
    private RawImage rawImage;
    private int this_level;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        this_level = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(SceneManager.sceneCountInBuildSettings);
    }
 
    void Update()
    {
        if (sceneStarting)
            StartScene();
        if (sceneEnding)
        {
            EndScene();
        }
    }
 
    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
 
    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }
 
    void StartScene()
    {
        FadeToClear();
        if (rawImage.color.a < 0.05f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }
 
    void EndScene()
    {
        rawImage.enabled = true;
        FadeToBlack();
        if (rawImage.color.a > 0.95f)
        {
            //restart
            if (restart) {
                SceneManager.LoadScene(this_level);
            }
            // final scene
            if (SceneManager.sceneCountInBuildSettings -1 == this_level || exit)
            {
                Application.Quit();
            }
            //next level
            else
            {
                SceneManager.LoadScene(this_level + 1);
            }
            
            

        }
    }

 
}
