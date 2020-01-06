
<template>
  <div>
    <h1>这是一个用于演示的页面</h1>
   
  <Button v-can="'create'" icon="md-create" type="primary" title="新增用户">新增(有权限才显示)</Button>
     <Upload multiple type="drag" action="http://localhost:64832/api/v1/upload/upload/insertpicture">
        <div style="padding: 20px 0">
            <Icon type="ios-cloud-upload" size="52" style="color: #3399ff"></Icon>
            <p>点击或将文件拖拽到这里上传</p>
        </div> </Upload>
  </div>
</template>

<script>
export default {
  name: 'demo_page',
  data () {
    return {

    }
  }
}
</script>
