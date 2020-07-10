using System.ComponentModel.DataAnnotations;

namespace ShopApp.Core
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
