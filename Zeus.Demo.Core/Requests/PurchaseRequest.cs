using Zeus.Demo.Core.Models;

namespace Zeus.Demo.Core.Requests
{
    public class PurchaseRequest : ICloneable
    {
        public ProductType ProductType { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
