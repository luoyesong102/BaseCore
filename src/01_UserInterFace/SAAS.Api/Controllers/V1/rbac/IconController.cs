/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/


using Common.Servicei.ViewModels.Users;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OauthService.Models.AuthContext;
using SAAS.Api.Router;
using SAAS.FrameWork;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.FrameWork.Util.SsError;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using User.Service;
using User.Service.ViewModels.Icon;
using SAAS.FrameWork.Extensions;
using Common.Service;
using static Common.Service.CommonEnum;
using Common.Service.ViewModels.Icon;

namespace SAAS.Api.Controllers.V1.Sys
{
    /// <summary>
    /// 1：图标管理
    /// 2：业务对象的校验和验证
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "rbac")]
    [Authorize]
    public class IconController : BaseApiController
    {
     
       
        private readonly IIconService _iconService;
        public IconController(IIconService iconService)
        {
          
            _iconService = iconService;
        }
        /// <summary>
        /// 获取图标列表
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("iconlist")]
        public ApiReturn<ApiResponsePage<SysIcon>> GetIconList(ApiRequestPage<CommonModel> requserobj)
        {
            return _iconService.GetIconList(requserobj);
        }
       /// <summary>
       /// 根据关键字查找KEY
       /// </summary>
       /// <param name="kw"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("findiconlistbykw/{kw}")]
        public ApiReturn<object> FindByKeyword(string kw)
        {
            if (string.IsNullOrEmpty(kw))
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "没有查询到数据"
                };
            }
            var list = _iconService.GetIconListByKey(kw);
            var data = list.Select(x => new { x.Code, x.Color, x.Size }).ToList();
            return data;
        }

        /// <summary>
        /// 创建图标
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("createicon")]
        public ApiReturn CreateIcon(IconCreateViewModel model)
        {
            if (model.Code.Trim().Length <= 0)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "请输入图标名称"
                };

            }
            var sysicon = model.ConvertBySerialize<SysIcon>();
            sysicon.CreatedOn = DateTime.Now;
            sysicon.CreatedByUserGuid = AuthContextService.CurrentUser.UserId;
            sysicon.CreatedByUserName = AuthContextService.CurrentUser.UserName;
            _iconService.CreateIcon(sysicon);
            return true;
        }
        /// <summary>
        /// 编辑图标
        /// </summary>
        /// <param name="id">图标ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("geticon/{id}")]
        public ApiReturn<IconUpdateViewModel> GetIconById(int id)
        {
            var entity = _iconService.GetIconById(id);
            var viewiconmodel = entity.ConvertBySerialize<IconUpdateViewModel>();
            return viewiconmodel;
        }
        /// <summary>
        /// 保存编辑后的图标信息
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateicon")]
        public ApiReturn UpdateIcon(IconUpdateViewModel model)
        {

            if (model.Code.Trim().Length <= 0)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "请输入图标名称"
                };

            }
            if (_iconService.ExistIcon(model.Code, model.Id))
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "图标已存在"
                };
            }
            var entity = _iconService.GetIconById(model.Id);
            entity.Code = model.Code;
            entity.Color = model.Color;
            entity.Custom = model.Custom;
            entity.Size = model.Size;
            entity.IsDeleted = (int)model.IsDeleted;
            entity.ModifiedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.UserName;
            entity.ModifiedOn = DateTime.Now;
            entity.Status = (int)model.Status;
            entity.Description = model.Description;
            _iconService.UpdateIcon(entity);
            return true;
        }

        /// <summary>
        /// 删除图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleteicon/{ids}")]
        public ApiReturn DeleteIcon(string ids)
        {
            _iconService.UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return true;
        }
        /// <summary>
        /// 恢复图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("recovericon/{ids}")]
        public ApiReturn Recover(string ids)
        {

            _iconService.UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
            return true;
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="batchmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("batchicon")]
        public ApiReturn BatchIcon(IconBatchViewModel batchmodel)
        {

            switch (batchmodel.command)
            {
                case "delete":
                    _iconService.UpdateIsDelete(CommonEnum.IsDeleted.Yes, batchmodel.ids);
                    break;
                case "recover":
                    _iconService.UpdateIsDelete(CommonEnum.IsDeleted.No, batchmodel.ids);
                    break;
                case "forbidden":
                    _iconService.UpdateStatus(UserStatus.Forbidden, batchmodel.ids);
                    break;
                case "normal":
                    _iconService.UpdateStatus(UserStatus.Normal, batchmodel.ids);
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 创建图标
        /// </summary>
        /// <param name="model">多行图标视图</param>
        /// <returns></returns>
        [HttpPost]
        [Route("importicon")]
        public ApiReturn Import(IconImportViewModel model)
        {
            if (model.Icons.Trim().Length <= 0)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "没有可用的图标"
                };
            }
            var models = model.Icons.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new SysIcon
            {
                Code = x.Trim(),
                CreatedByUserGuid = AuthContextService.CurrentUser.UserId,
                CreatedOn = DateTime.Now,
                CreatedByUserName = "超级管理员"
            });
            _iconService.CreateIconList(models.ToList());
            return true;
        }
    }
}