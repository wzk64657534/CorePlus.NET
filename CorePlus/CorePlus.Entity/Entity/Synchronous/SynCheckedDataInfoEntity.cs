using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Core;

namespace CorePlus.Entity
{
    [Table("SynCheckedDataInfo")]
    public class SynCheckedDataInfoEntity : BaseEntity
    {
        public string AccountName { get; set; }
        public string DealWithId { get; set; }
        public int? DealWithType { get; set; }
        public DateTime? DealWithDate { get; set; }
        public int? DataType { get; set; }
        public string DataTag { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsDownLoad { get; set; }
    }
}
