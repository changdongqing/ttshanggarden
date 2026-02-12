// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using TTShang.Core.Localization;
using TTShang.Core.Resources;
using TTShang.Core.SystemConfig.Dtos;
using TTShang.Core.SystemConfig.Services;
using Microsoft.AspNetCore.Components;
using static AntDesign.FormValidateErrorMessages;

namespace TTShang.Client.Entry
{
    public partial class App
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigValueService systemConfigValueService { get; set; } = null!;
        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        private SystemConfigDto? systemConfig { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            systemConfig = await systemConfigValueService.GetSystemConfig();
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 表单验证消息配置
        /// </summary>
        private FormConfig formValidateMessagesConfig = new FormConfig
        {
            ValidateMessages = new FormValidateErrorMessages
            {
                Required = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.RequiredValidationError)),
                Default = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.DefaultValidationError)),
                Enum = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),

                //OneOf = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.OneOfValidationError)),
                Whitespace = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.WhitespaceValidationError)),
                String = new CompareMessage
                {
                    Len = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.StringLenValidationError)),
                    Min = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.StringMinValidationError)),
                    Max = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.StringMaxValidationError)),
                    Range = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.StringRangeValidationError))
                },
                Number = new CompareMessage
                {
                    Len = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.NumberLenValidationError)),
                    Min = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.NumberMinValidationError)),
                    Max = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.NumberMaxValidationError)),
                    Range = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.NumberRangeValidationError))
                },
                Array = new CompareMessage
                {
                    Len = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.ArrayLenValidationError)),
                    Min = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.ArrayMinValidationError)),
                    Max = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.ArrayMaxValidationError)),
                    Range = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.ArrayRangeValidationError))
                },
                Pattern = new PatternMessage
                {
                    Mismatch = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.PatternMismatchValidationError))
                },
                Types = new TypesMessage
                {
                    String = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Array = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Object = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Number = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Date = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Boolean = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Integer = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Float = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Regexp = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Email = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError)),
                    Url = Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.TypesDefaultValidationError))
                }
            }
        };
        /// <summary>
        /// 
        /// </summary>
        private void GoHome()
        {
            Navigation.NavigateTo("/");
        }
    }
}
