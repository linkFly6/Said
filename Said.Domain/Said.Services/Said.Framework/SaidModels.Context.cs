﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Said.Domain.Said.Services.Said.Framework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SaidDBEntities : DbContext
    {
        public SaidDBEntities()
            : base("name=SaidDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Classify> Classify { get; set; }
        public DbSet<Said> Said { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}