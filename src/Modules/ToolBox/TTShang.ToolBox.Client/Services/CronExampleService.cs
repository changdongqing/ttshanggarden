

namespace TTShang.ToolBox.Client.Services
{
    /// <summary>
    /// Cron示例服务
    /// </summary>
    [ScopedService]
    public class CronExampleService : ClientServiceCaller, ICronExampleService
    {
        public CronExampleService(IApiCaller apiCaller) : base(apiCaller, "cron-example", "tool-box")
        {
        }

        public Task<CronCheckResult> Check(CronCheckInput checkInput)
        {
            return apiCaller.PostAsync<CronCheckInput, CronCheckResult>($"{this.baseUrl}/check", checkInput);
        }

        public Task<IEnumerable<CronExample>> GetCronExamples()
        {
            return apiCaller.GetAsync<IEnumerable<CronExample>>($"{this.baseUrl}/cron-examples");
        }
    }
}
