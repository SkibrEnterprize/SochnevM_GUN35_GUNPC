using UniRx;

public interface IBonusModel
{
    IReadOnlyReactiveCollection<Bonuses> CollectedBonuses { get; }
}
