using System.Collections;

public interface IHealable
{
    public abstract short Health { get; set; }
    public abstract float HealingSpeed { get; set; }
    public abstract bool CannotHealing { get; set; }
    public abstract bool IsDead { get; set; }
    public abstract void Healing(short value);
    public abstract void Damage(short value);
    public abstract IEnumerator Dead();
}
