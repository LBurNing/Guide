using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    private Button _open;

    private void Awake()
    {
        _open = Define.Find<Button>("Open");
        _open.onClick.AddListener(OnOpen);
    }

    void Start()
    {
        Game.Mask.Set(_open.GetComponent<RectTransform>(), OnOpen);
    }

    private void OnOpen()
    {
        Debug.LogError("click: " + _open.name);
    }
}
