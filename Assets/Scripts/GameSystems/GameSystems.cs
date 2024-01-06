using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSystems : MonoBehaviour
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public RuntimeGameSystemContext RuntimeGameSystemContext { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    public bool IsInitialized { get; private set; }

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    List<GameSystem_Base> _updateGameSystems = new();

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    List<GameSystem_Base> _lateUpdateGameSystems = new();

    public void Initialize()
    {
        RuntimeGameSystemContext = new RuntimeGameSystemContext();
        IsInitialized = InitializeSystems();
    }

    private bool InitializeSystems()
    {
        foreach (var system in _updateGameSystems)
        {
            if (!system.TryInitialize(this))
            {
                Logger.LogErrorWithTag(LogCategory.GameSystems, $"Error while trying to initialize system : {system}! Cannot continue initializing game systems!");
                return false;
            }
        }

        foreach (var system in _lateUpdateGameSystems)
        {
            if (!system.TryInitialize(this))
            {
                Logger.LogErrorWithTag(LogCategory.GameSystems, $"Error while trying to initialize late update system : {system}! Cannot continue initializing game systems!");
                return false;
            }
        }

        return true;
    }

    public bool TryAddGameSystem(GameSystem_Base gameSystem)
    {
        _updateGameSystems.Add(gameSystem);
        return true;
    }

    public bool TryAddGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base gameSystem = Activator.CreateInstance(typeof(T)) as GameSystem_Base;
        return TryAddGameSystem(gameSystem);
    }

    public bool TryGetGameSystemByType<T>(out T gameSystem) where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _updateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
        {
            gameSystem = null;
            return false;
        }

        gameSystem = foundGameSystem as T;
        return true;
    }

    public bool TryRemoveGameSystem(GameSystem_Base gameSystem)
    {
        if (!_updateGameSystems.Contains(gameSystem))
            return false;

        _updateGameSystems.Remove(gameSystem);
        return true;
    }

    public bool TryRemoveGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _updateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
            return false;

        _updateGameSystems.Remove(foundGameSystem);
        return true;
    }

    public bool TryAddLateUpdateGameSystem(GameSystem_Base gameSystem)
    {
        _lateUpdateGameSystems.Add(gameSystem);
        return true;
    }

    public bool TryAddLateUpdateGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base gameSystem = Activator.CreateInstance(typeof(T)) as GameSystem_Base;
        return TryAddLateUpdateGameSystem(gameSystem);
    }

    public bool TryGetLateUpdateGameSystemByType<T>(out T gameSystem) where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _lateUpdateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
        {
            gameSystem = null;
            return false;
        }

        gameSystem = foundGameSystem as T;
        return true;
    }

    public bool TryRemoveLateUpdateGameSystem(GameSystem_Base gameSystem)
    {
        if (!_lateUpdateGameSystems.Contains(gameSystem))
            return false;

        _lateUpdateGameSystems.Remove(gameSystem);
        return true;
    }

    public bool TryRemoveLateUpdateGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _lateUpdateGameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
            return false;

        _lateUpdateGameSystems.Remove(foundGameSystem);
        return true;
    }

    private void Update()
    {
        if (!IsInitialized)
            return;

        foreach (var system in _updateGameSystems)
            system.Update(RuntimeGameSystemContext);
    }

    private void LateUpdate()
    {
        if (!IsInitialized)
            return;

        foreach (var system in _lateUpdateGameSystems)
            system.Update(RuntimeGameSystemContext);
    }
}
