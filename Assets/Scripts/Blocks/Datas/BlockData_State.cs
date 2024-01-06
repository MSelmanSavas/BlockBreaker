using UnityEngine;
using UnityEngine.Events;

public class BlockData_State : GameEntityData_Base
{
    [SerializeField]
    BlockState _previousBlockState;

    [SerializeField]
    BlockState _blockState;

    public override bool TryInitialize(IGameEntity gameEntity)
    {
        _previousBlockState = _blockState;
        return true;
    }

    public UnityAction<BlockState, BlockState> OnBlockStateChange;
    public BlockState GetBlockState() => _blockState;
    public void SetBlockState(BlockState blockState)
    {
        if (_blockState == blockState)
            return;

        _previousBlockState = _blockState;
        _blockState = blockState;
        
        OnBlockStateChange?.Invoke(_previousBlockState, _blockState);
    }
}
