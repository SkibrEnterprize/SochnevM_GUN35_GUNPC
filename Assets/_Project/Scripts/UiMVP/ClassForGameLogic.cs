
public sealed class ClassForGameLogic
{
    public delegate void AddedDelegate(int newValue, int range);
    public delegate void RemovedDelegate(int newValue, int range);
    public delegate void ChangedDelegate(int newValue, int prevValue);

    public event AddedDelegate OnPointEarned;
    public event RemovedDelegate OnPointSpend;
    public event ChangedDelegate OnPointChanged;

    public int Point {  get; private set; }
    public ClassForGameLogic(int point)
    {
        Point = point;
        ChangePoint(Point);
    }
    public void SetupPoint(int point)
    {
        Point = point;
    }
    
    public void ChangePoint(int point)
    {
        int previopusPoint = this.Point;
        this.Point = point;
        OnPointChanged?.Invoke(Point, previopusPoint);
    }

    public void EarnPoint(int range)
    {        
        this.Point += range;
        OnPointEarned?.Invoke(Point, range);
    }

    public void SpendPoint(int range)
    {
        this.Point -= range;
        OnPointSpend?.Invoke(Point, range);
    }
}
