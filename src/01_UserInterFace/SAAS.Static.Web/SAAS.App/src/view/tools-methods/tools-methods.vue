<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/system/Sys_DictionaryList.js此处编写
 -->
<template>
    <div>
        <view-grid :columns="columns"
                   :detail="detail"
                   :editFormFileds="editFormFileds"
                   :editFormOptions="editFormOptions"
                   :searchFormFileds="searchFormFileds"
                   :searchFormOptions="searchFormOptions"
                   :table="table"
                   :extend="extend">
        </view-grid>
    </div>
</template>

<script>
    import extend from "@/extension/system/Sys_DictionaryList.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'DicList_ID',
                    footer: "Foots",
                    cnName: '字典明细',
                    name: 'Sys_DictionaryList',
                    url: "/Sys_DictionaryList/",
                    sortName: "DicList_ID"
                },
                extend: extend,
                editFormFileds: {"DicValue":"","DicName":"","OrderNo":"","Remark":"","Enable":""},
                editFormOptions: [[{"columnType":"string","title":"数据源Value","field":"DicValue","type":"text"},
                               {"columnType":"string","title":"数据源Text","field":"DicName","type":"text"},
                               {"columnType":"int","title":"排序号","field":"OrderNo","type":"text"},
                               {"columnType":"int","title":"备注","field":"Remark","type"