using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tabtba.Persistence.Data.DbContexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // تم تنظيف الباسورد، تغيير البورت لـ 5432، وإضافة تمديد وقت الانتظار لمنع الـ Timeout
        optionsBuilder.UseNpgsql(
            "Host=aws-1-eu-central-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.zykejgnznfrhwbhaegtm;Password=0243127679@Mo3hdy;SSL Mode=Require;Trust Server Certificate=true;Command Timeout=120",
            options => options.EnableRetryOnFailure()
        );

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}