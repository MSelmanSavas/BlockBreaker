using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulExtensions.Vector3;

public class GameFieldBoundryLoader : GameSystem_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    ScriptableGamefieldBoundryStorage _gamefieldBoundryStorage;

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    Camera _mainCamera;

    public GameObject BorderParent { get; private set; }
    public GameObject RightBorder { get; private set; }
    public GameObject LeftBorder { get; private set; }
    public GameObject TopBorder { get; private set; }
    public GameObject DownBorder { get; private set; }


    public override bool TryInitialize(GameSystems gameSystems)
    {
        if (!base.TryInitialize(gameSystems))
            return false;

        if (!base.TryInitialize(gameSystems))
            return false;


        if (!RefBook.TryGet(out GameConfig gameConfig))
            return false;

        if (gameConfig.GamefieldBoundryStorage == null)
            return false;

        _gamefieldBoundryStorage = gameConfig.GamefieldBoundryStorage;
        _mainCamera = Camera.main;

        BorderParent = new GameObject()
        {
            name = "GamefieldBorders",
        };

        BorderParent.transform.position = Vector3.zero;

        AlignGamefieldBoundries(_gamefieldBoundryStorage);
        return true;
    }

    public override bool TryDeInitialize(GameSystems gameSystems)
    {
        if (!base.TryDeInitialize(gameSystems))
            return false;

        GameObject.Destroy(BorderParent);
        GameObject.Destroy(RightBorder);
        GameObject.Destroy(LeftBorder);
        GameObject.Destroy(TopBorder);
        GameObject.Destroy(DownBorder);
        
        return true;
    }

    void AlignGamefieldBoundries(ScriptableGamefieldBoundryStorage gamefieldBoundryStorage)
    {
        RightBorder = GameObject.Instantiate(gamefieldBoundryStorage.RightBoundryPrefab);
        RightBorder.transform.SetParent(BorderParent.transform);

        SetRightBoundry(RightBorder);

        LeftBorder = GameObject.Instantiate(gamefieldBoundryStorage.LeftBoundryPrefab);
        LeftBorder.transform.SetParent(BorderParent.transform);

        SetLeftBoundry(LeftBorder);

        TopBorder = GameObject.Instantiate(gamefieldBoundryStorage.TopBoundryPrefab);
        TopBorder.transform.SetParent(BorderParent.transform);

        SetTopBoundry(TopBorder);

        DownBorder = GameObject.Instantiate(gamefieldBoundryStorage.DownBoundryPrefab);
        DownBorder.transform.SetParent(BorderParent.transform);

        SetDownBoundry(DownBorder);
    }

    void SetRightBoundry(GameObject boundry)
    {
        Vector3 borderPosition = _mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));
        boundry.transform.position = borderPosition.WithZ(0);
    }


    void SetLeftBoundry(GameObject boundry)
    {
        Vector3 borderPosition = _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        boundry.transform.position = borderPosition.WithZ(0);
    }

    void SetTopBoundry(GameObject boundry)
    {
        Vector3 borderPosition = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
        boundry.transform.position = borderPosition.WithZ(0);
    }

    void SetDownBoundry(GameObject boundry)
    {
        Vector3 borderPosition = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));
        boundry.transform.position = borderPosition.WithZ(0);
    }
}
