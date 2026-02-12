using TTShang.Core.Client.JsTool;
using TTShang.Core.Client.JsTool.PrintingJs;
using Microsoft.JSInterop;

namespace TTShang.Core.Client.Impl.Services.JsTools
{
    /// <summary>
    /// 打印服务
    /// </summary>
    [ScopedService]
    public class PrintingService : JsModuleBase, IPrintingService
    {
        public PrintingService(IJSRuntime js) : base(js, "./_content/Gardener.Core.Client.Impl/PrintJs/print.min.js")
        {
        }

        public async Task Print(PrintOptions options)
        {
            await (await GetModule()).InvokeVoidAsync("print", new PrintOptionsAdapter(options));
        }

        public Task Print(string printable, PrintType printType = PrintType.Pdf)
        {
            return Print(new PrintOptions(printable) { Printable = printable, Type = printType });
        }
        public Task Print(string printable, bool showModal, PrintType printType = PrintType.Pdf)
        {
            return Print(new PrintOptions(printable) { ShowModal = showModal, Type = printType });
        }
    }
}
