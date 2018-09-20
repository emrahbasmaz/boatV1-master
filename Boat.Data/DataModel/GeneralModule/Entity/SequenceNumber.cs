using Dapper.Contrib.Extensions;

namespace Boat.Data.DataModel.GeneralModule.Entity
{
    [Table("SequenceTABLE")]
    public class SequenceNumber
    {
        [Key]
        public long ID { get; set; }


    }
}
