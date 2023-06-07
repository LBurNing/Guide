using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UILayer
{
    Begin,
    MainLayer,
    DefaultLayer,
    MaskLayer,
    End,
}

public class UIModule : IManager
{
    private Transform _root;
    private GameObject _eventSystem;
    private Dictionary<UILayer, Canvas> _dicLayerNameToLayerCanvas = new Dictionary<UILayer, Canvas>();

    public bool Init()
    {
        GameObject rootGo = new GameObject("UIRoot");
        _root = rootGo.transform as Transform;

        _eventSystem = new GameObject("__event__", typeof(EventSystem));
        _eventSystem.transform.parent = _root;
        _eventSystem.AddComponent<StandaloneInputModule>();

        for (int i = (int)UILayer.Begin + 1; i < (int)UILayer.End; i++)
        {
            UILayer type = (UILayer)i;
            GameObject layerGo = new GameObject(type.ToString());
            layerGo.transform.SetParent(_root);

            Canvas canvas = layerGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = i;
            layerGo.AddComponent<GraphicRaycaster>();
            layerGo.AddComponent<CanvasScaler>();

            _dicLayerNameToLayerCanvas[type] = canvas;
        }

        GameObject.DontDestroyOnLoad(rootGo);
        return true;
    }

    public void LoadView(string viewName, UILayer uILayer = UILayer.DefaultLayer)
    {
        Game.Loader.Load(viewName, (UnityEngine.Object obj) =>
        {
            GameObject uiGo = GameObject.Instantiate(obj) as GameObject;
            Canvas canvas = GetCanvas(uILayer);
            uiGo.transform.SetParent(canvas.transform);
        });
    }

    public Canvas GetCanvas(UILayer layer)
    {
        Canvas canvas = null;
        if (_dicLayerNameToLayerCanvas.TryGetValue(layer, out canvas))
            return canvas;

        Debug.LogError("no find canvas, by layer: " + layer.ToString());
        return null;
    }

    public void UnInit()
    {
    }

    public void Update()
    {
    }
}
