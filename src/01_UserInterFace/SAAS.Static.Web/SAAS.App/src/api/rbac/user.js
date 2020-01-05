import axios from '@/libs/api.request'

export const getUserList = (data) => {
  return axios.request({
    url: 'rbac/user/userlist',
    method: 'post',
    data
  })
}

// createUser
export const createUser = (data) => {
  return axios.request({
    url: 'rbac/user/createuser',
    method: 'post',
    data
  })
}

//loadUser
export const loadUser = (data) => {
  debugger;
  return axios.request({
    url: 'rbac/user/edituser/' + data.id,
    method: 'get'
  })
}

// editUser
export const editUser = (data) => {
  return axios.request({
    url: 'rbac/user/updateuser',
    method: 'post',
    data
  })
}

// delete user
export const deleteUser = (ids) => {
  return axios.request({
    url: 'rbac/user/deleteuser/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/user/batchuser',
    method: 'post',
    data
  })
}

// save user roles
export const saveUserRoles = (data) => {
  return axios.request({
    url: 'rbac/user/saveroles',
    method: 'post',
    data
  })
}
