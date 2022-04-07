using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionEffect : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector3 targetPos = new Vector3();

    [SerializeField] ParticleSystem ps_Effect;

    public static bool isCollide = false;

    public void SetTarget(Vector3 _target)
    {
        targetPos = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != Vector3.zero)
        {
            if ((transform.position - targetPos).sqrMagnitude >= 0.1f)
                transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);
            else
            {
                ps_Effect.gameObject.SetActive(true);
                ps_Effect.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z - 1);
                ps_Effect.Play();
                isCollide = true;
                targetPos = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
    }
}
