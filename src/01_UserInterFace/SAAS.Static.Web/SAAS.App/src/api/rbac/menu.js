import axios from "@/libs/api.request";

export const getMenuList = data => {
  return axios.request({
    url: "rbac/menu/menulist",
    method: "post",
    data
  });
};

// create menu
export const createMenu = data => {
  return axios.request({
    url: "rbac/menu/createmenu",
    method: "post",
    data
  });
};

//load menu
export const loadMenu = data => {
  return axios.request({
    url: "rbac/menu/editmenu/" + data.id,
    method: "get"
  });
};

// edit menu
export const editMenu = data => {
  return axios.request({
    url: "rbac/menu/updatemenu",
    method: "post",
    data
  });
};

// delete menu
export const deleteMenu = ids => {
  return axios.request({
    url: "rbac/menu/deletemenu/" + ids,
    method: "get"
  });
};

// batch command
export const batchCommand = data => {
  return axios.request({
    url: "rbac/menu/batchmenu",
    method: "post",
    data
  });
};

//load menu truee
export const loadMenuTree = (id) => {
  let url = "rbac/menu/menutree";
  if (id != null) {
    url += "/" + id;
  }
  return axios.request({
    url: url,
    method: "get"
  });
};
