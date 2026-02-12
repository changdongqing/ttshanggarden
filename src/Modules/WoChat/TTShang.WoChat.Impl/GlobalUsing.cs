// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
global using Mapster;
global using Furion.DependencyInjection;
global using Furion.DatabaseAccessor;
global using Furion.FriendlyException;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;
global using TTShang.Core.Dtos;
global using TTShang.Core.Enums;
global using TTShang.Core.EventBus;
global using TTShang.Core.Resources;
global using TTShang.Core.Common.Entities;
global using TTShang.Core.SystemAsset.Enums;
global using TTShang.WoChat.Services;
global using TTShang.WoChat.Dtos;
global using TTShang.WoChat.Resources;
global using Furion.DynamicApiController;
global using TTShang.Core.Authorization.Services;
global using TTShang.Core.Common.DbContextLocators;
global using TTShang.Core.NotificationSystem;
global using TTShang.Core.UserCenter.Dtos;
global using TTShang.Core.UserCenter.Services;
global using TTShang.Core.Util;
global using TTShang.WoChat.Dtos.Notification;
global using TTShang.WoChat.Enums;
global using TTShang.WoChat.Impl.Core;
