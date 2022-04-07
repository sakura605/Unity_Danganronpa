using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameDialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;
    [SerializeField] GameObject go_DialogueState;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;
    [SerializeField] Text txt_State;


    Dialogue[] dialogues;

    bool isDialogue = false;    //대화중일 경우 true;
    bool isNext = false;        //특정 키 입력 대기.

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;          //대화 카운트.
    int contextCount = 0;       //대사 카운트.
    Vector3 CamTargetPos;
    bool AutoMode = false;
    float time = 0;
    bool NextDlg = false;

    InteractionController theIC;
    CameraController theCam;
    SplashManager theSplashManager;
    SpriteManager theSpriteManager;
    CutSceneManager theCutSceneManager;

    void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        theCam = FindObjectOfType<CameraController>();
        //theSplashManager = FindObjectOfType<SplashManager>();
        //theSpriteManager = FindObjectOfType<SpriteManager>();
        theCutSceneManager = FindObjectOfType<CutSceneManager>();
    }

    void Update()
    {
        if (theSpriteManager == null)
            theSpriteManager = FindObjectOfType<SpriteManager>();

        if (theSplashManager == null)
            theSplashManager = FindObjectOfType<SplashManager>();

        if (Input.GetKeyDown(KeyCode.M))
            AutoMode = !AutoMode;

        if (isDialogue) //현재 대화 중인가?
            if (isNext) //다음 키입력이 가능한가?
            {
                if (AutoMode == true)
                {
                    time += Time.deltaTime;
                    if (time >= 1.0f)
                    {
                        time = 0;
                        NextDlg = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space) || NextDlg == true)
                {
                    NextDlg = false;
                    isNext = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(CameraTargettingType());
                        }
                        else //대화가 종료된 경우
                        {
                            StartCoroutine(EndDialogue());
                        }

                    }
                }
            }

            if (lineCount == 1)
            {
                dialogues[0].tf_Target.gameObject.SetActive(false);
                dialogues[0].tf_Target.parent.gameObject.SetActive(false);
                dialogues[1].tf_Target.gameObject.SetActive(true);
            }

    }


    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        txt_State.text = "";
        theIC.SettingUI(false);
        dialogues = p_dialogues;
        theCam.CameOriginSetting();
        StartCoroutine(CameraTargettingType());
    }

    IEnumerator CameraTargettingType()
    {
        if (lineCount == 1)
            CamTargetPos = new Vector3(0, -2.5f, -6);
        else if (lineCount == 10)
            CamTargetPos = new Vector3(11.65f, 0, -10);
        else if (lineCount == 2 || lineCount == 3 || lineCount == 6 || lineCount == 14)
            CamTargetPos = new Vector3(0, -21, -10);
        else
            CamTargetPos = new Vector3(0, 0, -10);
        switch (dialogues[lineCount].cameraType)
        {
            case CameraType.FadeIn: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeIn(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FadeOut: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeOut(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashIn: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeIn(true, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashOut: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeOut(true, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.ObjectFront: theCam.CameraTargetting(dialogues[lineCount].tf_Target); break;
            case CameraType.Reset: theCam.CameraTargetting(null, 0.05f, true, false); break;
            case CameraType.ShowCutScene: SettingUI(false); CutSceneManager.isFinished = false; StartCoroutine(theCutSceneManager.CutSceneCoroutine(dialogues[lineCount].spriteName[contextCount], true)); yield return new WaitUntil(() => CutSceneManager.isFinished); break;
            case CameraType.HideCutScene: SettingUI(false); CutSceneManager.isFinished = false; StartCoroutine(theCutSceneManager.CutSceneCoroutine(null, false)); yield return new WaitUntil(() => CutSceneManager.isFinished); theCam.CameraTargetting(dialogues[lineCount].tf_Target); break;
            case CameraType.MoveToTarget:
                SettingUI(false);
                SplashManager.isfinished = false;
                StartCoroutine(theSplashManager.MovePos(CamTargetPos, false, true));
                if (SceneManager.GetActiveScene().name == "PrologueScene")
                    if (lineCount == 1 || lineCount == 3 || lineCount == 6 || lineCount == 10)
                        yield return new WaitUntil(() => SplashManager.isfinished);
                break;
        }
        StartCoroutine(TypeWriter());
    }

    IEnumerator EndDialogue()
    {
        if (theCutSceneManager.CheckCutScene())
        {
            CutSceneManager.isFinished = false;
            StartCoroutine(theCutSceneManager.CutSceneCoroutine(null, false));
            yield return new WaitUntil(() => CutSceneManager.isFinished);
        }

        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theCam.CameraTargetting(null, 0.05f, true, true);
        SettingUI(false);
    }

    void ChangeSprite()
    {
        if (dialogues[lineCount].tf_Target != null)
        {
            if (dialogues[lineCount].spriteName[contextCount] != "")
            {
                StartCoroutine(theSpriteManager.SpriteChangeCoroutine(dialogues[lineCount].tf_Target, dialogues[lineCount].spriteName[contextCount]));
            }
        }
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);
        
        ChangeSprite();

        Debug.Log(lineCount + " : " + contextCount);
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount]; //한줄이 들어감
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");
        t_ReplaceText = t_ReplaceText.Replace("\"\"", "\"");

        //txt_Dialogue.text = t_ReplaceText;

        bool t_white = false, t_skyblue = false, t_green = false;
        bool t_ignore = false;  //특수문자 생략

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch(t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true; t_skyblue = false; t_green = false; t_ignore = true; break;
                case 'ⓑ': t_white = false; t_skyblue = true; t_green = false; t_ignore = true; break;
                case 'ⓖ': t_white = false; t_skyblue = false; t_green = true; t_ignore = true; break;
            }

            string t_letter = t_ReplaceText[i].ToString();
            if (!t_ignore)
            {
                if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                else if (t_skyblue) { t_letter = "<color=#00C8FF>" + t_letter + "</color>"; }
                else if (t_green) { t_letter = "<color=#00ff00>" + t_letter + "</color>"; }
                txt_Dialogue.text += t_letter;
            }
            t_ignore = false;

            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        if (p_flag)
        {
            if (dialogues[lineCount].name == "")
            {
                go_DialogueNameBar.SetActive(false);
                if (go_DialogueState != null)
                    go_DialogueState.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                if (go_DialogueState != null)
                    go_DialogueState.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }

        }
        else
        {
            go_DialogueNameBar.SetActive(false);
            if (go_DialogueState != null)
                go_DialogueState.SetActive(false);
        }
    }
}
