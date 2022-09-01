using System.Linq.Expressions;

namespace ProjetoCrudWEB.Models;

public abstract class RepositorioAbstrato<T> where T : IEntidade
{
    public virtual void Add(T objeto) { }
    public virtual void Remove(T objeto) { }
    public virtual void Update(T objeto) { }
    public virtual IEnumerable<T>? GetAll() { return null; }
    public virtual IEnumerable<T>? Get(Expression<Func<T, bool>> predicate) { return null; }
}