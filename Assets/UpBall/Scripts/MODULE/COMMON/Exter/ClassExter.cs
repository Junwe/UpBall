using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public static class ClassExter
{
    public static void Swap(this ref Vector3 vector3,ref Vector3 e)
    {
        Vector3 temp = vector3;
        vector3 = e;
        e = temp;
    }
    public static void Swap(this ref Vector2 vector2,ref Vector2 e)
    {
        Vector2 temp = vector2;
        vector2 = e;
        e = temp;
    }

    public static void Swap(this ref float s, ref float e)
    {
        float temp = s;
        s = e;
        e = temp;
    }
    public static void SetAlpha(this SpriteRenderer sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void SetAlpha(this Image sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void SetAlpha(this Text sprite, float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static void PlusText(this Text text,int start, int end, float time)
    {
        text.StartCoroutine(Tween.instance.SetText(text, start, end, time));
    }
}
