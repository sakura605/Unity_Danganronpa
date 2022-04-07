using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleSceneMgr : MonoBehaviour
{
    public GameObject NewGame;
    public GameObject LoadGame;
    public GameObject Option;
    public GameObject Exit;
    public GameObject MenuScreen;
    public Image BG_Panel;
    int SelMenu = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuScreen.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ++SelMenu;
                if (3 < SelMenu)
                    SelMenu = 3;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                --SelMenu;
                if (SelMenu < 0)
                    SelMenu = 0;
            }
            SetImageScale();
        }
    }

    void SetImageScale()
    {
        if (SelMenu == 0)
        {
            NewGame.transform.localScale = new Vector2(1.4f, 1.4f);
            LoadGame.transform.localScale = new Vector2(1.0f, 1.0f);
            Option.transform.localScale = new Vector2(1.0f, 1.0f);
            Exit.transform.localScale = new Vector2(1.0f, 1.0f);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MenuScreen.SetActive(true);
                MenuScreen.GetComponentInParent<MenuScreenMgr>().IsOpen = false;
            }
        }

        else if (SelMenu == 1)
        {
            NewGame.transform.localScale = new Vector2(1.0f, 1.0f);
            LoadGame.transform.localScale = new Vector2(1.4f, 1.4f);
            Option.transform.localScale = new Vector2(1.0f, 1.0f);
            Exit.transform.localScale = new Vector2(1.0f, 1.0f);
        }

        else if (SelMenu == 2)
        {
            NewGame.transform.localScale = new Vector2(1.0f, 1.0f);
            LoadGame.transform.localScale = new Vector2(1.0f, 1.0f);
            Option.transform.localScale = new Vector2(1.4f, 1.4f);
            Exit.transform.localScale = new Vector2(1.0f, 1.0f);
        }

        else if (SelMenu == 3)
        {
            NewGame.transform.localScale = new Vector2(1.0f, 1.0f);
            LoadGame.transform.localScale = new Vector2(1.0f, 1.0f);
            Option.transform.localScale = new Vector2(1.0f, 1.0f);
            Exit.transform.localScale = new Vector2(1.4f, 1.4f);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }


    IEnumerator FadeIn()
    {
        BG_Panel.CrossFadeAlpha(0.0f, 1.0f, true);
        yield return null;
    }
}
