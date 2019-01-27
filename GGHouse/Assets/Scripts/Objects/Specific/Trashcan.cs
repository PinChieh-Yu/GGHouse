using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour, ISwitch
{
    private bool isFallDown;
    private int trashCount;

    [SerializeField]
    private Sprite stand;

    // Start is called before the first frame update
    void Start()
    {
        isFallDown = true;
        trashCount = 0;
    }

    public void Switch()
    {
        if (isFallDown)
        {
            isFallDown = false;
            GetComponent<SpriteRenderer>().sprite = stand;
            GetComponent<ObjectInfo>().Properties.Add(ObjectProperty.Container);
            GetComponent<Container>().OnPutIn += OnPutIn;
            GetComponent<BoxCollider2D>().offset = new Vector2(-4.2f, -2.7f);
            GetComponent<BoxCollider2D>().size = new Vector2(4f, 3f);
            transform.Find("hint").localPosition = new Vector3(-4.4f, 5f, 0f);
        }
    }

    private void OnPutIn(ObjectInfo objectInfo)
    {
        trashCount++;
        if (trashCount == 5)
        {
            GameManager.instance.FinishTask();
            GetComponent<ObjectInfo>().Properties.Remove(ObjectProperty.Container);
        }
    }
}
