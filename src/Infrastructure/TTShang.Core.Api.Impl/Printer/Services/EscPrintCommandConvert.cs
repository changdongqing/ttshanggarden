// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;
using TTShang.Core.Printer.Enums;
using TTShang.Core.Printer.Services;
using SixLabors.ImageSharp.Metadata.Profiles.Iptc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Core.Api.Impl.Printer.Services
{
    /// <summary>
    /// ESCPOS
    /// </summary>
    public class EscPrintCommandConvert : IPrintCommandConvert
    {
        private static byte[] Init = Convert.FromHexString("1B40");
        private static byte[] PrintGo = Convert.FromHexString("1B64");
        private static byte[] Bold = Convert.FromHexString("1B45");
        private static byte[] Align = Convert.FromHexString("1B61");
        private static byte[] FontDouble = Convert.FromHexString("1D21");
        private static byte[] ReverseDisplay = Convert.FromHexString("1D42");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public byte[] ConvertToByte(IReadOnlyList<PrintCommand> commands)
        {
            List<byte> bytes = new List<byte>();

            foreach (var item in commands)
            {
                IEnumerable<byte>? bs = null;
                switch (item.CommandType)
                {
                    case PrintCommandType.Init:
                        bs = Init;
                        break;
                    case PrintCommandType.PrintGo:
                        bs = Enumerable.Concat(PrintGo, [((PrintGoCommand)item).Line]);
                        break;
                    case PrintCommandType.Bold:
                        bs = Enumerable.Concat(Bold, [Convert.ToByte(((PrintBoldCommand)item).Bold ? 1 : 0)]);
                        break;
                    case PrintCommandType.Text:
                        if (item is PrintTextCommand text)
                        {
                            bs = Encoding.GetEncoding(text.CharacterSet).GetBytes(((PrintTextCommand)item).Text);
                        }
                        break;
                    case PrintCommandType.Align:
                        if (item is PrintAlignCommand alian)
                        {
                            int n = 0;
                            switch (alian.AlianType)
                            {
                                case PrintAlianType.Left: n = 0; break;
                                case PrintAlianType.Center: n = 1; break;
                                case PrintAlianType.Right: n = 2; break;
                            }
                            bs = Enumerable.Concat(Align, [Convert.ToByte(n)]);
                        }
                        break;
                    case PrintCommandType.FontDouble:
                        if (item is PrintFontDoubleCommand printFont)
                        {
                            int n = printFont.Width * 16 + printFont.Height;
                            bs = Enumerable.Concat(FontDouble, [Convert.ToByte(n)]);
                        }
                        break;
                    case PrintCommandType.ReverseDisplay:
                        if (item is PrintReverseDisplayCommand reverseDisplay)
                        {
                            bs = Enumerable.Concat(ReverseDisplay, [Convert.ToByte(reverseDisplay.Enable ? 1 : 0)]);
                        }
                        break;
                    case PrintCommandType.QrCode:
                        if (item is PrintQrCodeCommand qrCodeCommand)
                        {
                            List<byte> tempBs = new List<byte>();
                            //tempBs.AddRange([0x1B, 0x23, 0x23, 0x51, 0x50, 0x49, 0x58]);
                            //tempBs.Add(qrCodeCommand.QrPixelSize);
                            tempBs.AddRange([0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43]);
                            tempBs.Add(qrCodeCommand.QrSize);
                            tempBs.AddRange([0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45]);
                            tempBs.Add(qrCodeCommand.ErrorCorrectionLevel);
                            byte[] content = Encoding.GetEncoding(qrCodeCommand.CharacterSet).GetBytes(qrCodeCommand.Content);
                            byte h = (byte)(content.Length + 3 >> 8 & 0xFF);
                            byte l = (byte)(content.Length + 3 & 0xFF);
                            tempBs.AddRange([0x1D, 0x28, 0x6B]);
                            tempBs.Add(l);
                            tempBs.Add(h);
                            tempBs.AddRange([0x31, 0x50, 0x30]);
                            tempBs.AddRange(content);
                            tempBs.AddRange([0x1D, 0x28, 0x6B]);
                            tempBs.Add(l);
                            tempBs.Add(h);
                            tempBs.AddRange([0x31, 0x51, 0x30]);
                            bs = tempBs;
                        }
                        break;
                }
                if (bs != null && bs.Any())
                {
                    bytes.AddRange(bs);
                }
            }
            return bytes.ToArray();
        }
    }
}
