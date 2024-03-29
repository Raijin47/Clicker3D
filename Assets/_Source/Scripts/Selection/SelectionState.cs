public class SelectionState
{
    private ISelection _selection;
    public SelectionState(ISelection selection)
    {
        _selection = selection;
        _selection.Enter();
    }
    public void Change(ISelection state)
    {
        _selection.Exit();
        _selection = state;
        _selection.Enter();
    }
}