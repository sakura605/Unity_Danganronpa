using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField] Color colorWhite;
    [SerializeField] Color colorBalck;
    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeSlowSpeed;

    public static bool isfinished = false;

    Image image;
    Transform Camera;
    [SerializeField] float MoveSpeed;

    void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        image = GameObject.Find("Img_Screen").GetComponent<Image>();
    }

    public IEnumerator FadeOut(bool _isWhite, bool _isSlow)
    {
        if (Camera.transform.position != new Vector3(0, 0, - 10))
            Camera.transform.position = new Vector3(0, 0, - 10);

        Color t_Color = (_isWhite == true) ? colorWhite : colorBalck;
        t_Color.a = 0;

        image.color = t_Color;

        while (t_Color.a < 1)
        {
            t_Color.a += (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            image.color = t_Color;
            yield return null;
        }

        isfinished = true;
    }

    public IEnumerator FadeIn(bool _isWhite, bool _isSlow)
    {
        if (Camera.transform.position != new Vector3(0, 0, - 10))
            Camera.transform.position = new Vector3(0, 0, - 10);

        Color t_Color = (_isWhite == true) ? colorWhite : colorBalck;
        t_Color.a = 1;

        image.color = t_Color;

        while (t_Color.a > 0)
        {
            t_Color.a -= (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            image.color = t_Color;
            yield return null;
        }
        isfinished = true;
    }

    public IEnumerator MovePos(Vector3 TargetPos, bool _isWhite, bool _isSlow)
    {
        if (Camera.transform.position != new Vector3(0, 0, - 10))
            Camera.transform.position = new Vector3(0, 0, - 10);

        Color t_Color = (_isWhite == true) ? colorWhite : colorBalck;
        t_Color.a = 1;

        image.color = t_Color;

        while (t_Color.a > 0)
        {
            t_Color.a -= (_isSlow == true) ? fadeSlowSpeed : fadeSpeed;
            image.color = t_Color;
            yield return null;
        }

        while (Camera.position != TargetPos)
        {
            //OriginPos.y += MoveSpeed;
            //Camera.position = new Vector3(0, OriginPos.y, -10);

            Camera.position = Vector3.MoveTowards(Camera.position, TargetPos, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        TargetPos = new Vector3(0, 0, -10);
        isfinished = true;
    }

}
