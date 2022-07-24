public interface IAttackable
{
    public abstract int Attack { get; set; }
    public abstract float AttackSpeed { get; set; }
    public abstract void AttackEntity();
}