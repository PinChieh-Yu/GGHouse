using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;
    public float acceptRange;

    private bool completed = false;

    void Update()
    {
        if (!completed)
        {
            if ((targetObject.transform.position - transform.position).magnitude <= acceptRange)
            {
                completed = true;
                targetObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -10) - 5;
                GameManager.instance.FinishTask();
            }
        }
    }
}
