using log4net;
using PMS.Service;
using PMS.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service
{
    class MyDbContext:DbContext
    {
        private static ILog log = LogManager.GetLogger(typeof(MyDbContext));

        public MyDbContext():base("name=connstr")
            //name=conn1表示使用连接字符串中名字为conn1的去连接数据库
        {
            Database.SetInitializer<MyDbContext>(null);
            this.Database.Log = (sql) =>
            {
                log.DebugFormat("EF执行SQL：{0}", sql);
            };
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }
        public DbSet<ExamTypeEntity> ExamTypes { get; set; }


    }
}
