using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSceneMgr : MonoBehaviour
{
    [SerializeField] GameObject Game_Start_Dialogue;
    [SerializeField] GameObject ClassRoom_Front;
    [SerializeField] GameObject ClassRoom_Left;
    [SerializeField] Button m_LeftArrowBtn;
    [SerializeField] Button m_RightArrowBtn;

    InGameDialogueManager theDM;
    static bool IsFirstDlg = true;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<InGameDialogueManager>();
        //GameObject.Find("UI_Mgr").transform.GetChild(5).gameObject.SetActive(true);
        //if (m_LeftArrowBtn == null)
        //    m_LeftArrowBtn = GameObject.Find("Arrow_Left").GetComponent<Button>();
        //else if (m_LeftArrowBtn != null)
        //    m_LeftArrowBtn.onClick.AddListener(() => { ClassRoom_Front.SetActive(false); ClassRoom_Left.SetActive(true); });

        //if (m_RightArrowBtn == null)
        //    m_RightArrowBtn = GameObject.Find("Arrow_Right").GetComponent<Button>();
        //else if (m_RightArrowBtn != null)
        //    m_RightArrowBtn.onClick.AddListener(() => { ClassRoom_Front.SetActive(true); ClassRoom_Left.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Start_Dialogue.activeSelf == true)
        {
            time += Time.deltaTime;
            if (time >= 0.1f)
            {
                StartCoroutine(StartDialogue(Game_Start_Dialogue));
                time = 0.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ClassRoom_Front.SetActive(false);
            ClassRoom_Left.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            ClassRoom_Front.SetActive(true);
            ClassRoom_Left.SetActive(false);
        }
    }

    IEnumerator StartDialogue(GameObject go)
    {
        yield return new WaitUntil(() => IsFirstDlg == true);
        IsFirstDlg = false;

        theDM.ShowDialogue(go.transform.GetComponent<InteractionEvent>().GetDialogue());
    }
}
