import _axios from 'axios'
import config from '@/config'
import axios from '@/libs/api.request'

const authUrl = process.env.NODE_ENV === 'development' ? config.authUrl.dev : config.authUrl.pro

export const login = ({
  userName,
  password
}) => {
  var data = {username:userName,password:password};
  return axios.request({
    url: 'Login',
    method: 'post',
    data,
    withPrefix: false,  
    prefix:"api/Auth/"
  })
 
}

export const getUserInfo = (token) => {
  return axios.request({
    url: 'account/account/getuserinfo',
    method: 'get'
  
  })
}

export const logout = (token) => {
  return new Promise((resolve, reject) => {
    resolve()
  })
}

export const getUnreadCount = () => {
  return axios.request({
    url: 'messasge/Message/count',
    hideError: false,
    method: 'get'
  })
}

export const getMessage = () => {
  return axios.request({
    url: 'messasge/Message/init',
    method: 'get'
  })
}

export const getContentByMsgId = msg_id => {
  return axios.request({
    url: 'messasge/Message/content/' + msg_id,
    method: 'get'
  })
}

export const hasRead = msg_id => {
  return axios.request({
    url: 'messasge/Message/hasread/' + msg_id,
    method: 'get',
  })
}

export const removeReaded = msg_id => {
  return axios.request({
    url: 'messasge/Message/removeread/' + msg_id,
    method: 'get'
  })
}

export const restoreTrash = msg_id => {
  return axios.request({
    url: 'messasge/Message/restore/' + msg_id,
    method: 'get'
  })
}
