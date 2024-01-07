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

    GameObject _borderParent;
    GameObject _rightBorder;
    GameObject _leftBorder;
    GameObject _topBorder;
    GameObject _downBorder;


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

        _borderParent = new GameObject()
        {
            name = "GamefieldBorders",
        };

        _borderParent.transform.position = Vector3.zero;

        AlignGamefieldBoundries(_gamefieldBoundryStorage);
        return true;
    }

    void AlignGamefieldBoundries(ScriptableGamefieldBoundryStorage gamefieldBoundryStorage)
    {
        _rightBorder = GameObject.Instantiate(gamefieldBoundryStorage.RightBoundryPrefab);
        _rightBorder.transform.SetParent(_borderParent.transform);

        SetRightBoundry(_rightBorder);

        _leftBorder = GameObject.Instantiate(gamefieldBoundryStorage.LeftBoundryPrefab);
        _leftBorder.transform.SetParent(_borderParent.transform);

        SetLeftBoundry(_leftBorder);

        _topBorder = GameObject.Instantiate(gamefieldBoundryStorage.TopBoundryPrefab);
        _topBorder.transform.SetParent(_borderParent.transform);

        SetTopBoundry(_topBorder);

        _downBorder = GameObject.Instantiate(gamefieldBoundryStorage.DownBoundryPrefab);
        _downBorder.transform.SetParent(_borderParent.transform);

        SetDownBoundry(_downBorder);
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
