using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneMgr : MonoBehaviour
{
    public GameObject[] BG_Group;
    GameObject m_Cam;
    Vector3 m_CamOriginPos = new Vector3(0, 0, -10);
    Vector3 m_TarPos01 = new Vector3(0, -3, -6);
    Vector3 m_TarPos02 = new Vector3(0, -25, -11);


    PrologueDialogueMgr theDM;
    [HideInInspector] public bool IsFirstDlg = false;

    float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<PrologueDialogueMgr>();
        m_Cam = GameObject.Find("Main Camera");
        m_Cam.transform.position = m_CamOriginPos;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 0.1f)
        {
            StartCoroutine(StartDialogue());
            time = 0.0f;
        }
        SettingBG();
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitUntil(() => IsFirstDlg == false);
        IsFirstDlg = true;

        theDM.ShowDialogue(this.transform.GetComponent<InteractionEvent>().GetDialogue());
    }

    void SettingBG()
    {
        if (theDM.lineCount == 1)
            BG_Group[0].SetActive(true);

        else
            BG_Group[0].SetActive(false);

        if (theDM.lineCount == 2 || theDM.lineCount == 3 || theDM.lineCount == 6 || theDM.lineCount == 14)
            BG_Group[1].SetActive(true);
        else
            BG_Group[1].SetActive(false);

        if (theDM.lineCount == 5)
            BG_Group[2].SetActive(true);
        else
            BG_Group[2].SetActive(false);

        if (theDM.lineCount == 7 || theDM.lineCount == 12)
            BG_Group[3].SetActive(true);
        else
            BG_Group[3].SetActive(false);

        if (theDM.lineCount == 8)
            BG_Group[4].SetActive(true);
        else
            BG_Group[4].SetActive(false);

        if (theDM.lineCount == 9)
            BG_Group[5].SetActive(true);
        else
            BG_Group[5].SetActive(false);

        if (theDM.lineCount == 10)
            BG_Group[6].SetActive(true);
        else
            BG_Group[6].SetActive(false);

        if (theDM.lineCount == 11)
            BG_Group[7].SetActive(true);
        else
            BG_Group[7].SetActive(false);

        if (theDM.lineCount == 11)
            BG_Group[7].SetActive(true);
        else
            BG_Group[7].SetActive(false);

        if (theDM.lineCount == 13)
            BG_Group[8].SetActive(true);
        else
            BG_Group[8].SetActive(false);

        if (theDM.lineCount == 15)
            BG_Group[9].SetActive(true);
        else
            BG_Group[9].SetActive(false);

        if (theDM.lineCount == 16)
            BG_Group[10].SetActive(true);
        else
            BG_Group[10].SetActive(false);

        if (theDM.lineCount == 17)
            BG_Group[11].SetActive(true);
        else
            BG_Group[11].SetActive(false);

        if (theDM.lineCount == 18)
            BG_Group[12].SetActive(true);
        else
            BG_Group[12].SetActive(false);

    }
}
