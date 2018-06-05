using PMS.DTO;
using PMS.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.IService
{
    public interface IAdminUserService:IServiceSupport
    {
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        long Add(string name, string password);
        /// <summary>
        /// 判断名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CheckName(string name);
        /// <summary>
        /// 判断登陆是否成功
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckLogin(string name, string password);

        /// <summary>
        /// 获取所有管理员
        /// </summary>
        /// <returns></returns>
        AdminUserDTO[] GetAll();
        /// <summary>
        /// 获取管理员信息，通过id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdminUserDTO GetById(long id);
        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        bool Edit(long Id, string PassWord);
        /// <summary>
        /// 获取管理员列表数组，
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        AdminUserDTO[] GetPagedData(int pageSize, int currentIndex);
        /// <summary>
        /// 获取管理员总数
        /// </summary>
        /// <returns></returns>
        long GetTotalCount();
        /// <summary>
        /// 通过管理员名称，获取管理员
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        AdminUserDTO GetByName(string Name);
        /// <summary>
        /// 删除管理员（软删除）
        /// </summary>
        /// <param name="adminId"></param>
        void MarkDeleted(long adminId);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>

    }
}
