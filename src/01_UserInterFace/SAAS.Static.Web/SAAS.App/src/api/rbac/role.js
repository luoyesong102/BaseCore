import axios from '@/libs/api.request'

export const getRoleList = (data) => {
  return axios.request({
    url: 'rbac/role/rolelist',
    method: 'post',
    data
  })
}

// createRole
export const createRole = (data) => {
  return axios.request({
    url: 'rbac/role/createrole',
    method: 'post',
    data
  })
}

//loadRole
export const loadRole = (data) => {
  return axios.request({
    url: 'rbac/role/editrole/' + data.id,
    method: 'get'
  })
}

// editRole
export const editRole = (data) => {
  return axios.request({
    url: 'rbac/role/updaterole',
    method: 'post',
    data
  })
}

// delete role
export const deleteRole = (ids) => {
  return axios.request({
    url: 'rbac/role/deleterole/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/role/batchrole',
    method: 'post',
    data
  })
}

//load role list by user guid
export const loadRoleListByUserGuid = (user_guid) => {
  return axios.request({
    url: 'rbac/role/findlistbyuserid/' + user_guid,
    method: 'get'
  })
}

//load role simple list
export const loadSimpleList = () => {
  return axios.request({
    url: 'rbac/role/findsimplelist',
    method: 'get'
  })
}

//assign permissions for role
export const assignPermission = (data) => {
  return axios.request({
    url: 'rbac/role/assignpermission',
    method: 'post',
    data
  })
}
