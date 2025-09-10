using System;

public sealed class PointPresenter : Zenject.IInitializable, IDisposable
{
    private readonly ClassForGameLogic classForGameLogic;
    private readonly PointView pointView;

    public PointPresenter(ClassForGameLogic classForGameLogic, PointView pointView)
    {
        this.classForGameLogic = classForGameLogic;
        this.pointView = pointView;
    }    
    public void Initialize()
    {
        pointView.SetupPoint(classForGameLogic.Point.ToString());
        classForGameLogic.OnPointChanged += OnPointChanged;
        classForGameLogic.OnPointEarned += OnPointAdded;
        classForGameLogic.OnPointSpend += OnPointRemoved;
    }
    public void Dispose()
    {
        classForGameLogic.OnPointChanged -= OnPointChanged;
        classForGameLogic.OnPointEarned -= OnPointAdded;
        classForGameLogic.OnPointSpend -= OnPointRemoved;
    }

    private void OnPointChanged(int newValue, int prevValue)
    {
        pointView.ChangePoint(newValue.ToString());

    }
    private void OnPointAdded(int newValue, int range)
    {
        pointView.AddPoint(newValue.ToString());
    }
    private void OnPointRemoved(int newValue, int range)
    {
        pointView.RemovePoint(newValue.ToString());
    }

}
