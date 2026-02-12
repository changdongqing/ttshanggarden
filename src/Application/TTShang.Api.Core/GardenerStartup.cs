// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using TTShang.Iot.Impl;
using TTShang.Core.Api.Impl.Audit;
using TTShang.Core.Api.Impl.Authorization;
using TTShang.Core.Api.Impl.VerifyCode;
using TTShang.Core.Api.Impl.FileStore;
using TTShang.Core.Api.Impl.EventBus;
using TTShang.Core.Api.Impl.Cache;
using TTShang.Core.Api.Impl.Localization;
using TTShang.Core.Api.Impl.NotificationSystem;
using TTShang.Core.Api.Impl.DistributedLock;
using TTShang.Core.Util.JsonConverters;
using TTShang.Core.Resources;
using TTShang.Core.Api.Impl.Module;
using TTShang.Core.Api.Impl.EntityFramwork;
using TTShang.Core.Api.Impl.Swagger;
using TTShang.Core.Api.Impl.Attachment;
using TTShang.Core.Api.Impl.Email;
using TTShang.Core.Api.Impl.Dict;
using TTShang.Core.Api.Impl.SystemAsset;
using TTShang.Core.Api.Impl.UserCenter;
using TTShang.Core.Api.Impl.SystemConfig;
using TTShang.Core.CodeGeneration.Impl;
using TTShang.ToolBox.Impl;
using TTShang.WoChat.Impl;
using TTShang.EasyJob.Impl;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using TTShang.Core.Api.Impl.Module.Internal;
using TTShang.Core.Api.Impl.Common;
using TTShang.Core.Impl.Localization;
using TTShang.Weighbridge.Impl;
using TTShang.Core.Api.Impl.LicensePlateRecognition;
using Furion.JsonSerialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TTShang.Core.Api.Impl.AppManager;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using TTShang.Core.Api.Impl.QRCoder;
using TTShang.Core.Api.Impl.Printer;
using Microsoft.Extensions.Logging;

namespace TTShang.Api.Core
{
    /// <summary>
    /// 启动类
    /// </summary>
    [AppStartup(600)]
    public sealed class GardenerAdminStartup : AppStartup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //模块管理
            services.AddModuleManager();
            //公共模块
            services.AddCommonModule();
            //Ef数据库
            services.AddEF();
            //开启审计
            services.AddAudit();
            //开启附件
            services.AddAttachment();
            //开启邮件
            services.AddEmail();
            //开启字典
            services.AddDict();
            //开启身份认证&授权
            services.AddAuthor();
            //开启验证码
            services.AddVerifyCode(true);
            //开启文件存储
            services.AddFileStore();
            //缓存服务
            services.AddCache();
            //注册swagger
            services.AddSwagger();
            //事件总线
            services.AddEventBusServices(builder =>
            {
                // 注册事件订阅者
            });
            //注入包装好的本地化内容处理器
            services.AddLocalization<SharedLocalResource>();
            //添加系统通知服务
            services.AddSystemNotify();
            //分部式锁
            services.AddDistributedLock();
            //Iot
            services.AddIot();
            //资产
            services.AddSystemAsset();
            //用户中心
            services.AddUserCenter();
            //代码生成
            services.AddCodeGeneration();
            //工具箱
            services.AddToolBox();
            //wochat
            services.AddWoChat();
            //任务调度
            services.AddEasyJob();
            //系统配置
            services.AddSystemConfig();
            //注册跨域
            services.AddCorsAccessor();
            //远程请求
            services.AddRemoteRequest();
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 5242880000;// 500 MB
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 5242880000; // 500 MB
            });
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = 5242880000; // 500 MB
            });
            //注册控制器和视图
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                //忽略循环引用
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                //中文编码
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                //时间转换器
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
            })
            //配置多语言
            .AddAppLocalization()
            //注册动态api
            .AddDynamicApiControllers()
            //注册数据验证
            .AddDataValidation()
            //注册友好异常
            .AddFriendlyException<CustomErrorCodeTypeProvider>()
            //注册规范返回格式
            .AddUnifyResult<MyRESTfulResultProvider>()
            ;
            //TODO:furion并没有注入type=SystemTextJsonSerializerProvider，如果注入了移除
            services.TryAddSingleton<SystemTextJsonSerializerProvider>();
            //视图引擎
            services.AddViewEngine();

            //请求日志
            services.AddHttpLogging(_ => { });
            //文件日志
            Array.ForEach(new[] { LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error }, logLevel =>
            {
                services.AddFileLogging(options =>
                {
                    options.FileNameRule = fileName => string.Format(fileName, DateTime.UtcNow, logLevel.ToString()); // 如果是本地时间使用 DateTime.Now
                    options.WriteFilter = logMsg => logMsg.LogLevel == logLevel;
                });
            });
            //默认读取 Logging:Monitor 下配置
            //services.AddMonitorLogging();

            // 配置Nginx转发获取客户端真实IP
            // 1：如果负载均衡不是在本机通过 Loopback 地址转发请求的，
            // 一定要加上options.KnownNetworks.Clear()和options.KnownProxies.Clear()
            // 2：如果设置环境变量 ASPNETCORE_FORWARDEDHEADERS_ENABLED 为 True，
            // 则不需要下面的配置代码
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            // 限流服务
            // 只提供最基础限流服务，更多配置请查看
            // https://github.com/stefanprodan/AspNetCoreRateLimit/wiki/IpRateLimitMiddleware#defining-rate-limit-rules
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // 任务队列
            services.AddTaskQueue();
            //响应压缩
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["image/svg+xml"]);
            });
            //智能地磅
            services.AddWeighbridge();
            //LPR
            services.AddLPR();
            //二维码
            services.AddQRCoder();
            //appManager
            services.AddAppManager();
            //打印相关
            services.AddPrinter();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //启用EnableBuffering 解决Request body获取不到
            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });

            // 配置多语言，必须在 路由注册之前
            app.UseLocalization();
            //响应压缩
            app.UseResponseCompression();
            FileExtensionContentTypeProvider fileExtension = new FileExtensionContentTypeProvider();
            //允许浏览apk静态文件
            fileExtension.Mappings.Add(".apk", "application/vnd.android.package-archive");
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = fileExtension
            });

            app.UseHttpLogging();

            app.UseRouting();
            //跨域
            app.UseCorsAccessor();
            //限流组件(需注册在跨域之后)
            app.UseIpRateLimiting();
            //限流组件(需注册在跨域之后)
            app.UseClientRateLimiting();
            //身份认证
            app.UseAuthentication();
            //授权
            app.UseAuthorization();
            //注入基础中间件
            app.UseInjectBase();
            //swaager
            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                // 注册集线器
                endpoints.MapHubs();
                //endpoints.MapRazorPages();
                //endpoints.MapFallbackToFile("index.html");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
