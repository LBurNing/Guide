using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : IManager
{
    public bool Init()
    {
        return true;
    }

    public void Load(string path, Action<UnityEngine.Object> loadComplete)
    {
        UnityEngine.Object obj = Resources.Load(path);
        loadComplete?.Invoke(obj);
    }

    public void UnInit()
    {
    }

    public void Update()
    {
    }
}
