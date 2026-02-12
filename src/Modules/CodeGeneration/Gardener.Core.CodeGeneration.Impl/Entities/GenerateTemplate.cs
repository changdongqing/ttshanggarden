// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.CodeGeneration.Dtos;
using Gardener.Core.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.CodeGeneration.Impl.Entities
{
    /// <summary>
    /// 生成模板
    /// </summary>
    [Table("CodeGen" + nameof(GenerateTemplate))]
    public class GenerateTemplate : GenerateTemplateDto, IEntityBase, IEntitySeedData<GenerateTemplate, MasterDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<GenerateTemplate> HasData(DbContext dbContext, Type dbContextLocator)
        {
            List<GenerateTemplate> data = new List<GenerateTemplate>();
            //资源种子数据
            data.Add(new GenerateTemplate()
            {
                Id = 1,
                TemplateName = "资源种子数据",
                TemplateContent = @"return new[]
{
    @{
       string Resource(Guid id,Guid? parentId,string key,string type,string name,string moduleName,bool supportMultiTenant,string? path=null,string? icon=null)
       {
        string parentIdStr=(parentId==null?""null"":""Guid.Parse(\""""+parentId.ToString()+""\"")"");
        string typeStr=""ResourceType.""+type;
        string pathStr=path==null?""null"":$""\""{path}\"""";
        string iconStr=icon==null?""null"":$""\""{icon}\"""";
        return @""
    new ResourceDto()
    {
        Id=Guid.Parse(""""""+id.ToString()+@""""""),
        ParentId=""+parentIdStr+@"",
        Name=""""""+name+@"""""",
        Key=""""""+key+@"""""",
        ModuleName=""""""+moduleName+@"""""",
        Type=""+typeStr+@"",
        SupportMultiTenant=""+supportMultiTenant.ToString().ToLower()+@"",
        Path=""+pathStr+@"",
        Icon=""+iconStr+@"",
        Order=0,
        Hide=false,
        IsLocked=false,
        IsDeleted=false,
        CreatedTime=DateTimeOffset.Now,
        CreateBy=""""1"""",
        CreateIdentityType=IdentityType.User
    }"";
       }
        Guid menuId=Guid.NewGuid();
        string menu=Resource(menuId,null,Model.EntityConfig.ResourceKeyPrefix+""index"",""Menu"",Model.EntityConfig.MenuName,Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant,Model.EntityConfig.MenuPath,Model.EntityConfig.MenuIcon);
        string searchAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""search"",""Action"",""搜索"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string refreshAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""refresh"",""Action"",""刷新"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string addAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""add"",""Action"",""添加"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string updateAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""update"",""Action"",""更新"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string detailAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""detail"",""Action"",""详情"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string deleteAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""delete"",""Action"",""删除"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string deleteSelectedAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""delete_selected"",""Action"",""删除选中"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);
        string lockAction=Resource(Guid.NewGuid(),menuId,Model.EntityConfig.ResourceKeyPrefix+""lock"",""Action"",""锁定"",Model.EntityConfig.ModuleName,Model.EntityConfig.SupportMultiTenant);

    }
    @(menu)
    @if(Model.EntityConfig.EnableSearch)
    {
    @("",""+searchAction)
    }
    @if(Model.EntityConfig.EnableRefresh)
    {
    @("",""+refreshAction)
    }
    @if(Model.EntityConfig.EnableAdd)
    {
    @("",""+addAction)
    }
    @if(Model.EntityConfig.EnableUpdate)
    {
    @("",""+updateAction)
    }
    @if(Model.EntityConfig.EnableDetail)
    {
    @("",""+detailAction)
    }
    @if(Model.EntityConfig.EnableDelete)
    {
    @("",""+deleteAction)
    }
    @if(Model.EntityConfig.EnableDeleteSelected)
    {
    @("",""+deleteSelectedAction)
    }
    @if(Model.EntityConfig.EnableLock)
    {
    @("",""+lockAction)
    }

};",
                CreatedTime = DateTime.Now,

            });
            //List列表页.razor
            data.Add(new GenerateTemplate()
            {
                Id=2,
                TemplateName= "List列表页.razor",
                TemplateContent= @"@@*page:@(Model.EntityTypeName).razor*@@
@@*gardener-time:@(DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss""))*@@
@@page ""@Model.EntityConfig.MenuPath""
@@inherits ListOperateTableBase<@Model.EntityDtoName, @Model.PrimaryKeyTypeName, @(Model.EntityTypeName+""Edit"")@(Model.DisplayNameResourceTypeName!=null?"",""+Model.DisplayNameResourceTypeName:"""")>
@{
    Dictionary<string,bool> enumDic=new Dictionary<string,bool>();
    Dictionary<string,bool> codeDic=new Dictionary<string,bool>();

    string FirstToLower(string str)
    {
        return char.ToLower(str[0]) + str.Substring(1);
    }
    bool beginTableFilter=false;
}

    @foreach(var fieldDto in Model.Fields)
    {
        
        if(fieldDto.IsEnum)
        {
            if(!enumDic.ContainsKey(fieldDto.TypeName))
            {
                enumDic.Add(fieldDto.TypeName,true);
                if(!beginTableFilter)
                {
@(""@{"")
                    beginTableFilter=true;
                }
@(@""
"")
    @($""    TableFilter<{fieldDto.TypeName}>[] {FirstToLower(fieldDto.TypeName)}EnumFilters = EnumHelper.EnumToList<{fieldDto.TypeName}>().Select(x => {{ return new TableFilter<{fieldDto.TypeName}>() {{ Text = Localizer[EnumHelper.GetEnumDescriptionOrName(x)], Value = x }}; }}).ToArray();"")
@(@""
"")
            } 
        }else if(fieldDto.IsDictCodeField)
        {
            if(!codeDic.ContainsKey(fieldDto.TypeName))
            {
                codeDic.Add(fieldDto.TypeName,true);
                if(!beginTableFilter)
                {
@(""@{"")
                    beginTableFilter=true;
                }
@(@""
"")
    @($""    TableFilter<string>[] {FirstToLower(fieldDto.Name)}DictFilters = DictHelper.GetCodesFromCache<{Model.EntityDtoName}>(p=>p.{fieldDto.Name})?.Select(x => {{ return new TableFilter<string>() {{ Text = Localizer[x.CodeName], Value = x.CodeValue }}; }}).ToArray() ?? new TableFilter<string>[0];"")
@(@""
"")
            } 

        }
        
    }
@if(beginTableFilter)
{
@(@""}"")
}

<div>
    <Table @@ref=""_table""
        TItem=""@Model.EntityDtoName""
        DataSource=""_datas""
        Total=""_total""
        OnChange=""OnChange""
        @@bind-PageSize=""_pageSize""
        @@bind-SelectedRows=""_selectedRows""
        Loading=""_tableLoading.Value""
        Context=""model""
        Size=""ClientConstant.DefaultTableSize""
        PaginationPosition=""@@ClientConstant.TablePaginationPosition""
        RemoteDataSource
        @if(Model.PrimaryKeyName!=null)
        {
            @($""RowKey=\""x=>x.{Model.PrimaryKeyName}\"""")
        }
        >
            <TitleTemplate>
            @if(Model.EntityConfig.EnableSearch)
            {
                <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""search"")"">
                    <TableSearch TDto=""@Model.EntityDtoName"" @@ref=""this._tableSearch"" OnSearch=""OnTableSearch"" Settings=""_tableSearchSettings"" CustomLocalizer=""Localizer"" />
                </ResourceAuthorize>
            }
            <Row>
                <AntDesign.Col Span=""8"">
                @if(Model.EntityConfig.EnableDeleteSelected)
                {
                    <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""delete_selected"")"">
                        <Button Type=""@@ButtonType.Primary"" Icon=""@@IconType.Outline.Delete"" Danger OnClick=""OnClickDeletes"" Loading=""_deletesBtnLoading"">
                        @@Localizer[nameof(SharedLocalResource.DeleteSelected)]
                        </Button>
                    </ResourceAuthorize>
                }
                </AntDesign.Col>
                <AntDesign.Col Span=""16"" Style=""text-align:right"">
                    <Space>
                    @if(Model.EntityConfig.EnableAdd)
                    {
                        <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""add"")"">
                        <SpaceItem>
                            <Button Type=""@@ButtonType.Primary"" Icon=""@@IconType.Outline.Plus"" OnClick=""OnClickAdd"">
                                @@Localizer[nameof(SharedLocalResource.Add)]
                            </Button>
                        </SpaceItem>
                        </ResourceAuthorize>
                    }
                     @if(Model.EntityConfig.EnableRefresh)
                    {
                        <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""refresh"")"">
                        <SpaceItem>
                            <Button Type=""@@ButtonType.Primary"" Icon=""@@IconType.Outline.Reload"" OnClick=""ReLoadTable"">
                                @@Localizer[nameof(SharedLocalResource.Refresh)]
                            </Button>
                        </SpaceItem>
                        </ResourceAuthorize>
                    }
                    </Space>
                </AntDesign.Col>
            </Row>
            </TitleTemplate>
            <ColumnDefinitions>
            @if(Model.PrimaryKeyName!=null && Model.EntityConfig.EnableDeleteSelected)
            {
                <Selection Hidden=""@@_userUnauthorizedResources[""@(Model.EntityConfig.ResourceKeyPrefix+""selected"")""]"" />
            }
            
            @foreach(var fieldDto in Model.Fields.Where(x=>x.FieldConfig.ShowInList).OrderBy(x=>x.FieldConfig.ListOrder))
            {
                string? sortStr=null;
                if(fieldDto.FieldConfig.Sortable && fieldDto.FieldConfig.DefaultSortOrder!=null)
                {
                    sortStr="" DefaultSortOrder=\""@SortDirection.""+(fieldDto.FieldConfig.DefaultSortOrder.Equals(""asc"")?""Ascending"":""Descending"")+""\"""";
                }
                if(fieldDto.Name.Equals(""TenantId"") && Model.EntityConfig.SupportMultiTenant)
                {
                <PropertyColumn Property=""c=>c.Tenant"" Hidden=""@@_userUnauthorizedResources[CommonResourceKeys.SystemTenantAdministratorKey]"">
                    <span>@@model.Tenant?.Name</span>
                </PropertyColumn>
                
                }else if(fieldDto.Name.Equals(""IsLocked""))
                {
                <PropertyColumn Property=""c=>c.@(fieldDto.Name)"" @(fieldDto.FieldConfig.Filterable?"" Filterable"":"""")@(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""")>
                     @if(Model.EntityConfig.EnableLock)
                    {
                    <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""lock"")"">
                        <Authorized>
                            <Switch @@bind-Value=""@@model.IsLocked"" OnChange=""e=>OnChangeIsLocked(model,e)"" Loading=""_lockBtnLoading[model]""></Switch>
                        </Authorized>
                        <NotAuthorized>
                            <TagYesNo Yes=""model.@(fieldDto.Name)""></TagYesNo>
                        </NotAuthorized>
                    </ResourceAuthorize>
                    }else
                    {
                    <TagYesNo Yes=""model.@(fieldDto.Name)""></TagYesNo>
                    }
                </PropertyColumn>
                }else if(fieldDto.TypeName.Equals(nameof(Boolean)))
                {
               <PropertyColumn Property=""c=>c.@(fieldDto.Name)"" @(fieldDto.FieldConfig.Filterable?"" Filterable"":"""")@(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""") >
                    <TagYesNo Yes=""model.@(fieldDto.Name)""></TagYesNo>
                </PropertyColumn>
                }else if(fieldDto.TypeName.Equals(nameof(DateTimeOffset)))
                {
                    <PropertyColumn Property=""c=>c.@(fieldDto.Name)"" @(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""") Format=""@@ClientConstant.DateTimeFormat""/>
                }else if(fieldDto.TypeName.Equals(nameof(DateTime)))
                {
                    <PropertyColumn Property=""c=>c.@(fieldDto.Name)"" @(fieldDto.FieldConfig.Filterable?"" Filterable"":"""")@(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""") Format=""@@ClientConstant.DateTimeFormat""/>
                }else if(fieldDto.IsEnum)
                {
                <PropertyColumn Property=""c=>c.@(fieldDto.Name)"" @(fieldDto.FieldConfig.Filterable?"" Filterable"":"""")@(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""") Filters=""@(FirstToLower(fieldDto.TypeName))EnumFilters"">
                    <TagPro Text=""@(""@model.""+fieldDto.Name)""></TagPro>
                </PropertyColumn>
                }else if(fieldDto.IsDictCodeField)
                {
                <PropertyColumn Property=""c=>c.@(fieldDto.Name)"" TData=""string"" @(fieldDto.FieldConfig.Filterable?"" Filterable"":"""")@(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""") Filters=""@(FirstToLower(fieldDto.Name))DictFilters"">
                    @@{
                    var @(FirstToLower(fieldDto.Name))CodeName =DictHelper.GetCodeNameFromCache<@(Model.EntityDtoName)>(() => model.@(fieldDto.Name));
                    if (@(FirstToLower(fieldDto.Name))CodeName != null)
                    {
                    @(FirstToLower(fieldDto.Name))CodeName = Localizer[@(FirstToLower(fieldDto.Name))CodeName];
                    }
                    }
                    <span>@@(@(FirstToLower(fieldDto.Name))CodeName)</span>
                </PropertyColumn>
                }
                else
                {
                <PropertyColumn Property=""c=>c.@(fieldDto.Name)""@(fieldDto.FieldConfig.Filterable?"" Filterable"":"""")@(fieldDto.FieldConfig.Sortable?"" Sortable"":"""")@(sortStr!=null?sortStr:"""") />
                }
            }
            @if(Model.EntityConfig.EnableUpdate || Model.EntityConfig.EnableDetail || Model.EntityConfig.EnableDelete)
            {
            <ActionColumn>
            <Space>
            @if(Model.EntityConfig.EnableUpdate)
            {
            <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""update"")"">
            <SpaceItem>
                <Tooltip Title=""@@Localizer[nameof(SharedLocalResource.Edit)]"" ArrowPointAtCenter=""true"">
                    <Button Icon=""@@IconType.Outline.Edit"" Type=""@@ButtonType.Primary"" Size=""@@ClientConstant.OperationButtonSize"" OnClick=""()=>OnClickEdit(model.@(Model.PrimaryKeyName))""></Button>
                </Tooltip>
            </SpaceItem>
            </ResourceAuthorize>
            }
             @if(Model.EntityConfig.EnableDetail)
            {
            <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""detail"")"">
            <SpaceItem>
                <Tooltip Title=""@@Localizer[nameof(SharedLocalResource.Detail)]"" ArrowPointAtCenter=""true"">
                    <Button Icon=""@@IconType.Outline.Eye"" Type=""@@ButtonType.Primary"" Size=""@@ClientConstant.OperationButtonSize"" OnClick=""()=>OnClickDetail(model.@(Model.PrimaryKeyName))""></Button>
                </Tooltip>
            </SpaceItem>
            </ResourceAuthorize>
            }
             @if(Model.EntityConfig.EnableDelete)
            {
            <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""delete"")"">
                <SpaceItem>
                    <Tooltip Title=""@@Localizer[nameof(SharedLocalResource.Delete)]"" ArrowPointAtCenter=""true"">
                        <Button Icon=""@@IconType.Outline.Delete"" Type=""@@ButtonType.Primary"" Danger OnClick=""()=>OnClickDelete(model.@(Model.PrimaryKeyName))""></Button>
                    </Tooltip>
                </SpaceItem>
            </ResourceAuthorize>
            }
            </Space> 
            </ActionColumn>
            }
            </ColumnDefinitions>
        </Table>
</div>",
                CreatedTime= DateTime.Now,

            });
            //List列表页.razor.cs
            data.Add(new GenerateTemplate()
            {
                Id=3,
                TemplateName= "List列表页.razor.cs",
                TemplateContent= @"namespace @(Model.EntityConfig.ClientNameSpace).Pages.@(Model.EntityTypeName)View
{
    /// <summary>
    /// @(Model.DisplayName)列表页
    /// </summary>
    public partial class @Model.EntityTypeName : ListOperateTableBase<@Model.EntityDtoName, @Model.PrimaryKeyTypeName, @(Model.EntityTypeName)Edit@(Model.DisplayNameResourceTypeName!=null?"",""+Model.DisplayNameResourceTypeName:"""")>
    { 
    }
}",
                CreatedTime= DateTime.Now,

            });
            //Edit编辑页.razor
            data.Add(new GenerateTemplate() 
            { 
                Id=4,
                TemplateName= "Edit编辑页.razor",
                TemplateContent= @"@using Gardener.Core.Dtos.Constraints;
@@*page:@(Model.EntityTypeName)Edit.razor*@@
@@*gardener-time:@(DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss""))*@@
@@inherits EditOperationDialogBase<@Model.EntityDtoName, @Model.PrimaryKeyTypeName@(Model.DisplayNameResourceTypeName!=null?"",""+Model.DisplayNameResourceTypeName:"""")>

<Form @@ref=""_editForm""
      Loading=""_dialogLoading.Value""
      Model=""_editModel""
      LabelCol=""new ColLayoutParam { Span = 6 }""
      WrapperCol=""new ColLayoutParam { Span = 18 }""
      OnFinish=""OnFormFinish""
      ValidateMode=""FormValidateMode.Complex""
      Context=""model"">
    @foreach(var fieldDto in Model.Fields.Where(x=>x.FieldConfig.ShowInList).OrderBy(x=>x.FieldConfig.ListOrder))
    {
        if(!fieldDto.FieldConfig.ShowInDetail)
        {
            continue;
        }
        string disabledStr=fieldDto.FieldConfig.CanModity?""base.operationIsOnlyReadData"":""true"";
        string rules=fieldDto.FieldConfig.IsUnique?""Rules=\""[_uniqueValueValidatorRule]\"""":"""";
         if(Model.EntityConfig.SupportMultiTenant && fieldDto.Name.Equals(""TenantId""))
        {
        <ResourceAuthorize ResourceKey=""@@CommonResourceKeys.SystemTenantAdministratorKey"">
            <FormItem Label=""@@Localizer[nameof(SharedLocalResource.Tenant)]"">
                <Select DataSource=""_tenants""
                        @@bind-Value=""model.TenantId""
                        ValueName=""@@nameof(SystemTenantDto.Id)""
                        LabelName=""@@nameof(SystemTenantDto.Name)""
                        TItem=""SystemTenantDto""
                        TItemValue=""Guid?""
                        AllowClear
                        Disabled=""@disabledStr"" >
                </Select>
            </FormItem>
        </ResourceAuthorize>
        }else if(fieldDto.TypeName.Equals(nameof(Boolean)))
        {
            if(fieldDto.Name.Equals(nameof(IModelLocked.IsLocked)) && Model.EntityConfig.EnableLock)
            { 
            <FormItem @(rules)>
                <ResourceAuthorize ResourceKey=""@(Model.EntityConfig.ResourceKeyPrefix+""lock"")"">
                    <Authorized>
                        <Switch @@bind-Value=""@@model.IsLocked"" Disabled=""@disabledStr""></Switch>
                    </Authorized>
                    <NotAuthorized>
                        <Switch @@bind-Value=""@@model.IsLocked"" Disabled=""true""></Switch>
                    </NotAuthorized>
                </ResourceAuthorize>
            </FormItem>
            }else if(fieldDto.Name.Equals(nameof(IModelTenantPermission.EmpowerAllTenants)))
            {
            <ResourceAuthorize ResourceKey=""@@CommonResourceKeys.SystemTenantAdministratorKey"">
                <FormItem @(rules)>
                    <Switch @@bind-Value=""model.@(fieldDto.Name)"" Disabled=""@disabledStr"" ></Switch>
                </FormItem>
            </ResourceAuthorize>
            }else
            {
            <FormItem @(rules)>
                <Switch @@bind-Value=""model.@(fieldDto.Name)"" Disabled=""@disabledStr"" ></Switch>
             </FormItem>
            }
        }else if(fieldDto.IsEnum)
        {
            <FormItem @(rules)>
                <Select DataSource=""EnumHelper.EnumToDictionary<@(fieldDto.TypeName)>()""
                        TItem=""KeyValuePair<@(fieldDto.TypeName),string>""
                        TItemValue=""@(fieldDto.TypeName+(fieldDto.IsNullableType?""?"":""""))""
                        ItemValue=""x=>x.Key""
                        ItemLabel=""x=>Localizer[x.Value]""
                        @@bind-Value=""model.@(fieldDto.Name)""
                        Disabled=""@disabledStr"">
                </Select>
            </FormItem>
        }else if(fieldDto.IsDictCodeField)
        {
        <FormItem @(rules)>
            <Select DataSource=""DictHelper.GetCodesFromCache<@Model.EntityDtoName>(() => model.@(fieldDto.Name))""
                    @@bind-Value=""model.@(fieldDto.Name)""
                    ValueName=""@@nameof(CodeDto.CodeValue)""
                    LabelName=""@@nameof(CodeDto.CodeName)""
                    TItem=""CodeDto""
                    TItemValue=""string""
                    AllowClear
                    Disabled=""@disabledStr"">
            </Select>
        </FormItem>
        }else if(fieldDto.Name.Equals(nameof(IModelCreated.CreatedTime)))
        {
            @(@""
            "")
           @(""@if (this.Options.Type.Equals(OperationDialogInputType.Edit) || this.Options.Type.Equals(OperationDialogInputType.Select))"")
            @(@""
            {
"")
                <FormItem Label=""@@Localizer[nameof(SharedLocalResource.CreatedTime)]"" @(rules)>
                    <span>@@model.CreatedTime.ToString(ClientConstant.DateTimeFormat)</span>
                </FormItem>
            @(@""
            "")@(@""}
            "")
        }else if(fieldDto.Name.Equals(nameof(IModelUpdated.UpdatedTime)))
        {
            @(@""
            "")
           @(""@if (this.Options.Type.Equals(OperationDialogInputType.Edit) || this.Options.Type.Equals(OperationDialogInputType.Select))"")
            @(@""
            {
"")
                <FormItem Label=""@@Localizer[nameof(SharedLocalResource.UpdatedTime)]"" @(rules)>
                    <span>@@model.UpdatedTime?.ToString(ClientConstant.DateTimeFormat)</span>
                </FormItem>
           @(@""
            "")@(@""}
            "")
        }else if(fieldDto.Name.Equals(nameof(IModelId<int>.Id)))
        {
            @(@""
            @if (base.OperationIs(OperationDialogInputType.Edit | OperationDialogInputType.Select))"")
            @(@""
            {
                "")
                <FormItem @(rules)>
                <Input @@bind-Value=""model.@(fieldDto.Name)"" Disabled=""@disabledStr"" />
                </FormItem>
            @(@""
            }
            "")
            
        }else if(
        fieldDto.TypeName.Equals(nameof(Boolean)) ||
        fieldDto.TypeName.Equals(nameof(Int16)) ||
        fieldDto.TypeName.Equals(nameof(Int32)) ||
        fieldDto.TypeName.Equals(nameof(Int64)) ||
        fieldDto.TypeName.Equals(nameof(Single)) ||
        fieldDto.TypeName.Equals(nameof(Double)) ||
        fieldDto.TypeName.Equals(nameof(Decimal)))
        {
            <FormItem @(rules)>
                <InputNumber @@bind-Value=""model.@(fieldDto.Name)"" Disabled=""@disabledStr"" />
            </FormItem>
        }else if(fieldDto.TypeName.Equals(nameof(String)) && fieldDto.MaxLength!=null && fieldDto.MaxLength>200)
        {
            <FormItem @(rules)>
                <TextArea AutoSize=""true"" MinRows=""2"" MaxRows=""6"" @@bind-Value=""@@model.@(fieldDto.Name)""  Disabled=""@disabledStr"" ></TextArea>
            </FormItem>
        }else
        {
            <FormItem @(rules)>
                <Input @@bind-Value=""model.@(fieldDto.Name)"" Disabled=""@disabledStr"" />
            </FormItem>
        }
    }
    <FormItem WrapperColOffset=""8"" WrapperColSpan=""16"">
        <Space>
            @@if (!OperationDialogInputType.Select.Equals(this.Options.Type))
            {
                <SpaceItem>
                    <Button Type=""@@ButtonType.Primary"" OnClick=""OnGoToSubmit"">
                        @@Localizer[nameof(SharedLocalResource.Save)]
                    </Button>
                </SpaceItem>
            }
            <SpaceItem>
                <Button OnClick=""_=>OnFormCancel()"">
                    @@Localizer[nameof(SharedLocalResource.Cancel)]
                </Button>
            </SpaceItem>
        </Space>
    </FormItem>
</Form>"
            });
            //Edit编辑页.razor.cs
            data.Add(new GenerateTemplate() 
            { 
                Id=5,
                TemplateName= "Edit编辑页.razor.cs",
                TemplateContent = @"namespace @(Model.EntityConfig.ClientNameSpace).Pages.@(Model.EntityTypeName)View
{
    /// <summary>
    /// @(Model.DisplayName)编辑页
    /// </summary>
    public partial class @(Model.EntityTypeName)Edit : EditOperationDialogBase<@Model.EntityDtoName, @Model.PrimaryKeyTypeName@(Model.DisplayNameResourceTypeName!=null?"",""+Model.DisplayNameResourceTypeName:"""")>
    {
        /// <summary>
        /// 页面初始化后
        /// </summary>
         protected override void OnInitialized()
        {
             @foreach(var fieldDto in Model.Fields.Where(x=>x.FieldConfig.ShowInList).OrderBy(x=>x.FieldConfig.ListOrder))
            {
                if(fieldDto.FieldConfig.IsUnique)
                {
                @(@""
                "")
                @($""_uniqueVerificationTool.AddField(x => x.{fieldDto.Name});"")
                @(@""
                "")
                }
                
            }
            base.OnInitialized();
        }
    }
}"
            });
            //IApiService.cs
            data.Add(new GenerateTemplate() 
            { 
                Id=6,
                TemplateName= "IApiService.cs",
                TemplateContent = @"namespace @(Model.EntityConfig.BaseNameSpace).Services
{
    /// <summary>
    ///  @(Model.DisplayName)服务
    /// </summary>
    public interface I@(Model.EntityTypeName)Service : IServiceBase< @(Model.EntityDtoName), @Model.PrimaryKeyTypeName>
    {
    }
}"
            });
            //ApiService.cs
            data.Add(new GenerateTemplate() 
            { 
                Id=7,
                TemplateName= "ApiService.cs",
                TemplateContent = @"namespace @(Model.EntityConfig.ApiNameSpace).Services
{
    /// <summary>
    /// @(Model.DisplayName)服务
    /// </summary>
    [ApiDescriptionSettings(""@(Model.EntityConfig.ModuleName)"", Module = ""@(Model.EntityConfig.ModuleName.ToUnderscore().Dasherize())"")]
    public class @(Model.EntityTypeName)Service : ServiceBase<@(Model.EntityTypeName), @(Model.EntityDtoName), @Model.PrimaryKeyTypeName@(Model.EntityConfig.SupportMultiTenant?"", GardenerMultiTenantDbContextLocator"":"""")>, I@(Model.EntityTypeName)Service
    {

        /// <summary>
        /// @(Model.DisplayName)服务
        /// </summary>
        /// <param name=""repository""></param>
        public @(Model.EntityTypeName)Service(IRepository<@(Model.EntityTypeName)@(Model.EntityConfig.SupportMultiTenant?"", GardenerMultiTenantDbContextLocator"":"""")> repository) : base(repository)
        {
        }
    }
}
"
            });
            //ClientService.cs
            data.Add(new GenerateTemplate() 
            { 
                Id=8,
                TemplateName= "ClientService.cs",
                TemplateContent = @"namespace @(Model.EntityConfig.ClientNameSpace).Services
{
    /// <summary>
    ///  @(Model.DisplayName)服务
    /// </summary>
    [ScopedService]
    public class @(Model.EntityTypeName)Service : ClientServiceBase<@(Model.EntityDtoName), @Model.PrimaryKeyTypeName>, I@(Model.EntityTypeName)Service
    {
        /// <summary>
        ///  @(Model.DisplayName)服务
        /// </summary>
        public @(Model.EntityTypeName)Service(IApiCaller apiCaller) : base(apiCaller, ""@(Model.EntityTypeName.ToUnderscore().Dasherize())"", ""@(Model.EntityConfig.ModuleName.ToUnderscore().Dasherize())"")
        {
        }
    }

}
"
            });
            //_imports.razor
            data.Add(new GenerateTemplate() 
            { 
                Id=9,
                TemplateName= "client_imports.razor",
                TemplateContent = @"@@using Microsoft.AspNetCore.Components.Web
@@using Microsoft.AspNetCore.Authorization
@@using AntDesign
@@using AntDesign.ProLayout
@@using Gardener.Core.Authorization.Constants
@@using Gardener.Core.Client.OperationDialog
@@using Gardener.Core.Client.Constants
@@using Gardener.Core.Client.Components
@@using Gardener.Core.Client.Components.PageBaseClass
@@using Gardener.Core.Client.TableSearch
@@using Gardener.Core.Client.Authorization
@@using Gardener.Core.Client.Tag
@@using Gardener.Core.Resources
@@using @(Model.EntityConfig.BaseNameSpace+"".Dtos"")
@@using @(Model.EntityConfig.BaseNameSpace+"".Enums"")
@@using @(Model.EntityConfig.BaseNameSpace+"".Services"")
@if(Model.DisplayNameResourceType?.Namespace!=null)
{
@($""@using {Model.DisplayNameResourceType.Namespace}"")
}

@@attribute [Authorize]"
            });
            //GlobalUsing.cs
            data.Add(new GenerateTemplate() 
            { 
                Id=10,
                TemplateName= "client_GlobalUsing.cs",
                TemplateContent = @"global using AntDesign;
global using Gardener.Core.DependencyInjection;
global using Gardener.Core.Util;
global using Gardener.Core.Enums;
global using Gardener.Core.Resources;
global using Gardener.Core.Client.Services;
global using Gardener.Core.Client.OperationDialog;
global using Gardener.Core.Client.Extensions;
global using Gardener.Core.Client.Components.PageBaseClass;
global using @(Model.EntityConfig.BaseNameSpace+"".Dtos"");
global using @(Model.EntityConfig.BaseNameSpace+"".Enums"");
global using @(Model.EntityConfig.BaseNameSpace+"".Services"");
@if(Model.DisplayNameResourceType?.Namespace!=null)
{
@($""global using {Model.DisplayNameResourceType.Namespace};"");
}"});
            return data;
        }
    }
}
