using Microsoft.AspNetCore.Mvc;
using C9info.Models;
using Microsoft.Extensions.Options;

namespace C9info.Controllers
{
    public class HomeController : Controller
    {
        readonly MailSettings _mailSettings = null;
        public HomeController(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Index(EmailModel emailModel)
        {
            if (ModelState.IsValid)
            {
                return new Email().SendEmail(emailModel, _mailSettings);
            }
            return false;
        }
    }
}
