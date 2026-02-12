// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Printer.Dtos;

namespace TTShang.Core.Api.Impl.Printer.Entities
{
    /// <summary>
    /// 打印模板
    /// </summary>
    public class PrintTemplate : PrintTemplateDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
        , IEntitySeedData<PrintTemplate, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
        , IEntityTypeBuilder<PrintTemplate, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<PrintTemplate> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.TemplateKey).IsUnique();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<PrintTemplate> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return [
                new PrintTemplate()
                {
                    Id = Guid.Parse("4468cfe3-5c16-4a5b-ac79-318f1b580b15"),
                    TemplateName = "测试打印",
                    TemplateKey = "test_template",
                    TemplateType = "test_template",
                    TemplateWidth = 58,
                    TemplateContent = @"
                  
                 
                  
                  @{  List<PrintCommand> commands = [
                        new PrintInitCommand(),
                        new PrintGoCommand(),
                        new PrintAlignCommand(PrintAlianType.Center),
                        new PrintTextCommand(""============================""),
                        new PrintGoCommand(),
                        new PrintReverseDisplayCommand(true),
                        new PrintTextCommand(""黑底白字""),
                        new PrintGoCommand(),
                        new PrintReverseDisplayCommand(false),
                        new PrintAlignCommand(PrintAlianType.Left),
                        new PrintFontDoubleCommand(1,1),
                        new PrintTextCommand(""放大字符测试""),
                        new PrintGoCommand(),
                        new PrintFontDoubleCommand(0,0),
                        new PrintAlignCommand(PrintAlianType.Center),
                        new PrintTextCommand(""居中测试""),
                        new PrintGoCommand(),
                        new PrintAlignCommand(PrintAlianType.Right),
                        new PrintTextCommand(""居右测试""),
                        new PrintGoCommand(),
                        new PrintAlignCommand(PrintAlianType.Left),
                        new PrintBoldCommand(true),
                        new PrintTextCommand(""加粗测试""),
                        new PrintGoCommand(),
                        new PrintBoldCommand(false),
                        new PrintTextCommand(""不加粗测试""),
                        new PrintBoldCommand(true),
                        new PrintGoCommand(),
                        new PrintTextCommand(""向前走纸1行""),
                        new PrintGoCommand(1),
                        new PrintBoldCommand(true),
                        new PrintTextCommand(""现在时间""),
                        new PrintTextCommand(Model.ToString(""yyyy-MM-dd HH:mm:ss"")),
                        new PrintGoCommand(),
                        new PrintQrCodeCommand(""https://www.baidu.com/""){QrSize=6},
                        new PrintGoCommand(1),
                        new PrintTextCommand(""结束""),
                        new PrintGoCommand()
                    ];
                    }
                    @(commands.ToJson())
                
                ",
                    EmpowerAllTenants =true,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                },
                new PrintTemplate()
                {
                    Id = Guid.Parse("ed490912-7112-4e54-a02d-36dfe9edceba"),
                    TemplateName = "称重记录58",
                    TemplateKey = "weighing-record-58-1",
                    TemplateType = "wbg_print_detail",
                    TemplateWidth = 58,
                    TemplateContent = @"@{ 
    WeighingRecordDto record=Model;
    List<WeighingRecordLogDto> weighingRecordLogs=record.WeighingRecordLogs;
     List<PrintCommand> commands = [
new PrintInitCommand(),
new PrintGoCommand(),
new PrintAlignCommand(PrintAlianType.Center),
new PrintBoldCommand(true),
new PrintFontDoubleCommand(2,2),
new PrintTextCommand(""称重记录""),
new PrintGoCommand(),
new PrintBoldCommand(false),
new PrintFontDoubleCommand(0,0),
new PrintTextCommand(""================================""),
new PrintGoCommand(),
new PrintAlignCommand(PrintAlianType.Left),
new PrintBoldCommand(true),
new PrintTextCommand(""车牌号： ""),
new PrintBoldCommand(false),
new PrintTextCommand(Model.PlateNumber),
new PrintGoCommand(),

];
if (!WeighingStatus.Unknown.Equals(record.WeighingStatus))
{
    commands.AddRange([
        new PrintAlignCommand(PrintAlianType.Left),
        new PrintBoldCommand(true),
        new PrintTextCommand(""当前状态：""),
        new PrintBoldCommand(false),
        new PrintTextCommand(EnumHelper.GetEnumDescriptionOrName(Model.WeighingStatus)),
        new PrintGoCommand()
    ]);
}
if (!string.IsNullOrEmpty(Model.Driver))
{
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(""司机：  ""),
    new PrintBoldCommand(false),
    new PrintTextCommand(Model.Driver),
    new PrintGoCommand()

]);
}
if (!string.IsNullOrEmpty(record.CommodityName))
{
    string commodityNames = string.Join("","", record.CommodityName.Split("","").Where(x=>!string.IsNullOrEmpty(x)).Distinct());
commands.AddRange([
        new PrintAlignCommand(PrintAlianType.Left),
        new PrintBoldCommand(true),
        new PrintTextCommand(""货物：   ""),
        new PrintBoldCommand(false),
        new PrintTextCommand(commodityNames),
        new PrintGoCommand()
    ]);
}
if (!string.IsNullOrEmpty(Model.CommodityCode))
{
    string commodityCodes = string.Join("","", record.CommodityCode.Split("","").Where(x => !string.IsNullOrEmpty(x)).Distinct());
commands.AddRange([
        new PrintAlignCommand(PrintAlianType.Left),
        new PrintBoldCommand(true),
        new PrintTextCommand(""货号：   ""),
        new PrintBoldCommand(false),
        new PrintTextCommand(commodityCodes),
        new PrintGoCommand()
    ]);
}

commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(""重量：   ""),
    new PrintBoldCommand(false),
    new PrintTextCommand(Model.Weight.ToString(""F0"")+""Kg""),
    new PrintGoCommand()
]);
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(""皮重：   ""),
    new PrintBoldCommand(false),
    new PrintTextCommand(Model.TareWeight.ToString(""F0"")+""Kg""),
    new PrintGoCommand()
]);
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(""货重：   ""),
    new PrintBoldCommand(false),
    new PrintTextCommand(Model.CommodityWeight.ToString(""F0"")+""Kg""),
    new PrintGoCommand()
]);
foreach(var log in weighingRecordLogs.OrderBy(x => x.CreatedTime))
{
string type = """";
string type1 = """";
string type2 = """";
double weight = log.Weight;
if (WeighingStatus.NoLoadGoods.Equals(log.WeighingStatus))
{
    type = ""空车称重时间"";
    type1 = ""皮重"";
}
else if (WeighingStatus.LoadGoods.Equals(log.WeighingStatus))
{
    type = ""载货称重时间"";
    type1 = ""总重"";
}
else if (WeighingStatus.UnloadedGoods.Equals(log.WeighingStatus))
{
    type = Model.WeighingNumber > 2 ? ""结束称重时间"" : ""卸货后称重时间"";
    type1 = ""总重"";
}
else if (WeighingStatus.LoadedGoods.Equals(log.WeighingStatus))
{
    type = Model.WeighingNumber > 2 ? ""结束称重时间"" : ""装货后称重时间"";
    type1 = ""总重"";
}
else if (WeighingStatus.LoadingGoods.Equals(log.WeighingStatus))
{
    type = ""装货称重时间"";
    type1 = ""货重"";
    weight = log.WeightChange;
    if (!string.IsNullOrEmpty(log.CommodityName) && !string.IsNullOrEmpty(log.CommodityCode))
    {
        type2=$""{log.CommodityName}({log.CommodityCode})"";
    }
}
else if (WeighingStatus.UnloadingGoods.Equals(log.WeighingStatus))
{
    type = ""卸货称重时间"";
    type1 = ""货重"";
    weight = log.WeightChange * -1;
    if (!string.IsNullOrEmpty(log.CommodityName) && !string.IsNullOrEmpty(log.CommodityCode))
    {
        type2 = $""{log.CommodityName}({log.CommodityCode})"";
    }
}
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(type+""：""),
    new PrintGoCommand(),
    new PrintBoldCommand(false),
    new PrintTextCommand(log.CreatedTime.ToLocalTime().ToString(""yyyy/MM/dd HH:mm:ss"")),
    new PrintGoCommand()
]);
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(type1+""：  ""),
    new PrintBoldCommand(false),
    new PrintTextCommand((!string.IsNullOrEmpty(type2)?type2+"" "":"""")+weight.ToString(""F0"")+""Kg""),
    new PrintGoCommand()
]);
}
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Left),
    new PrintBoldCommand(true),
    new PrintTextCommand(""操作员： ""),
    new PrintBoldCommand(false),
    new PrintTextCommand(record.OperatorName),
    new PrintGoCommand()
]);
commands.AddRange([
    new PrintAlignCommand(PrintAlianType.Center),
    new PrintTextCommand(""================================""),
    new PrintGoCommand(),
    new PrintAlignCommand(PrintAlianType.Center),
    new PrintTextCommand(DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss"")),
    new PrintGoCommand(2)
]);
}
@(commands.ToJson())
",
                    EmpowerAllTenants =true,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                }
                ];
        }
    }
}
