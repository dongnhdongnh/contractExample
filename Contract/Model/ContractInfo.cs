using System;

namespace Contract.Model
{
    [Serializable()]
    public class ContractInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Abi { get; set; }
        public string ByteCode { get; set; }
        public int Active { get; set; }                         //1:active 0:unactive
    }
}
