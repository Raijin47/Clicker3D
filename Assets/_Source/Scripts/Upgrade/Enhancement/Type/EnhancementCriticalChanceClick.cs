public class EnhancementCriticalChanceClick : EnhancementBase
{
    protected override void Execute()
    {
        Modifier.EnhancementCritChanceClick = _currentValue;
    }

    protected override void UpdateTextMax()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateTextProcess()
    {
        throw new System.NotImplementedException();
    }
}