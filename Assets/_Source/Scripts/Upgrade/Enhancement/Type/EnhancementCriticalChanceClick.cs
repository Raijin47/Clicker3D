public class EnhancementCriticalChanceClick : EnhancementLimited
{
    protected override void Execute()
    {
        Modifier.EnhancementCritChanceClick = _currentValue;
    }
}