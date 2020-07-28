﻿using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

namespace WebAPI.Models
{
    public class PaymentDetailContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PaymentDetailContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("HomeConnection"));
        }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }

    }
}
