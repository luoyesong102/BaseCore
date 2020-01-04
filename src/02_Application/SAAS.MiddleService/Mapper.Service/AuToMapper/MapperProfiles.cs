//-----------------------------------------------------------------------
// <copyright file= "MapperProfiles.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/12/20 21:14:45 
// Modified by:
// Description: 配置实体映射关系
//-----------------------------------------------------------------------
using AutoMapper;
using Common.Service.DTOModel.Menu;
using Common.Service.DTOModel.Permission;
using Common.Service.DTOModel.Role;
using Common.Servicei.ViewModels.Users;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.Service
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //Todo：配置实体映射 
            //#region DncIcon
            CreateMap<SysMenu, MenuJsonModel>();
            CreateMap<SysRole, RoleJsonModel>();
            CreateMap<SysUser, UserJsonModel>();

            CreateMap<SysPermission, PermissionJsonModel>()
               .ForMember(d => d.MenuName, s => s.MapFrom(x => x.MenuGu.Name))
               .ForMember(d => d.PermissionTypeText, s => s.MapFrom(x => x.Type.ToString()));
            //#endregion
        }
    }
}
      //services.AddAutoMapper();
      //IMapper mapper
      //var entity = _mapper.Map<IconCreateViewModel, DncIcon>(model);
     // 5:对象序列化 arg.orgin_data = new { arg.orgin_orderdata, arg.orgin_orderassigndata }.Serialize(); 组织新的对象 accountmodel.ConvertBySerialize<List<SellerAccountInfoModel>>(); var decoratoraddress = decoratorsettingmodel.address.Deserialize<List<DecoratorAddressModel>>(); decoratoraddress.Serialize();

