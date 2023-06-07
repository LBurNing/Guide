using System;
using UnityEngine;
using UnityEngine.UI;

public class MaskModule : IManager
{
    private RectTransform _mask;
    private RectTransform _target;
    private Material _material;
    private Action _click;

    public bool Active
    {
        set { _mask?.gameObject.SetActive(value); }
    }

    public bool Init()
    {
        Canvas canvas = Game.UIModule.GetCanvas(UILayer.MaskLayer);
        GameObject maskGo = new GameObject("Mask");
        maskGo.SetActive(false);
        maskGo.transform.SetParent(canvas.transform);
        Image image = maskGo.AddComponent<Image>();

        _mask = maskGo.GetComponent<RectTransform>();
        _mask.pivot = Vector2.one * 0.5f;
        _mask.anchorMin = Vector2.zero;
        _mask.anchorMax = Vector2.one;
        _mask.sizeDelta = Vector2.zero;
        _mask.anchoredPosition = Vector2.zero;
        _material = Define.UIMask;

        image.material = _material;
        image.color = Define.MaskColor;
        return true;
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            bool detect = Detect(Input.mousePosition);
            if (detect)
            {
                Active = false;
                _click?.Invoke();
                _click = null;
            }
        }
    }

    public void Set(RectTransform target, Action click)
    {
        CreateMask(target.localPosition, target.sizeDelta);
        _target = target;
        _click = click;
        _mask.gameObject.SetActive(true);
    }

    public void Set(Vector2 pos, Vector2 size, RectTransform target, Action click)
    {
        CreateMask(pos, size);
        _target = target;
        _click = click;
        _mask.gameObject.SetActive(true);
    }

    private void CreateMask(Vector2 pos, Vector2 size)
    {
        _material.SetFloat("_MaskType", 1f);
        _material.SetVector("_Origin", new Vector4(pos.x, pos.y, size.x, size.y));
    }

    private bool Detect(Vector2 sp, Camera eventCamera = null)
    {
        if (_target == null)
            return true;

        return RectTransformUtility.RectangleContainsScreenPoint(_target, sp, eventCamera);
    }

    public void UnInit()
    {
        _click = null;
    }
}