using System;
using System.Linq;
using Core.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistance.SQLDatabase
{
    public class SQLDbCommandContext : SQLDBContext
    {
        public SQLDbCommandContext(DbContextOptions<SQLDBContext> options) : base(options) { }
    }
}