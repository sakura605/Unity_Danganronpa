using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenMgr : MonoBehaviour
{
    public GameObject MenuBG;
    [Header("옵션 설정 바")]
    public RawImage Bar1;
    public RawImage Bar2;
    public RawImage OK_Img;
    public RawImage Cancel_Img;
    public Text Bar1_txt;
    public Text Bar2_txt;
    public Text OK_txt;
    public Text Cancel_txt;

    [Header("난이도 선택메뉴")]
    public RawImage InferDiff_Img01;
    public Text InferDiff_txt01;
    public RawImage InferDiff_Img02;
    public Text InferDiff_txt02;
    public RawImage InferDiff_Img03;
    public Text InferDiff_txt03;

    public RawImage ActDiff_Img01;
    public Text ActDiff_txt01;
    public RawImage ActDiff_Img02;
    public Text ActDiff_txt02;
    public RawImage ActDiff_Img03;
    public Text ActDiff_txt03;

    int SelMenu = 0;
    int SelOk_Can = 0;
    int SelInfer_Diff = 0;
    int SelAct_Diff = 0;

    float ScrSize = 0.0f;
    public bool IsOpen = false;
    bool SelMenu1 = false;
    bool SelMenu2 = false;

    // Start is called before the first frame update
    void Start()
    {
        IsOpen = false;
        MenuBG.transform.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOpen == false)
        {
            ScrSize += 0.1f;
            if (ScrSize > 1.0f)
            {
                ScrSize = 1.0f;
            }
        }
        else
        {
            ScrSize -= 0.1f;
            if (ScrSize < 0.0f)
            {
                ScrSize = 0.0f;
                this.gameObject.SetActive(false);
            }
        }
        MenuBG.transform.localScale = new Vector2(ScrSize, ScrSize);

        SetColor();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++SelMenu;
            if (2 < SelMenu)
                SelMenu = 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            --SelMenu;
            if (SelMenu < 0)
                SelMenu = 0;
        }
    }

    void SetColor()
    {
        if (SelMenu == 0)
        {
            Bar1.color = new Color32(255, 255, 255, 255);
            Bar1_txt.color = new Color32(0, 0, 0, 255);

            Bar2.color = new Color32(0, 0, 0, 0);
            Bar2_txt.color = new Color32(255, 255, 255, 255);

            OK_Img.color = new Color32(0, 0, 0, 255);
            OK_txt.color = new Color32(255, 255, 255, 255);
            SelMenu1 = true;
            SelMenu2 = false;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelInfer_Diff++;
                if (2 < SelInfer_Diff)
                    SelInfer_Diff = 2;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelInfer_Diff--;
                if (SelInfer_Diff < 0)
                    SelInfer_Diff = 0;
            }
        }

        else if (SelMenu == 1)
        {
            Bar1.color = new Color32(0, 0, 0, 0);
            Bar1_txt.color = new Color32(255, 255, 255, 255);
            Bar2.color = new Color32(255, 255, 255, 255);
            Bar2_txt.color = new Color32(0, 0, 0, 255);
            OK_Img.color = new Color32(0, 0, 0, 255);
            OK_txt.color = new Color32(255, 255, 255, 255);
            SelMenu1 = false;
            SelMenu2 = true;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ++SelAct_Diff;
                if (2 < SelAct_Diff)
                    SelAct_Diff = 2;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                --SelAct_Diff;
                if (SelAct_Diff < 0)
                    SelAct_Diff = 0;
            }
        }

        else if (SelMenu == 2)
        {
            Bar1.color = new Color32(0, 0, 0, 0);
            Bar1_txt.color = new Color32(255, 255, 255, 255);
            Bar2.color = new Color32(0, 0, 0, 0);
            Bar2_txt.color = new Color32(255, 255, 255, 255);
            SelMenu1 = false;
            SelMenu2 = false;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ++SelOk_Can;
                if (1 < SelOk_Can)
                    SelOk_Can = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                --SelOk_Can;
                if (SelOk_Can < 0)
                    SelOk_Can = 0;
            }

            if (SelOk_Can == 0)
            {
                OK_Img.color = new Color32(255, 255, 255, 255);
                OK_txt.color = new Color32(0, 0, 0, 255);
                Cancel_Img.color = new Color32(0, 0, 0, 255);
                Cancel_txt.color = new Color32(255, 255, 255, 255);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    BaseSceneManager.baseinstance.StartPrologueScene("TitleScene");
                }
            }

            else if (SelOk_Can == 1)
            {
                OK_Img.color = new Color32(0, 0, 0, 255);
                OK_txt.color = new Color32(255, 255, 255, 255);
                Cancel_Img.color = new Color32(255, 255, 255, 255);
                Cancel_txt.color = new Color32(0, 0, 0, 255);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    IsOpen = true;
                }
            }
        }

        SetColor2();
    }


    void SetColor2()
    {
        if (SelInfer_Diff == 0)
        {
            InferDiff_Img01.color = new Color32(255, 255, 255, 255);
            InferDiff_txt01.color = new Color32(0, 0, 0, 255);

            InferDiff_Img02.color = new Color32(0, 0, 0, 255);
            InferDiff_txt02.color = new Color32(255, 255, 255, 255);

            InferDiff_Img03.color = new Color32(0, 0, 0, 255);
            InferDiff_txt03.color = new Color32(255, 255, 255, 255);

        }

        else if (SelInfer_Diff == 1)
        {
            InferDiff_Img01.color = new Color32(0, 0, 0, 255);
            InferDiff_txt01.color = new Color32(255, 255, 255, 255);

            InferDiff_Img02.color = new Color32(255, 255, 255, 255);
            InferDiff_txt02.color = new Color32(0, 0, 0, 255);

            InferDiff_Img03.color = new Color32(0, 0, 0, 255);
            InferDiff_txt03.color = new Color32(255, 255, 255, 255);
        }

        else if (SelInfer_Diff == 2)
        {
            InferDiff_Img01.color = new Color32(0, 0, 0, 255);
            InferDiff_txt01.color = new Color32(255, 255, 255, 255);

            InferDiff_Img02.color = new Color32(0, 0, 0, 255);
            InferDiff_txt02.color = new Color32(255, 255, 255, 255);

            InferDiff_Img03.color = new Color32(255, 255, 255, 255);
            InferDiff_txt03.color = new Color32(0, 0, 0, 255);
        }

        if (SelAct_Diff == 0)
        {
            ActDiff_Img01.color = new Color32(255, 255, 255, 255);
            ActDiff_txt01.color = new Color32(0, 0, 0, 255);

            ActDiff_Img02.color = new Color32(0, 0, 0, 255);
            ActDiff_txt02.color = new Color32(255, 255, 255, 255);

            ActDiff_Img03.color = new Color32(0, 0, 0, 255);
            ActDiff_txt03.color = new Color32(255, 255, 255, 255);

        }

        else if (SelAct_Diff == 1)
        {
            ActDiff_Img01.color = new Color32(0, 0, 0, 255);
            ActDiff_txt01.color = new Color32(255, 255, 255, 255);
            ActDiff_Img02.color = new Color32(255, 255, 255, 255);
            ActDiff_txt02.color = new Color32(0, 0, 0, 255);
            ActDiff_Img03.color = new Color32(0, 0, 0, 255);
            ActDiff_txt03.color = new Color32(255, 255, 255, 255);
        }

        else if (SelAct_Diff == 2)
        {
            ActDiff_Img01.color = new Color32(0, 0, 0, 255);
            ActDiff_txt01.color = new Color32(255, 255, 255, 255);
            ActDiff_Img02.color = new Color32(0, 0, 0, 255);
            ActDiff_txt02.color = new Color32(255, 255, 255, 255);
            ActDiff_Img03.color = new Color32(255, 255, 255, 255);
            ActDiff_txt03.color = new Color32(0, 0, 0, 255);
        }
    }
}
