using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private load_scene rawImage;
    // Start is called before the first frame update
    void Awake()
    {
        rawImage = GameObject.Find("Canvas").transform.Find("RawImage").GetComponent<load_scene>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        rawImage.restart = true;
        rawImage.sceneEnding = true;
    }
}
