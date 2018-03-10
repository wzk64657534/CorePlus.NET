using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("SynDataInfo")]
    public class SynDataInfoEntity : BaseEntity
    {
        public string AccountName { get; set; }
        public string AccountPwd { get; set; }
        public string Token { get; set; }
        public string DealWithId { get; set; }
        public int? DealWithType { get; set; }
        public DateTime? DealWithDate { get; set; }
        public int? DataType { get; set; }
        public string DataTag { get; set; }
    }
}