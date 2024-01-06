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
    List<GameSystem_Base> _gameSystems = new();

    public void Initialize()
    {
        RuntimeGameSystemContext = new RuntimeGameSystemContext();
        IsInitialized = InitializeSystems();
    }

    private bool InitializeSystems()
    {
        foreach (var system in _gameSystems)
        {
            if (!system.TryInitialize(this))
            {
                Logger.LogErrorWithTag(LogCategory.GameSystems, $"Error while trying to initialize system : {system}! Cannot continue initializing game systems!");
                return false;
            }
        }

        return true;
    }

    public bool TryAddGameSystem(GameSystem_Base gameSystem)
    {
        _gameSystems.Add(gameSystem);
        return true;
    }

    public bool TryAddGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base gameSystem = Activator.CreateInstance(typeof(T)) as GameSystem_Base;
        return TryAddGameSystem(gameSystem);
    }

    public bool TryRemoveGameSystem(GameSystem_Base gameSystem)
    {
        if (!_gameSystems.Contains(gameSystem))
            return false;

        _gameSystems.Remove(gameSystem);
        return true;
    }

    public bool TryRemoveGameSystemByType<T>() where T : GameSystem_Base
    {
        GameSystem_Base foundGameSystem = _gameSystems.Where(x => x.GetType() == typeof(T)).First();

        if (foundGameSystem == null)
            return false;

        _gameSystems.Remove(foundGameSystem);
        return true;
    }

    private void Update()
    {
        if (!IsInitialized)
            return;

        foreach (var system in _gameSystems)
            system.Update(RuntimeGameSystemContext);
    }
}
