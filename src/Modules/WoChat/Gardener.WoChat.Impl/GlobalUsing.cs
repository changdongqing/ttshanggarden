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
global using Gardener.Core.Dtos;
global using Gardener.Core.Enums;
global using Gardener.Core.EventBus;
global using Gardener.Core.Resources;
global using Gardener.Core.Common.Entities;
global using Gardener.Core.SystemAsset.Enums;
global using Gardener.WoChat.Services;
global using Gardener.WoChat.Dtos;
global using Gardener.WoChat.Resources;
global using Furion.DynamicApiController;
global using Gardener.Core.Authorization.Services;
global using Gardener.Core.Common.DbContextLocators;
global using Gardener.Core.NotificationSystem;
global using Gardener.Core.UserCenter.Dtos;
global using Gardener.Core.UserCenter.Services;
global using Gardener.Core.Util;
global using Gardener.WoChat.Dtos.Notification;
global using Gardener.WoChat.Enums;
global using Gardener.WoChat.Impl.Core;
