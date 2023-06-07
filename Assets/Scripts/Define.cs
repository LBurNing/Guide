using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Define
{
    public const string TestUI = "TestUI";
    public static Color MaskColor = new Color(0, 0, 0, 0.5f);

    public static Material UIMask
    {
        get
        {
            return new Material(Shader.Find("UI/Mask"));
        }
    }

    public static T Find<T>(string path) where T : Component
    {
        GameObject go = GameObject.Find(path);
        T t = go.GetComponent<T>();

        return t;
    }
}
