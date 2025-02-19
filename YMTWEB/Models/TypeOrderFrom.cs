﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoNDS.Models
{
    [Table("YMTG_OrderType_NDS")]
    public class TypeOrderFrom
    {
        [Key]
        public int Code { get; set; }             // รหัส
        public string TypeRecapFrom { get; set; }    // ประเภทการ Recap
        public string Description { get; set; }      // รายละเอียด
        public string CodeChar { get; set; }         // ตัวอักษรโค้ด
    }
}
