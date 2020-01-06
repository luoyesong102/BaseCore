<template>
  <div>
    <Card>
      <tables
        ref="tables"
        editable
        searchable
        :border="false"
        size="small"
        search-place="top"
        v-model="stores.icon.data"
        :totalCount="stores.icon.query.totalCount"
        :columns="stores.icon.columns"
        @on-delete="handleDelete"
        @on-edit="handleEdit"
        @on-select="handleSelect"
        @on-selection-change="handleSelectionChange"
        @on-refresh="handleRefresh"
        :row-class-name="rowClsRender"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
         @on-sort-change="handleSortChange"
      >
        <div slot="search">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.icon.query.kw"
                      placeholder="输入关键字搜索..."
                      @on-search="handleSearchIcon()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.icon.query.where.isDeleted"
                        @on-change="handleSearchIcon"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.icon.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                      <Select
                        slot="prepend"
                        v-model="stores.icon.query.where.status"
                        @on-change="handleSearchIcon"
                        placeholder="图标状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.icon.sources.statusSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                    </Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button
                    class="txt-success"
                    icon="md-redo"
                    title="恢复"
                    @click="handleBatchCommand('recover')"
                  ></Button>
                  <Button
                    class="txt-danger"
                    icon="md-hand"
                    title="禁用"
                    @click="handleBatchCommand('forbidden')"
                  ></Button>
                  <Button
                    class="txt-success"
                    icon="md-checkmark"
                    title="启用"
                    @click="handleBatchCommand('normal')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增图标"
                >新增图标</Button>
              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="false"
      :styles="styles"
    >
      <Form :model="formModel.fields" ref="formIcon" :rules="formModel.rules">
        <FormItem label="图标名称" prop="code" label-position="left">
          <Input v-model="formModel.fields.code" placeholder="请输入图标名称"/>
        </FormItem>
        <FormItem label="自定义图标" label-position="top">
          <Input v-model="formModel.fields.custom" placeholder="请输入自定义图标"/>
        </FormItem>
        <Row :gutter="8">
          <Col span="12">
            <FormItem label="图标状态" label-position="left">
              <i-switch
                size="large"
                v-model="formModel.fields.status"
                :true-value="1"
                :false-value="0"
              >
                <span slot="open">正常</span>
                <span slot="close">禁用</span>
              </i-switch>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="图标大小" label-position="left">
              <InputNumber v-model="formModel.fields.size" placeholder="图标大小"></InputNumber>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="8">
          <Col span="12">
            <FormItem label="图标颜色" label-position="top">
              <ColorPicker v-model="formModel.fields.color" placeholder="图标颜色"/>
            </FormItem>
          </Col>
        </Row>
        <FormItem label="备注" label-position="top">
          <Input
            type="textarea"
            v-model="formModel.fields.description"
            :rows="4"
            placeholder="图标备注信息"
          />
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitIcon">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
        <Button
          style="margin-left: 8px"
          icon="md-arrow-up"
          @click="handleOpenBatchImportDrawer"
        >批量导入</Button>
      </div>
    </Drawer>
    <Drawer
      title="批量导入图标"
      v-model="formModel.batchImport.opened"
      width="360"
      :mask-closable="false"
    >
      <Form>
        <FormItem label="批量图标" label-position="top">
          <Input
            type="textarea"
            v-model="formModel.batchImport.icons"
            :rows="16"
            placeholder="以回车分隔,每行一个图标名称"
          />
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleBatchSubmitIcon">保 存</Button>
        <Button
          style="margin-left: 8px"
          icon="md-close"
          @click="formModel.batchImport.opened = false"
        >取 消</Button>
      </div>
    </Drawer>
  </div>
</template>

<script>
import Tables from "_c/tables";
import {
  getIconList,
  createIcon,
  loadIcon,
  editIcon,
  deleteIcon,
  batchCommand,
  batchImportIcon
} from "@/api/rbac/icon";
export default {
  name: "rbac_icon_page",
  components: {
    Tables
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },
      formModel: {
        opened: false,
        title: "创建图标",
        mode: "create",
        selection: [],
        fields: {
          id: 0,
          code: "",
          size: 24,
          color: "",
          custom: "",
          isLocked: 0,
          status: 1,
          isDeleted: 0,
          description: ""
        },
        rules: {
          code: [
            {
              type: "string",
              required: true,
              message: "请输入图标名称",
              min: 2
            }
          ]
        },
        batchImport: {
          opened: false,
          icons: ""
        }
      },
      stores: {
        icon: {
          query: {
            totalCount: 0,
            pageSize: 20,
            PageIndex: 1,
            kw: "",
            SortCol: 1, 
            OrderBy:"Id",
            where:{ isDeleted: 0,
            status: -1}
          },
          sources: {
            isDeletedSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "正常" },
              { value: 1, text: "已删" }
            ],
            statusSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ],
            statusFormSources: [
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ]
          },
          columns: [
            { type: "selection", width: 30, key: "handle" },
            {
              title: "图标",
              key: "code",
              width: 80,
              align: "center",
              render: (h, params) => {
                return h("Icon", {
                  props: {
                    type: params.row.code,
                    size: 24,
                    color: params.row.color
                  }
                });
              }
            },
            { title: "图标名称", key: "code", width: 250, sortable: true },
            { title: "自定义", key: "custom", width: 150 },
            { title: "大小", key: "size", width: 60 },
            { title: "颜色", key: "color", width: 80 },
            {
              title: "状态",
              key: "status",
              align: "center",
              width: 120,
              render: (h, params) => {
                let status = params.row.status;
                let statusColor = "success";
                let statusText = "正常";
                switch (status) {
                  case 0:
                    statusText = "禁用";
                    statusColor = "default";
                    break;
                }
                return h(
                  "Tooltip",
                  {
                    props: {
                      placement: "top",
                      transfer: true,
                      delay: 500
                    }
                  },
                  [
                    //这个中括号表示是Tooltip标签的子标签
                    h(
                      "Tag",
                      {
                        props: {
                          //type: "dot",
                          color: statusColor
                        }
                      },
                      statusText
                    ), //表格列显示文字
                    h(
                      "p",
                      {
                        slot: "content",
                        style: {
                          whiteSpace: "normal"
                        }
                      },
                      statusText //整个的信息即气泡内文字
                    )
                  ]
                );
              }
            },
            {
              title: "创建时间",
              width: 90,
              ellipsis: true,
              tooltip: true,
              key: "createdOn"
            },
            {
              title: "创建者",
              key: "createdByUserName",
              ellipsis: true,
              tooltip: true
            },
            {
              title: "操作",
              align: "center",
              key: "handle",
              width: 150,
              className: "table-command-column",
              options: ["edit"],
              button: [
                (h, params, vm) => {
                  return h(
                    "Poptip",
                    {
                      props: {
                        confirm: true,
                        title: "你确定要删除吗?"
                      },
                      on: {
                        "on-ok": () => {
                          vm.$emit("on-delete", params);
                        }
                      }
                    },
                    [
                      h(
                        "Tooltip",
                        {
                          props: {
                            placement: "left",
                            transfer: true,
                            delay: 1000
                          }
                        },
                        [
                          h("Button", {
                            props: {
                              shape: "circle",
                              size: "small",
                              icon: "md-trash",
                              type: "error"
                            }
                          }),
                          h(
                            "p",
                            {
                              slot: "content",
                              style: {
                                whiteSpace: "normal"
                              }
                            },
                            "删除"
                          )
                        ]
                      )
                    ]
                  );
                },
                (h, params, vm) => {
                  return h(
                    "Tooltip",
                    {
                      props: {
                        placement: "left",
                        transfer: true,
                        delay: 1000
                      }
                    },
                    [
                      h("Button", {
                        props: {
                          shape: "circle",
                          size: "small",
                          icon: "md-create",
                          type: "primary"
                        },
                        on: {
                          click: () => {
                            vm.$emit("on-edit", params);
                            vm.$emit("input", params.tableData);
                          }
                        }
                      }),
                      h(
                        "p",
                        {
                          slot: "content",
                          style: {
                            whiteSpace: "normal"
                          }
                        },
                        "编辑"
                      )
                    ]
                  );
                }
              ]
            }
          ],
          data: []
        }
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode == "create") {
        return "创建图标";
      }
      if (this.formModel.mode == "edit") {
        return "编辑图标";
      }
      return "";
    },
    selectedRows() { //选中的行
      return this.formModel.selection;
    },
    selectedRowsId() {//选中的行的主键ID集合
      return this.formModel.selection.map(x => x.id);
    }
  },
  methods: {
    loadIconList() {
      getIconList(this.stores.icon.query).then(res => {
        this.stores.icon.data = res.data.data.body;
        debugger
        this.stores.icon.query.totalCount = res.data.data.count;
      });//加载datatable集合列表
    },
    handleSortChange(column) {
      this.stores.icon.query.SortCol = column.order;
      this.stores.icon.query.OrderBy = column.key;
      this.loadIconList();
    },
    handleOpenFormWindow() {//显示from表单页面
      this.formModel.opened = true;
    },
    handleCloseFormWindow() {//关闭from表单页面
      this.formModel.opened = false;
    },
    handleSwitchFormModeToCreate() {//创建表单
      this.formModel.mode = "create";
    },
    handleSwitchFormModeToEdit() {//编辑表单
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    handleEdit(params) {//编辑表单
      this.handleSwitchFormModeToEdit();
      this.handleResetFormIcon();
      this.doLoadIcon(params.row.id);//获取表单显示值
    },
    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },//选中
    handleRefresh() {
      this.loadIconList();
    },//刷新
    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      this.handleResetFormIcon();
    },//显示创建页面
    handleSubmitIcon() {
      let valid = this.validateIconForm();
      if (valid) {
        if (this.formModel.mode == "create") {
          this.doCreateIcon();
        }
        if (this.formModel.mode == "edit") {
          this.doEditIcon();
        }
      }
    },//提交表单
    handleResetFormIcon() {
      this.$refs["formIcon"].resetFields();
    },//重置表单
    doCreateIcon() {
      createIcon(this.formModel.fields).then(res => {
        if (res.data.success == true) {
          this.$Message.success("执行成功");
          this.loadIconList();
          this.handleCloseFormWindow();
        } else {
          this.$Message.warning(res.data.error.errorMessage);
        }
      });
    },//创建表单
    doEditIcon() {
      editIcon(this.formModel.fields).then(res => {
        if (res.data.success == true) {
          this.$Message.success("执行成功");
          this.loadIconList();
          this.handleCloseFormWindow();
        } else {
          this.$Message.warning(res.data.error.errorMessage);
        }
      });
    },//编辑表单
    validateIconForm() {
      let _valid = false;
      this.$refs["formIcon"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
          _valid = false;
        } else {
          _valid = true;
        }
      });
      return _valid;
    },//验证表单
    doLoadIcon(id) {
      loadIcon({ id: id }).then(res => {
        this.formModel.fields = res.data.data;
      });
    },//根据ID获取表单信息
    handleDelete(params) {
      this.doDelete(params.row.id);
    },//删除表单
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }

 
      deleteIcon(ids).then(res => {
        if (res.data.success == true) {
          this.$Message.success("删除成功");
          this.loadIconList();
        } else {
          this.$Message.warning(res.data.error.errorMessage);
        }
      });
    },//删除表单
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doBatchCommand(command);
        }
      });
    },
    doBatchCommand(command) {
      debugger
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.success == true) {
          this.$Message.success("执行成功");
          this.handleCloseFormWindow();
          this.formModel.batchImport.opened = false;
          this.loadIconList();
          this.formModel.selection=[];
        } else {
          this.$Message.warning(res.data.error.errorMessage);
        }
        this.$Modal.remove();
      });
    },//批量操作
    handleSearchIcon() {
      this.loadIconList();
    },//查询表单列表
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    handleOpenBatchImportDrawer() {
      this.formModel.batchImport.opened = true;
    },//显示批量导入
    handleBatchSubmitIcon() {
      var data = { icons: this.formModel.batchImport.icons };
      batchImportIcon(data).then(res => {
        if (res.data.success == true) {
          this.$Message.success("执行成功");
          this.handleCloseFormWindow();
          this.formModel.batchImport.opened = false;
          this.loadIconList();
        } else {
          this.$Message.warning(res.data.error.errorMessage);
        }
      });
    },//提交批量表单
    handlePageChanged(page) {
     
      this.stores.icon.query.PageIndex = page;
      this.loadIconList();
    },//页面变更
    handlePageSizeChanged(pageSize) {
      this.stores.icon.query.pageSize = pageSize;
      this.loadIconList();
    }
  },//页面页码变更
  mounted() {
    this.loadIconList();
  }//加载页面数据
};
</script>
