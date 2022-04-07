using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject MenuBG;
    public RawImage Ok_Btn;
    public Text Ok_txt;
    public RawImage Cancel_Btn;
    public Text Cancel_txt;

    VideoPlayer VP;
    bool IsPlaying = true;
    bool IsOpen = false;
    bool IsQuit = true;
    float ScrSize = 0.0f;
    InteractionController theIC;

    // Start is called before the first frame update
    void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        theIC.SettingUI(false);

        IsOpen = false;
        VP = this.gameObject.GetComponent<VideoPlayer>();
        this.gameObject.GetComponent<VideoPlayer>().targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Ok_Btn.color = new Color32(255, 255, 255, 255);
        Ok_txt.color = new Color32(0, 0, 0, 255);
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
                MenuBG.gameObject.SetActive(false);
            }
        }
        MenuBG.transform.localScale = new Vector2(ScrSize, ScrSize);

        if (VP.clip.length <= (VP.time + 0.05f))
            BaseSceneManager.baseinstance.StartTitleScene("StartVideoScene");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPlaying = !IsPlaying;
            if (IsPlaying)
            {
                IsOpen = false;
                MenuBG.SetActive(true);
                VP.Pause();
            }
            else
            {
                IsOpen = true;
                VP.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IsQuit = false;
            Ok_Btn.color = new Color32(0, 0, 0, 255);
            Ok_txt.color = new Color32(255, 255, 255, 255);
            Cancel_Btn.color = new Color32(255, 255, 255, 255);
            Cancel_txt.color = new Color32(0, 0, 0, 255);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            IsQuit = true;
            Ok_Btn.color = new Color32(255, 255, 255, 255);
            Ok_txt.color = new Color32(0, 0, 0, 255);
            Cancel_Btn.color = new Color32(0, 0, 0, 255);
            Cancel_txt.color = new Color32(255, 255, 255, 255);
        }

        if (MenuBG.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (IsQuit == false)
                {
                    IsPlaying = !IsPlaying;
                    IsOpen = true;
                    VP.Play();
                }
                else
                {
                    IsPlaying = !IsPlaying;
                    BaseSceneManager.baseinstance.StartTitleScene("StartVideoScene");
                }
            }
        }

    }
}
