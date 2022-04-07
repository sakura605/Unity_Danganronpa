using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueDialogueMgr : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    [HideInInspector] public Dialogue[] dialogues;
    GameSceneMgr GameMgr;

    [HideInInspector]public bool isDialogue = false;
    [HideInInspector]public bool isNext = false;    //특정 키 입력 대기 (다음 대사 출력)

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    [HideInInspector]public int lineCount = 0;      //대화 카운트(현재 말하는 캐릭터)
    [HideInInspector]public int contextCount = 0;   //대사 카운트
    [HideInInspector]public Vector3 CamTargetPos;

    [SerializeField] Button Skip_Btn;
    [SerializeField] GameObject Pologue_End;

    InteractionController theIC;
    SplashManager theSplashManager;

    bool AutoMode = false;
    float time = 0;

    bool NextDlg = false;
    //CameraController theCam;
    // Start is called before the first frame update
    void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        theIC.SettingUI(false);

        if (Skip_Btn != null)
            Skip_Btn.onClick.AddListener(() => 
            {
                lineCount = dialogues.Length - 1;
                contextCount = dialogues[lineCount].contexts.Length - 1;
                txt_Dialogue.text = "";
                StartCoroutine(TypeWriter());
            });

        theSplashManager = FindObjectOfType<SplashManager>();
        //theCam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            AutoMode = !AutoMode;
        if (isDialogue) //대화중이면서
            if (isNext)  //다음키 입력이 가능하고
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
                        StartCoroutine(TypeWriter());

                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(CameraTargettingType());
                        }
                        else
                            EndDialogue();
                    }
                }
            }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        //theIC.SettingUI(false);

        dialogues = p_dialogues;

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
            case CameraType.FadeIn:
                SettingUI(false);
                SplashManager.isfinished = false;
                StartCoroutine(theSplashManager.FadeIn(false, true));
                yield return new WaitUntil(() => SplashManager.isfinished);
                break;

            case CameraType.FadeOut:
                SettingUI(false);
                SplashManager.isfinished = false;
                StartCoroutine(theSplashManager.FadeOut(false, true));
                yield return new WaitUntil(() => SplashManager.isfinished);
                break;

            case CameraType.FlashIn:
                SettingUI(false);
                SplashManager.isfinished = false;
                StartCoroutine(theSplashManager.FadeIn(true, true));
                yield return new WaitUntil(() => SplashManager.isfinished);
                break;

            case CameraType.FlashOut:
                SettingUI(false);
                SplashManager.isfinished = false;
                StartCoroutine(theSplashManager.FadeOut(true, true));
                yield return new WaitUntil(() => SplashManager.isfinished);
                break;

            case CameraType.MoveToTarget:
                SettingUI(false);
                SplashManager.isfinished = false;
                StartCoroutine(theSplashManager.MovePos(CamTargetPos, false, true));
                if (lineCount == 1 || lineCount == 3 || lineCount == 6 || lineCount == 10)
                    yield return new WaitUntil(() => SplashManager.isfinished);
                break;

            case CameraType.ObjectFront:
                //theCam.CameraTargetting(dialogues[lineCount].tf_Target);
                break;
            case CameraType.Reset:
                //theCam.CameraTargetting(null, 0.05f, true, false);
                break;
        }
        StartCoroutine(TypeWriter());
    }

    public void EndDialogue()
    {
        isDialogue = false;
        isNext = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        SettingUI(false);
        Pologue_End.SetActive(true);
    }


    public IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount]; //' < t_ReplaceText를 ,로 변환
        t_ReplaceText = t_ReplaceText.Replace("'", ","); //특정 문자 변환
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");
        t_ReplaceText = t_ReplaceText.Replace("\"\"", "\"");

        //txt_Dialogue.text = t_ReplaceText;
        bool t_origin = false; bool t_green = false;
        bool t_ignore = false;

        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓖ': t_origin = false; t_green = true; t_ignore = true; break;
                case 'ⓐ': t_origin = true; t_green = false; t_ignore = true; break;
            }

            string t_letter = t_ReplaceText[i].ToString();

            if (!t_ignore)
            {
                if (t_origin) { t_letter = "<color=#00C8FF>" + t_letter + "</color>"; }
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
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
    }
}
