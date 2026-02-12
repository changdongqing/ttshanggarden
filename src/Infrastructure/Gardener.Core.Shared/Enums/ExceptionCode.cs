// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Core.Enums
{
    /// <summary>
    /// 异常状态码
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 系统异常默认提示
        /// </summary>
        System_Default_Error_Message,
        /// <summary>
        /// 身份验证失败
        /// </summary>
        Unauthorized,
        /// <summary>
        /// 拒绝访问资源
        /// </summary>
        Forbidden,
        /// <summary>
        /// 对象验证失败
        /// </summary>
        ModelValidateFailed,
        /// <summary>
        /// 用户锁定
        /// </summary>
        User_Locked,
        /// <summary>
        /// 用户密码错误
        /// </summary>
        User_Name_Or_Password_Error,
        /// <summary>
        /// 密码错误
        /// </summary>
        Password_Error,
        /// <summary>
        /// 验证码验证失败
        /// </summary>
        Verify_Code_Verification_Failed,
        /// <summary>
        /// 用户名重复
        /// </summary>
        User_Name_Repeat,
        /// <summary>
        /// 资源键值重复
        /// </summary>
        Resource_Key_Repeat,
        /// <summary>
        /// 刷新token不存在或已过期
        /// </summary>
        Refreshtoken_No_Exist_Or_Expire,
        /// <summary>
        /// 条件组中的操作类型错误
        /// </summary>
        Filter_Group_Operate_Error,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        Field_In_Type_Not_Found,
        /// <summary>
        /// 指定的属性“{0}”在类型“{1}”中不存在
        /// </summary>
        Query_Value_Type_No_Find_Converter,
        /// <summary>
        /// 请求的地址无效
        /// </summary>
        Request_Url_Is_Invalid,
        /// <summary>
        /// 刷新token不能用于鉴权
        /// </summary>
        Refreshtoken_Cannot_Used_In_Authentication,
        /// <summary>
        /// TOKEN无效
        /// </summary>
        Token_Invalid,
        /// <summary>
        /// 客户端登录失败
        /// </summary>
        Client_Login_Fail,
        /// <summary>
        /// 客户端未找到
        /// </summary>
        Client_No_Find,
        /// <summary>
        /// 时间戳已过期
        /// </summary>
        Timespan_Is_Expired,
        /// <summary>
        /// 邮件服务器未找到
        /// </summary>
        Email_Server_No_Find,
        /// <summary>
        /// 接口方法需要备注
        /// </summary>
        Controller_Need_Comment,
        /// <summary>
        /// SugarRepository 初始化错误
        /// </summary>
        Sugar_Repository_Init_Fail,
        /// <summary>
        /// 表名已存在
        /// </summary>
        Table_Name_Exist,
        /// <summary>
        /// 代码生成模板编译错误
        /// </summary>
        Code_Gen_Template_Compile_Error,
        /// <summary>
        /// 查询出错，检索字段在数据库中可能为null
        /// </summary>
        Search_Error_DB_Field_Is_Null,
        /// <summary>
        /// 未找到数据
        /// </summary>
        Data_Not_Find,
        /// <summary>
        /// 没有权限修改这个数据
        /// </summary>
        No_Permission_Modify_The_Data,
        /// <summary>
        /// 数据键“{0}”唯一性冲突
        /// </summary>
        Data_Key_Uniqueness_Conflict,
        /// <summary>
        /// 任务调度不存在
        /// </summary>
        Task_Not_Exist,
        /// <summary>
        /// 已存在同名任务调度
        /// </summary>
        Task_Allready_Exist,
        /// <summary>
        /// 字段“{0}”禁止修改
        /// </summary>
        Field_Cannot_Be_Modified,
        /// <summary>
        /// 未找到调度
        /// </summary>
        Scheduler_Not_Find,
        /// <summary>
        /// 确认密码不一致
        /// </summary>
        Confirm_New_Password_Inconformity,
        /// <summary>
        /// 文件系统服务{0}配置未找到
        /// </summary>
        File_Store_Service_Config_Not_Find,
        /// <summary>
        /// 不支持文件系统服务类型{0}
        /// </summary>
        File_Store_Service_Type_Unsupported,
        /// <summary>
        /// 设备连接是连接中状态，不能删除
        /// </summary>
        Device_Connection_Is_Connecting_Cannot_Delete,
        /// <summary>
        /// 已达到最大用户数
        /// </summary>
        Maximum_Number_Of_Users_Has_Been_Reached,
        /// <summary>
        /// 已达到最大角色数
        /// </summary>
        Maximum_Number_Of_Roles_Has_Been_Reached
    }
}
