// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Client.JsTool;
using Gardener.Core.EventBus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text;

namespace Gardener.Iot.Client.Pages.DeviceDataView
{
    public partial class DeviceDataDetail : OperationDialogBase<DeviceDto, bool, IotLocalResource>
    {
        private bool isHex = false;
        private string content = string.Empty;
        private string contentType = string.Empty;
        private ISubscriber? subscriber;
        [Inject]
        private IEventBus eventBus { get; set; } = null!;
        [Inject]
        private IClientMessageService messageService { get; set; } = null!;
        [Inject]
        private IDeviceService deviceService { get; set; } = null!;
        /// <summary>
        /// js操作
        /// </summary>
        [Inject]
        private IJsTool JsTool { get; set; } = null!;
        Timer? timer = null;
        protected override void OnInitialized()
        {
            timer = new Timer(async x => await ShowContent(), true, 0, 1000);

            string eventKey = nameof(DeviceDataDto.DeviceId) + this.Options.Id;
            //订阅该数据
            subscriber = eventBus.Subscribe<DeviceDataSaveAfterNotificationData>(Handle, eventKey);
            sendDataInput = new SendDataInput(this.Options.ClientId, string.Empty);
            base.OnInitialized();
        }
        private Queue<string> contentQueue = new Queue<string>(100);
        /// <summary>
        /// 处理实时数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task Handle(DeviceDataSaveAfterNotificationData data)
        {
            contentQueue.Enqueue($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {data.DeviceData.ContentType}");
            contentQueue.Enqueue($"{data.DeviceData.GetContentString(isHex) ?? "数据为空"}");
            return Task.CompletedTask;
        }
        StringBuilder contents = new StringBuilder();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        async Task ShowContent()
        {
            while (contentQueue.TryDequeue(out string? item))
            {
                contents.AppendLine(item);
            }
            content = contents.ToString();
            if(contents.Length>100000)
            {
                contents.Clear();
            }
            await base.RefreshPageDom();
            await JsTool.Document.ScrollBarToBottom("content_textarea");
        }

        protected override void Dispose(bool disposing)
        {
            if (subscriber != null)
            {
                eventBus.UnSubscribe(subscriber);
            }
            if (timer != null)
            {
                timer.Dispose();
            }
            base.Dispose(disposing);
        }

        private SendDataInput? sendDataInput;

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected async Task OnFormFinish(EditContext editContext)
        {
            await StartLoading();
            if (sendDataInput != null)
            {
                var result = await deviceService.SendMessage(sendDataInput);

                if (result)
                {
                    messageService.Success(Localizer.Combination(nameof(SharedLocalResource.Send), nameof(SharedLocalResource.Success)));
                }
                else
                {
                    messageService.Error(Localizer.Combination(nameof(SharedLocalResource.Send), nameof(SharedLocalResource.Fail)));
                }
            }
            await StopLoading();
        }
    }
}
