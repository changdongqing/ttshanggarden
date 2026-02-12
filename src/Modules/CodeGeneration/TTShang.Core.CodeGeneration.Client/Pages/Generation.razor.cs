// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using BlazorMonaco;
using BlazorMonaco.Editor;
using TTShang.Core.Client.Components;
using TTShang.Core.Module.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace TTShang.Core.CodeGeneration.Client.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Generation : OperationDialogBase<int, bool, CodeGenLocalResource>
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private ICodeGenerationService codeGenerationService { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IEntityConfigService entityConfigService { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IGenerateTemplateService templateService { get; set; } = null!;
        [Inject]
        private IClientMessageService clientMessageService { get; set; } = null!;
        /// <summary>
        /// 确认提示服务
        /// </summary>
        [Inject]
        private IConfirmService confirmService { get; set; } = null!;

        [Inject]
        private ISystemModuleService systemModuleService { get; set; } = null!;
        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        private IOperationDialogService operationDialogService { get; set; } = null!;
        [Inject]
        private IFieldConfigService fieldConfigService { get; set; } = null!;

        private int stepCurrent = 0;

        private IEnumerable<EntityDescriptionDto>? entities = null;

        private EntityDescriptionDto? selectEntityDescription = null;

        private EntityConfigDto? entityConfig = null;

        private Form<EntityConfigDto>? entityGenerationConfigForm = null;
        private IEnumerable<GenerateTemplateDto>? generateTemplates = null;
        private bool templateAdding = false;
        private string templateAddingName = string.Empty;
        private StandaloneCodeEditor? _templateContentEditor = null;
        private ClientListBindValue<int, bool> templateIsEdit = new ClientListBindValue<int, bool>(false);
        private bool templateHaveEdit = false;
        private string editorTabsActiveKey = "template";
        private StandaloneCodeEditor? _codeContentEditor = null;
        private bool saveEntityConfigBtnLoading = false;
        private bool toEntityConfigBtnLoading = false;

        private List<string> modules = new List<string>();

        private List<FieldConfigDto>? selectEntityFieldConfigs = null;
        IDictionary<string, (bool edit, FieldConfigDto data)> editSelectEntityFieldConfigCache = new Dictionary<string, (bool edit, FieldConfigDto data)>();
        private List<KeyValuePair<string, string>> sortOrders = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("asc","asc"),
            new KeyValuePair<string, string>("desc","desc")
        };
        protected override async Task OnInitializedAsync()
        {
            await LoadEntities();
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadEntities()
        {
            entities = (await codeGenerationService.GetEntityDescriptions()).OrderBy(x => x.EntityTypeName);
        }

        private async Task OnPreClick()
        {

            if (stepCurrent == 1)
            {
                //刷新
                await LoadEntities();
            }

            stepCurrent--;

        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private Task TemplateEditorOnDidInit()
        {
            _templateContentEditor?.AddCommand((int)KeyMod.CtrlCmd | (int)KeyCode.KeyS, async args =>
            {
                if (templateAdding)
                {
                    await OnClickSaveAddTemplate();
                }
                if (templateHaveEdit && editTemplate != null)
                {
                    await OnClickSaveEditTemplate(editTemplate);
                }
            });

            editorTabsActiveKey = "code";
            return Task.CompletedTask;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private Task CodeEditorOnDidInit()
        {
            editorTabsActiveKey = "template";
            return Task.CompletedTask;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private StandaloneEditorConstructionOptions TemplateEditorConstructionOptions(Editor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp",
                FormatOnPaste = true,
                FormatOnType = true,
                ReadOnly = true,
                ReadOnlyMessage = new MarkdownString()
                {
                    Value = Localizer[nameof(CodeGenLocalResource.TemplateContentIsReadOnlyPleaseClickEditTemplate)]
                }
            };
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private StandaloneEditorConstructionOptions CodeEditorConstructionOptions(Editor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp",
                FormatOnPaste = true,
                FormatOnType = true,
                ReadOnly = true,
                ReadOnlyMessage = new MarkdownString()
                {
                    Value = Localizer[nameof(CodeGenLocalResource.CodeContentIsReadOnly)]
                }
            };
        }
        /// <summary>
        /// 去往第二步
        /// </summary>
        /// <returns></returns>
        private async Task OnToEntityConfigClick()
        {
            if (selectEntityDescription != null)
            {
                toEntityConfigBtnLoading = true;
                var moduleInfos = await systemModuleService.GetAll();
                if (moduleInfos != null)
                {
                    modules = moduleInfos.Select(x => x.Name).ToList();
                }
                //加载实体配置
                editSelectEntityFieldConfigCache.Clear();
                entityConfig = selectEntityDescription.EntityConfig;
                selectEntityDescription.Fields.ForEach(x =>
                {
                    editSelectEntityFieldConfigCache[x.Name] = (false, x.FieldConfig);
                });
                selectEntityFieldConfigs = selectEntityDescription.Fields.Select(x => x.FieldConfig).ToList();
                toEntityConfigBtnLoading = false;
                stepCurrent++;
                await base.RefreshPageDom();
            }
        }
        /// <summary>
        /// 去第三步
        /// </summary>
        /// <returns></returns>
        private void OnSaveEntityConfigClick()
        {
            if (entityGenerationConfigForm != null)
            {
                entityGenerationConfigForm.Submit();
            }
        }
        private Collapse? entityConfigCollapse = null;
        private void OnSaveEntityConfigFinishFailed(EditContext editContext)
        {
            entityConfigCollapse?.Activate("1");
            //return base.RefreshPageDom();
        }
        /// <summary>
        /// 当保存实体生成配置时
        /// </summary>
        private async void OnEntityConfigFormSubmit()
        {
            if (entityConfig != null)
            {
                saveEntityConfigBtnLoading = true;
                var config = await entityConfigService.Find(entityConfig.Id);
                bool succeed = false;
                if (config != null)
                {
                    var result = await entityConfigService.Update(entityConfig);
                    if (result)
                    {
                        succeed = true;
                    }
                }
                else
                {
                    var result = await entityConfigService.Insert(entityConfig);
                    if (result != null)
                    {
                        succeed = true;
                    }
                }
                if (succeed)
                {
                    saveEntityConfigBtnLoading = false;
                    stepCurrent++;
                    editorTabsActiveKey = "template";
                    editorTabsActiveKey = "template";
                    if (_codeContentEditor != null)
                    {
                        await _codeContentEditor.SetValue("");
                    }
                    if (_templateContentEditor != null)
                    {
                        await _templateContentEditor.SetValue("");
                    }
                    await LoadTemplates();
                    await base.RefreshPageDom();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadTemplates()
        {
            generateTemplates = await templateService.GetAllUsable();
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnClickAddTemplate()
        {
            if (_templateContentEditor != null)
            {
                templateAdding = true;
                await _templateContentEditor.SetValue(string.Empty);
                if (_codeContentEditor != null)
                {
                    await _codeContentEditor.SetValue("");
                }
                editorTabsActiveKey = "template";
                await SwitchEditorReadOnlyState(_templateContentEditor);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnClickSaveAddTemplate()
        {
            if (_templateContentEditor == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(templateAddingName))
            {
                clientMessageService.Warn(Localizer[nameof(CodeGenLocalResource.PleaseEnterTheTemplateName)]);
                return;
            }
            string content = (await _templateContentEditor.GetValue()) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(content))
            {
                clientMessageService.Warn(Localizer[nameof(CodeGenLocalResource.PleaseEnterTheTemplateContent)]);
                return;
            }
            var result = await templateService.Insert(new GenerateTemplateDto()
            {
                TemplateName = templateAddingName,
                TemplateContent = content
            });
            if (result != null)
            {
                clientMessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Succeed)));

                templateAdding = false;
                templateAddingName = string.Empty;
                await _templateContentEditor.SetValue(string.Empty);
                await LoadTemplates();
                await base.RefreshPageDom();
            }
            else
            {
                clientMessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Failed)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnClickCancelAddTemplate()
        {
            templateAdding = false;
            templateAddingName = string.Empty;
            if (_templateContentEditor != null)
            {
                await _templateContentEditor.SetValue(string.Empty);
            }
        }
        private GenerateTemplateDto? editTemplate;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        private async Task OnClickEditTemplate(GenerateTemplateDto template)
        {
            if (_templateContentEditor == null)
            {
                return;
            }
            editTemplate = template;
            templateIsEdit[template.Id] = true;
            templateHaveEdit = true;
            await SwitchEditorReadOnlyState(_templateContentEditor);
            await _templateContentEditor.SetValue(template.TemplateContent);
            editorTabsActiveKey = "template";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        private async Task OnClickDeleteTemplate(int templateId)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                if (await templateService.Delete(templateId))
                {
                    if (_templateContentEditor != null)
                    {
                        await _templateContentEditor.SetValue(string.Empty);
                    }
                    await LoadTemplates();
                    await base.RefreshPageDom();
                }
                else
                {
                    clientMessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Delete), nameof(SharedLocalResource.Failed)));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private async Task OnClickCurrentTemplate(GenerateTemplateDto template)
        {
            if (_templateContentEditor == null)
            {
                return;
            }
            await SwitchEditorReadOnlyState(_templateContentEditor);
            await _templateContentEditor.SetValue(template.TemplateContent);
            editorTabsActiveKey = "template";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private async Task OnClickRunCurrentTemplate(GenerateTemplateDto template)
        {
            await OnClickCurrentTemplate(template);
            await OnGenerationClick();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private Task SwitchEditorReadOnlyState(StandaloneCodeEditor editor)
        {
            if (templateAdding || templateHaveEdit)
            {
                return CancelEditorReadOnly(editor);
            }
            else
            {
                return SetEditorReadOnly(editor);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private Task SetEditorReadOnly(StandaloneCodeEditor editor)
        {
            return editor.UpdateOptions(new EditorUpdateOptions()
            {
                ReadOnly = true
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private Task CancelEditorReadOnly(StandaloneCodeEditor editor)
        {
            return editor.UpdateOptions(new EditorUpdateOptions()
            {
                ReadOnly = false
            });
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnClickSaveEditTemplate(GenerateTemplateDto template)
        {
            if (_templateContentEditor == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(template.TemplateName))
            {
                clientMessageService.Warn(Localizer[nameof(CodeGenLocalResource.PleaseEnterTheTemplateName)]);
                return;
            }
            string content = (await _templateContentEditor.GetValue()) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(content))
            {
                clientMessageService.Warn(Localizer[nameof(CodeGenLocalResource.PleaseEnterTheTemplateContent)]);
                return;
            }
            template.TemplateContent = content;
            var result = await templateService.Update(template);
            if (result)
            {
                clientMessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Succeed)));
                templateIsEdit[template.Id] = false;
                templateHaveEdit = false;
                await LoadTemplates();
                await SwitchEditorReadOnlyState(_templateContentEditor);
                await base.RefreshPageDom();
            }
            else
            {
                clientMessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Failed)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task OnClickCancelEditTemplate(GenerateTemplateDto template)
        {
            templateIsEdit[template.Id] = false;
            templateHaveEdit = false;
            await LoadTemplates();
            if (_templateContentEditor != null)
            {
                await _templateContentEditor.SetValue(template.TemplateContent);
                await SwitchEditorReadOnlyState(_templateContentEditor);
            }
            await base.RefreshPageDom();
        }
        private bool generationCodeLoading = false;
        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        private async Task OnGenerationClick()
        {
            editorTabsActiveKey = "code";

            if (_templateContentEditor == null || selectEntityDescription == null)
            {
                return;
            }

            string template = await _templateContentEditor.GetValue();
            generationCodeLoading = true;
            string code = await codeGenerationService.GenerationCode(new GenerateCodeInput(template, selectEntityDescription.EntityTypeFullName));
            if (code != null && _codeContentEditor != null)
            {
                await _codeContentEditor.SetValue(code);
            }

            generationCodeLoading = false;
        }
        /// <summary>
        /// 开始编辑字段配置
        /// </summary>
        /// <param name="fieldName"></param>
        private void startEditSelectEntityFieldConfig(string fieldName)
        {
            var data = editSelectEntityFieldConfigCache[fieldName];
            editSelectEntityFieldConfigCache[fieldName] = (true, data.data.Adapt(new FieldConfigDto())); // add a copy in cache
        }
        /// <summary>
        /// 取消编辑字段配置
        /// </summary>
        /// <param name="fieldName"></param>
        private void cancelEditSelectEntityFieldConfig(string fieldName)
        {
            var data = selectEntityFieldConfigs?.FirstOrDefault(item => item.FieldName == fieldName);
            if (data != null)
            {
                editSelectEntityFieldConfigCache[fieldName] = (false, data); // recovery
            }
        }
        /// <summary>
        /// 保存字段配置
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private async Task saveEditSelectEntityFieldConfig(string fieldName)
        {
            if (selectEntityFieldConfigs == null)
            {
                return;
            }
            var index = selectEntityFieldConfigs.FindIndex(item => item.FieldName == fieldName);
            var newFieldConfig = editSelectEntityFieldConfigCache[fieldName].data;
            bool saved = false;
            //保存
            if (newFieldConfig.Id != 0)
            {
                saved = await fieldConfigService.Update(newFieldConfig);
            }
            else
            {
                var insertFieldConfig = await fieldConfigService.Insert(newFieldConfig);
                if (insertFieldConfig != null)
                {
                    insertFieldConfig.Adapt(newFieldConfig);
                    saved = true;
                }
            }
            if (saved)
            {
                selectEntityFieldConfigs[index] = newFieldConfig;
                editSelectEntityFieldConfigCache[fieldName] = (false, newFieldConfig.Adapt(new FieldConfigDto()));
                await RefreshPageDom();
            }
            else
            {
                clientMessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Fail)));
            }

        }
    }
}
