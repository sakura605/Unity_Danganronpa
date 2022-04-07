using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueEndBGController : MonoBehaviour
{
    public GameObject Pologue_End_Arrow;
    public GameObject Pologue_End_Txt;
    public GameObject Pologue_End_Castle;
    public GameObject Panel;

    Vector2 Arrow_TargetPos = new Vector2(-50, -327);
    Vector2 Txt_TargetPos = new Vector2(-510, -195);
    Vector2 Castle_TargetPos = new Vector2(0, 50);
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(true);
        Pologue_End_Arrow.transform.localPosition = new Vector2(-1500, -327);
        Pologue_End_Txt.transform.localPosition = new Vector2(-1470, -93);
        Pologue_End_Castle.transform.localPosition = new Vector2(500, 50);

        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {    
        Pologue_End_Arrow.transform.localPosition = Vector2.MoveTowards(Pologue_End_Arrow.transform.localPosition, Arrow_TargetPos, 20.0f);
        Pologue_End_Txt.transform.localPosition = Vector2.MoveTowards(Pologue_End_Txt.transform.localPosition, Txt_TargetPos, 130.0f);
        Pologue_End_Castle.transform.localPosition = Vector2.MoveTowards(Pologue_End_Castle.transform.localPosition, Castle_TargetPos, 6.0f);
    }

    IEnumerator FadeIn()
    {
        Pologue_End_Castle.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, true);
        Pologue_End_Castle.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.5f, true);
        yield return new WaitForSeconds(5.0f);

        Color color = Panel.GetComponent<Image>().color;
        color.a = 0;
        while (color.a < 1)
        {
            color.a += 0.01f;
            Panel.GetComponent<Image>().color = color;
            yield return null;
        }

        BaseSceneManager.baseinstance.StartClassRoomScene("PrologueScene");
    }
}
