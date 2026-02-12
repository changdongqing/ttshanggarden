// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Api.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [IgnoreAudit]
        public IActionResult Index()
        {
            return View();
        }
    }
}
