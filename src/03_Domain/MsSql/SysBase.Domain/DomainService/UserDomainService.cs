using CacheSqlXmlService.SqlManager;
using Common.Service;
using Common.Servicei.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using SAAS.FrameWork;
using SAAS.FrameWork.IOC;
using SAAS.FrameWork.Util.Exp;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Service.CommonEnum;

namespace SysBase.Domain.DomainService
{
    /// <summary>
    /// 1：领域的基础操作
    /// 2：查询和批量可以不准守编码规范
    /// </summary>
     public  class UserDomainService
    {
        private readonly ISysRepositoryBase<SysUser> _UserRepository; 
        private readonly SysUnitOfWork _unitOfWork;  
        public UserDomainService(ISysRepositoryBase<SysUser> userRepository,SysUnitOfWork unitOfWork)
        {
            _UserRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        public ApiResponsePage<SysUser> UserList(ApiRequestPage requserobj)
        {
            #region 默认排序
            Dictionary<int, string> orderDictionary = new Dictionary<int, string>
            {
                {1, "Id"},
            };
            if (string.IsNullOrEmpty(requserobj.OrderBy))
            {
                requserobj.OrderBy = orderDictionary.FirstOrDefault().Value;
            }
            #endregion
            #region 动态条件查询
            var query = _UserRepository.DBContext.SysUser.AsQueryable();
            if (!string.IsNullOrEmpty(requserobj.Kw))
            {
                query = query.Where(x => x.UserName.Contains(requserobj.Kw.Trim()) || x.DisplayName.Contains(requserobj.Kw.Trim()));
            }
            #endregion
            #region 分页查询
            var dataRequest = new ApiRequestPage<IQueryable<SysUser>>()
            {
                PageIndex = requserobj.PageIndex,
                PageSize = requserobj.PageSize,
                OrderBy = requserobj.OrderBy,
                SortCol = requserobj.SortCol,
                Where = query
            };
            ApiResponsePage<SysUser> pageuserlist = _UserRepository.ToPage(dataRequest);
            #endregion
            return pageuserlist;
        }
       
        public SysUser GetUser(string username)
        {
            var sysmodel= _UserRepository.Get(u => u.UserName == username).FirstOrDefault();
            return sysmodel;
        }
        public  void UpdateStatus(UserStatus status, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_User SET Status=@Status WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@Status", (int)status));
            _UserRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }

        public bool SaveRoles(List<SysUserRoleMapping> model, Guid userid)
        {
           
            var success = true;
            if (model.Count > 0)
            {
                _unitOfWork.BeginTransaction();
                _UserRepository.ExecuteSql("DELETE FROM Sys_UserRoleMapping WHERE UserId={0}", userid);
                _UserRepository.AddList(model);
                success = _unitOfWork.SaveChanges() > 0;
                _unitOfWork.CommitTrans();
               
            }

            return success;
        }
        public SysUser GetUserKey(Guid id)
        {

            var sysmodel = _UserRepository.GetByKey(id);
            return sysmodel;
        }
        public void CreateUser(SysUser model)
        {
           if( _UserRepository.IsExist<SysUser>(x => x.UserName == model.UserName))
            {
                throw new BaseException("该用户已存在");
            }
            _UserRepository.Add(model);
            _unitOfWork.SaveChanges();
        }
        public void EditUser(SysUser model)
        {

            _UserRepository.Update(model);
            _unitOfWork.SaveChanges();
        }
        public void UpdateIsDeleteUser(CommonEnum.IsDeleted isDeleted, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_User SET IsDeleted=@IsDeleted WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
            _UserRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        /// <summary>
        /// sqlserver测试DEMO所有和数据库操作的形式
        /// </summary>
        /// <returns></returns>
        public List<SysUser>   GetUserList()
        {
            #region 基础表达式树增删改查操作
            var result =  _UserRepository.GetByKey(Guid.Parse("D7F32600-64C3-484D-A933-2D4A62BDA0BC"));
            bool isuser= _UserRepository.IsExist<SysUser>(u => u.Id == Guid.Parse("D7F32600-64C3-484D-A933-2D4A62BDA0BC"));
            var userlist=  _UserRepository.LoadListAll<SysUser>(u => u.Status == 0);    
         
            #endregion
            #region 执行sql语句
            List<SqlParameter> paraList = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName="@Description",SqlDbType = System.Data.SqlDbType.NVarChar,Value = "来到地球"},
                  
                };
            _UserRepository.ExecuteSql(@"update Sys_User set Description=@Description", paraList);
            _unitOfWork.SaveChanges();
            List<SqlParameter> paraList1 = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName="@status",SqlDbType = System.Data.SqlDbType.Int,Value = 1}
                    
                };
            _UserRepository.LoadAllBySql<SysUser>("select * from  Sys_User where status=@status", paraList1.ToArray()).ToList();
            #region  从XMl取出数据并参数化条件
            ICondition where = ConditionBulider.Acquire();
            List<QueryParameter> param = new List<QueryParameter>();
            where.And(Field.Val("1") == Field.Val("1"));

            param.Add(new QueryParameter()
            {
                Name = "@where",
                Value = where.ToString()
            });
            /////////////////////////////sql替换变量///////////////////////////////////////////////////////
            List<SqlParameter> parmlist;
            var sqlEntity = DataCommandHelper.FindSQLString("LoadSysUser", param.ToArray(), out parmlist);//读取XML至内存，将xml中@where替换为动态生成的条件,及sqlparm参数
            string sqls = sqlEntity;

            /////////////////////////////////获取集合对象/////////////////////////////////////////////////////////
            List<SysUser> prizelist = _UserRepository.LoadAllBySql<SysUser>(sqls, parmlist.ToArray()).ToList();


            /////////////////////////////////分页传递对象/////////////////////////////////////////////////////////
            ApiRequestPage<SysUser> sqldicmodel = new ApiRequestPage<SysUser>();
            sqldicmodel.PageIndex = 1;
            sqldicmodel.PageSize = 10;
            sqldicmodel.OrderBy = "Id";
            sqldicmodel.SortCol = Sort.Asc;
            var sqldataRequest = new ApiRequestPage()
            {
                PageIndex = sqldicmodel.PageIndex,
                PageSize = sqldicmodel.PageSize,
                OrderBy = sqldicmodel.OrderBy,
                SortCol = sqldicmodel.SortCol,
            };

            ApiResponsePage<SysUser> listdto1 = _UserRepository.ToPageRowNumber<SysUser>(sqldataRequest, sqls, parmlist.ToArray());

            #endregion
            #endregion
            #region 执行iqeurybel动态查询
            var query=   _UserRepository.DBContext.SysUser.AsQueryable();
            if (true)
            {
                query = query.Where(x => x.IsDeleted ==0);
            }
            #region 动态分页
            ////////////////////////////默认排序///////////////////////////////////////
            ApiRequestPage<SysUser> dicmodel = new ApiRequestPage<SysUser>();
            dicmodel.Where = new SysUser();
            dicmodel.Where.UserName = "admin";
            Dictionary<int, string> orderDictionary = new Dictionary<int, string>
            {
                {1, "Id"},
            };
            if (string.IsNullOrEmpty(dicmodel.OrderBy))
            {
                dicmodel.OrderBy = orderDictionary.FirstOrDefault().Value;
            }

            var list = _UserRepository.DBContext.SysUser.AsQueryable();
            if (!string.IsNullOrEmpty(dicmodel.Where.UserName))
            {
                list = list.Where(u => u.UserName.Contains(dicmodel.Where.UserName) || u.UserName.Contains(dicmodel.Where.UserName));
            }
           
            var dataRequest = new ApiRequestPage<IQueryable<SysUser>>()
            {
                PageIndex = 1,
                PageSize = 10,
                OrderBy = dicmodel.OrderBy,
                SortCol = Sort.Desc,
                Where = list
            };
            ApiResponsePage<SysUser> pageactivity = _UserRepository.ToPage(dataRequest);
            #endregion

            #endregion


            return prizelist.ToList();
        }
    }
}