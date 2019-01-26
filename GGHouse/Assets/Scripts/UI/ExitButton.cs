using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private load_scene rawImage;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GameObject.Find("Canvas").transform.Find("RawImage").GetComponent<load_scene>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClick()
    {
        rawImage.exit = true;
        rawImage.sceneEnding = true;
    }
}
