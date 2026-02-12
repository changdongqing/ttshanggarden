namespace TTShang.Weighbridge.Dtos.Cmds
{
    /// <summary>
    /// 地磅命令输入
    /// </summary>
    public class WeighbridgeCmdInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="channelIds"></param>
        public WeighbridgeCmdInput(Guid configId, params int[] channelIds)
        {
            ConfigId = configId;
            ChannelIds = !channelIds.Any() ? [1] : channelIds;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid ConfigId { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public int[] ChannelIds { get; init; }

    }

    /// <summary>
    /// 地磅命令输入
    /// </summary>
    public class WeighbridgeCmdInput<TConfig> : WeighbridgeCmdInput
    {
        /// <summary>
        /// 配置数据
        /// </summary>
        public TConfig Config { get; set; }
        /// <summary>
        /// 地磅命令输入
        /// </summary>
        /// <param name="configId"></param>
        /// <param name="channelIds"></param>
        /// <param name="config"></param>
        public WeighbridgeCmdInput(Guid configId, TConfig config, params int[] channelIds) : base(configId, channelIds)
        {
            Config = config;
        }
    }
}
