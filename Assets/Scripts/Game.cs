using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game _game = null;
    private List<IManager> _managerList;
    private MaskModule _maskModule;
    private UIModule _uiModule;
    private AssetLoader _loader;

    public static MaskModule Mask
    {
        get { return _game._maskModule; }
    }

    public static AssetLoader Loader
    {
        get { return _game._loader; }
    }

    public static UIModule UIModule
    {
        get { return _game._uiModule; }
    }

    private void Awake()
    {
        _game = this;
        DontDestroyOnLoad(this);
        _managerList = new List<IManager>();
        InitManagers();
    }

    private void Start()
    {
        Game.UIModule.LoadView(Define.TestUI);
    }

    void Update()
    {
        foreach (var manager in _managerList)
        {
            try
            {
                manager?.Update();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    private void InitManagers()
    {
        _uiModule = new UIModule();
        InitManager(_uiModule);

        _maskModule = new MaskModule();
        InitManager(_maskModule);

        _loader = new AssetLoader();
        InitManager(_loader);

        _managerList.Add(_maskModule);
    }

    private void InitManager(IManager manager)
    {
        if (manager.Init() == false)
        {
            Debug.LogError("Manager: " + manager.GetType().Name + " Init Error");
            throw new System.ApplicationException();
        }
        else
        {
            _managerList.Add(manager);
        }
    }
}
