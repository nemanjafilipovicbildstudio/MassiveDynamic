namespace MassiveDynamic.Data.Models
{
    public class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
