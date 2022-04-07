using System.Collections;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(SpriteRenderer p_SpriteRenderer, Sprite p_Sprite)
    {
        if (p_SpriteRenderer.sprite == p_Sprite)
            return true;
        else
            return false;
    }

    public IEnumerator SpriteChangeCoroutine(Transform p_Target, string p_SpriteName)
    {
        SpriteRenderer t_SpriteRenderer = p_Target.GetComponent<SpriteRenderer>();
        p_SpriteName = p_SpriteName.Replace("\r", "");

        if (t_SpriteRenderer != null && p_SpriteName != "")
        {
            Sprite t_Sprite = (Sprite)Resources.Load(p_SpriteName, typeof(Sprite));

            if (!CheckSameSprite(t_SpriteRenderer, t_Sprite))
            {
                Color t_color = t_SpriteRenderer.color;
                t_color.a = 0;
                t_SpriteRenderer.color = t_color;

                t_SpriteRenderer.sprite = t_Sprite;

                while (t_color.a < 1)
                {
                    t_color.a += fadeSpeed;
                    t_SpriteRenderer.color = t_color;
                    yield return null;
                }
            }
        }
        

    }
}
