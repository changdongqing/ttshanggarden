// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
global using Mapster;
global using Furion.EventBus;
global using Furion.DatabaseAccessor;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using Gardener.Core.EventBus;
global using Gardener.Core.Dtos;
global using Gardener.Core.Enums;
global using Gardener.Core.Cache;
global using Gardener.Core.Common;
global using Gardener.Core.Common.DbContextLocators;
global using Gardener.Core.Common.Entities;
global using Gardener.Core.SystemAsset.Enums;
global using Gardener.Iot.Services;
global using Gardener.Iot.Dtos;
global using Gardener.Iot.Resources;
global using Gardener.Iot.Impl.Entities;
global using Gardener.Iot.Enums;
global using Gardener.Iot.Impl.Constants;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
