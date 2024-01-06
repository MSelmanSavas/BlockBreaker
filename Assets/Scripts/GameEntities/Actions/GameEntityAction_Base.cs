using Codeflow;
using UnityEngine;

public abstract class GameEntityAction_Base
{
    [SerializeReference]
    Codeflow.Execution.ICodeflowExecutor _codeflowExecutor;

    public virtual bool TryInitialize(IGameEntity gameEntity)
    {
        CodeflowState codeflowState = _codeflowExecutor.Initialize();

        return codeflowState == CodeflowState.Success;
    }

    public virtual bool TryStart(IGameEntity gameEntity)
    {
        CodeflowState codeflowState = _codeflowExecutor.TryStart();

        return codeflowState is CodeflowState.Success or CodeflowState.Waiting;
    }

    public virtual bool TryProcess(IGameEntity gameEntity)
    {
        CodeflowState codeflowState = _codeflowExecutor.TryProcess();

        return codeflowState is CodeflowState.Success or CodeflowState.Waiting;
    }

    public virtual bool Reset(IGameEntity gameEntity)
    {
        CodeflowState codeflowState = _codeflowExecutor.Reset();

        return codeflowState == CodeflowState.Success;
    }
}
