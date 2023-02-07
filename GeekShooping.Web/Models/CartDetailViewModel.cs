using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.Web.Models
{
    public class CartDetailViewModel
    {
        public long Id { get; set; }
        public long CartHeaderId { get; set; }
        public CartHeaderViewModel CartHeader { get; set; }
        public long ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public int Count { get; set; }
    }
}
