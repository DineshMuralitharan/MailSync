﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MailSync.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataEntities : DbContext
    {
        public DataEntities()
            : base("name=DataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CampaignListMapping> CampaignListMappings { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<CampaignType> CampaignTypes { get; set; }
        public virtual DbSet<CustomerList> CustomerLists { get; set; }
        public virtual DbSet<CustomerListMapping> CustomerListMappings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<EmailStatistic> EmailStatistics { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<EmailDetail> EmailDetails { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }
    }
}
