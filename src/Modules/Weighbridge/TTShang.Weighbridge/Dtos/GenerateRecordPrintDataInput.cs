using TTShang.Core.Printer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Weighbridge.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GenerateRecordPrintDataInput
    {
        /// <summary>
        /// 打印协议
        /// </summary>
        public PrintProtocolType ProtocolType { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public string TemplateKey { get; set; } = null!;
        /// <summary>
        /// 记录
        /// </summary>
        public WeighingRecordDto WeighingRecord { get; set; }=null!;
    }
}
