﻿using Microsoft.EntityFrameworkCore;
using QuoNDS.Models;
using YMTWEB.Models;

namespace YMTWEB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Item> Items { get; set; } // เพิ่ม DbSet สำหรับแต่ละ Entity
        //YMTGUser ชื่อ Model
        public DbSet<YMTGUser> YMTG_USER { get; set; }
        
        public DbSet<MasterStyle> MasterStyles { get; set; }

        public DbSet<QuotationRun> QuotationRuns { get; set; }

        public DbSet<MasterProvince> MasterProvinces { get; set; }

        public DbSet<YmtgProductNds> YmtgProducts { get; set; }

        public DbSet<YmtgOrderNds> YmtgOrders { get; set; }

        public DbSet<TypeOrderFrom> TypeOrder { get; set; }

    }
}
