using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObj : MonoBehaviour
{
    [SerializeField] Vector2 StarPos;
    [SerializeField] Vector2 EndPos;
    [SerializeField] float MoveSpeed;
    Color color;

    void Awake()
    {
        Debug.Log(this.transform.localPosition);
    }

    void Start()
    {
        if (this.gameObject.GetComponent<Text>() == null)
        {
            color = this.gameObject.GetComponent<Image>().color;
        }

        if (this.gameObject.GetComponent<Image>() == null)
        {
            color = this.gameObject.GetComponent<Text>().color;
        }
        color.a = 0;

        this.transform.localPosition = StarPos;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, EndPos, MoveSpeed * Time.deltaTime);

        if (color.a < 1.0f)
        {
            color.a += 0.01f;
            if (this.gameObject.GetComponent<Text>() == null)
            {
                this.gameObject.GetComponent<Image>().color = color;
            }
            else if (this.gameObject.GetComponent<Image>() == null)
            {
                this.gameObject.GetComponent<Text>().color = color;
            }
        }
    }
}
