import axios from '@/libs/api.request'

export const getPermissionList = (data) => {
  return axios.request({
    url: 'rbac/permission/permissionlist',
    method: 'post',
    data
  })
}

// create
export const createPermission = (data) => {
  return axios.request({
    url: 'rbac/permission/createpermission',
    method: 'post',
    data
  })
}

//edit
export const loadPermission = (data) => {
  return axios.request({
    url: 'rbac/permission/editpermission/' + data.id,
    method: 'get'
  })
}

// edit submit
export const editPermission = (data) => {
  return axios.request({
    url: 'rbac/permission/updatepermission',
    method: 'post',
    data
  })
}

// delete
export const deletePermission = (ids) => {
  return axios.request({
    url: 'rbac/permission/deletepermission/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/permission/batchpermission',
    method: 'post',
    data
  })
}


//load role-permission tree
export const loadPermissionTree = (roleid) => {
  return axios.request({
    url: 'rbac/permission/permissiontree/' + roleid,
    method: 'get'
  })
}
