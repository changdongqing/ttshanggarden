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
global using TTShang.Core.Dtos;
global using TTShang.Core.Enums;
global using TTShang.Core.Resources;
global using TTShang.Core.Common;
global using TTShang.Core.Common.Entities;
global using TTShang.Core.SystemAsset.Enums;
global using TTShang.EasyJob.Services;
global using TTShang.EasyJob.Dtos;
global using TTShang.EasyJob.Resources;
global using Furion.DynamicApiController;