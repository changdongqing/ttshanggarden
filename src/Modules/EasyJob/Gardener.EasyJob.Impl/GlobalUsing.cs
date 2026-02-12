// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
global using Mapster;
global using Furion.DatabaseAccessor;
global using Furion.FriendlyException;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;
global using Gardener.Core.Dtos;
global using Gardener.Core.Enums;
global using Gardener.Core.Resources;
global using Gardener.Core.Common;
global using Gardener.Core.Common.Entities;
global using Gardener.Core.SystemAsset.Enums;
global using Gardener.EasyJob.Services;
global using Gardener.EasyJob.Dtos;
global using Gardener.EasyJob.Resources;
global using Furion.DynamicApiController;