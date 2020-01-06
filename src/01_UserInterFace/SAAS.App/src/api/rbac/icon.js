import axios from '@/libs/api.request'

export const getIconList = (data) => {
  return axios.request({
    url: 'rbac/icon/iconlist',
    method: 'post',
    data
  })
}

// create icon
export const createIcon = (data) => {
  return axios.request({
    url: 'rbac/icon/createicon',
    method: 'post',
    data
  })
}

//load icon
export const loadIcon = (data) => {
  return axios.request({
    url: 'rbac/icon/geticon/' + data.id,
    method: 'get'
  })
}

// edit icon
export const editIcon = (data) => {
  return axios.request({
    url: 'rbac/icon/updateicon',
    method: 'post',
    data
  })
}

// delete icon
export const deleteIcon = (ids) => {
  return axios.request({
    url: 'rbac/icon/deleteicon/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  debugger
  return axios.request({
    url: 'rbac/icon/batchicon',
    method: 'post',
    data
  })
}


// batch import
export const batchImportIcon = (data) => {
  return axios.request({
    url: 'rbac/icon/importicon',
    method: 'post',
    data
  })
}


// loadIconDataSource

// find icon data source by keyword
export const findIconDataSourceByKeyword = (data) => {
  return axios.request({
    url: 'rbac/icon/findiconlistbykw/' + data.keyword,
    method: 'get'
  })
}
