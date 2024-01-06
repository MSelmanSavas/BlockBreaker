[System.Serializable]
public class BlockData_ID : GameEntityData_Base
{
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ShowInInspector]
#endif
    int _id = 0;
    public int GetID() => _id;
    public void SetID(int id) => _id = id;
}
