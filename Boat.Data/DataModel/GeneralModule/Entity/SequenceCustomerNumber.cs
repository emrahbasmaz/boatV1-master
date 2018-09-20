using Dapper.Contrib.Extensions;

namespace Boat.Data.DataModel.GeneralModule.Entity
{
    [Table("SequenceCustomerTABLE")]
    public class SequenceCustomerNumber
    {
        [Key]
        public long ID { get; set; }
       
    }
}
