using System.Collections;

public interface IMovable
{
    public abstract float MoveSpeed { get; set; }
    public abstract IEnumerator Move();
}
