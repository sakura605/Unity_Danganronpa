using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;
    [SerializeField] GameObject go_NormalCrosshair;
    [SerializeField] GameObject go_Crosshair;
    [SerializeField] GameObject go_Cursor;
    [SerializeField] Text txt_TargetName;

    bool isContact = false;
    public static bool isInteract = false;

    [SerializeField] ParticleSystem ps_QuestionEffect;

    [SerializeField] Image img_Interaction;
    [SerializeField] Image img_InteractionEffect;
    [SerializeField] Image img_InteractionLine;
    [SerializeField] Image img_InteractionLineLens;

    InGameDialogueManager theDM;

    public void SettingUI(bool p_flag)
    {
        go_Crosshair.SetActive(p_flag);
        go_Cursor.SetActive(p_flag);

        isInteract = !p_flag;

    }

    void Start()
    {
        theDM = FindObjectOfType<InGameDialogueManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isInteract)
        {
            CheckObject();
            ClickLeftBtn();
        }
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        if (Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 100))
            Contact();
        else
            NotContact();
    }

    void Contact()
    {
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            if (!isContact)
            {
                isContact = true;
                img_Interaction.transform.position = cam.WorldToScreenPoint(hitInfo.transform.position);
                img_InteractionEffect.transform.position = img_Interaction.transform.position;
                img_InteractionLine.transform.position = new Vector3(img_Interaction.transform.position.x + 170, img_Interaction.transform.position.y - 34, 0);
                StopCoroutine("Interaction");
                StopCoroutine("InteractionEffect");
                StartCoroutine("Interaction", true);
                StartCoroutine("InteractionEffect");
            }
        }
        else
        {
            NotContact();
        }
    }

    void NotContact()
    {
        if(isContact)
        {
            isContact = false;
            StopCoroutine("Interaction");
            StartCoroutine("Interaction", false);
        }
    }

    IEnumerator Interaction(bool p_Appear)
    {
        Color color = img_Interaction.color;
        float amount = img_InteractionLine.fillAmount;
        if (p_Appear)
        {
            color.a = 0;
            while (color.a < 1)
            {
                color.a += 0.1f;
                img_Interaction.color = color;
                yield return null;
            }
            while (amount < 1)
            {
                amount += 0.1f;
                img_InteractionLine.fillAmount = amount;
                if (amount > 0.7f)
                {
                    txt_TargetName.text = hitInfo.transform.GetComponent<InteractionType>().GetName();
                    img_InteractionLineLens.gameObject.SetActive(true);
                }
                yield return null;
            }
        }
        else
        {
            while (color.a > 0)
            {
                color.a -= 0.1f;
                img_Interaction.color = color;
                yield return null;
            }
            while (amount > 0)
            {
                amount -= 0.1f;
                img_InteractionLine.fillAmount = amount;
                if (amount < 0.7f)
                {
                    txt_TargetName.text = "";
                    img_InteractionLineLens.gameObject.SetActive(false);
                }
                yield return null;
            }
        }
    }

    IEnumerator InteractionEffect()
    {
        while (isContact && !isInteract)
        {
            Color color = img_InteractionEffect.color;
            color.a = 0.8f;
            img_InteractionEffect.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            Vector3 t_scale = img_InteractionEffect.transform.localScale;

            while (color.a > 0)
            {
                color.a -= 0.03f;
                img_InteractionEffect.color = color;
                t_scale.Set(t_scale.x + Time.deltaTime, t_scale.y + Time.deltaTime, t_scale.z + Time.deltaTime); //Set벡터의 값을 수정하기 위한 함수
                img_InteractionEffect.transform.localScale = t_scale;
                yield return null;
            }
            yield return null;
        }
    }

    void ClickLeftBtn()
    {
        if (!isInteract)
        {
            if (Input.GetMouseButton(0))
            {
                if (isContact)
                {
                    Interact();
                }
            }
        }
    }

    void Interact()
    {
        isInteract = true;

        StopCoroutine("Interaction");
        img_InteractionLine.fillAmount = 0.0f;
        img_InteractionLineLens.gameObject.SetActive(false);
        txt_TargetName.text = "";

        Color color = img_Interaction.color;
        color.a = 0;
        img_Interaction.color = color;

        ps_QuestionEffect.gameObject.SetActive(true);
        Vector3 t_targetPos = hitInfo.transform.position;
        ps_QuestionEffect.GetComponent<QuestionEffect>().SetTarget(t_targetPos);
        ps_QuestionEffect.transform.position = cam.transform.position;
        StartCoroutine(WaitCollision());
    }

    IEnumerator WaitCollision()
    {
        yield return new WaitUntil(() => QuestionEffect.isCollide);
        QuestionEffect.isCollide = false;
 
        theDM.ShowDialogue(hitInfo.transform.GetComponent<InteractionEvent>().GetDialogue());
    }
}
