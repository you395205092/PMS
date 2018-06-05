using PMS.Common;
using PMS.Service;
using PMS.IService;
using PMS.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.DTO;
using System.Data.Entity;

namespace PMS.Service
{
    public class AdminUserService : IAdminUserService
    {
        public long Add(string name, string password)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                if (CheckName(name))
                {
                    throw new ArgumentException("管理员名称已经存在" + name);
                }
                AdminUserEntity ef = new AdminUserEntity();
                ef.Name =name;
                string Salt = CommonHelper.CreateVerifyCode(5);
                ef.PasswordSalt = Salt;
                ef.PasswordHash = CommonHelper.CalcMD5(Salt+password);

                ctx.SaveChanges();
                return ef.Id;
            }
        }

        public bool Edit(long Id, string PassWord)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var data=bs.GetById(Id);
                if (data==null)
                {
                    return false;
                    throw new ArgumentException("该管理员不存在id:" + Id);
                }
                string Salt = data.PasswordSalt;
                data.PasswordHash = CommonHelper.CalcMD5(Salt + PassWord);
                
                ctx.SaveChanges();
                return true;
            }
        }

        public bool CheckLogin(string name, string password)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var data = bs.GetAll().SingleOrDefault(u => u.Name == name);
                if (data==null)
                {
                    return false;
                }
                string dbHash = data.PasswordHash.ToUpper();
                string userHash = CommonHelper.CalcMD5(data.PasswordSalt + password).ToUpper();
                return userHash == dbHash;
            }
        }

        public bool CheckName(string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().Any(e => e.Name == name);
            }
        }

        public AdminUserDTO[] GetAll()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().ToList().Select(e=>ToDTO(e)).ToArray();
            }
        }

        public AdminUserDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var data = bs.GetById(id);
                if (data==null)
                {
                    return null;
                }
                return ToDTO(data);
            }
        }

        public AdminUserDTO GetByName(string Name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return ToDTO(bs.GetAll().SingleOrDefault(p => p.Name == Name));
            }
        }

        public AdminUserDTO[] GetPagedData(int pageSize, int currentIndex)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().OrderByDescending(e => e.Id).Skip((currentIndex - 1) * pageSize).Take(pageSize).Select(e=>ToDTO(e)).ToArray();
            }
        }

        public long GetTotalCount()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().LongCount();
            }
        }

        public void MarkDeleted(long adminId)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                bs.MarkDeleted(adminId);
            }
        }

        public AdminUserDTO ToDTO(AdminUserEntity ef)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.LastLoginErrorDateTime = ef.LastLoginErrorDateTime;
            dto.LoginErrorTimes = ef.LoginErrorTimes;
            dto.Name = ef.Name;
            return dto;
        } 
    }
}
