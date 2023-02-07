using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;

using static System.IO.File;

namespace API.Controllers
{
    public class CatchallController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public CatchallController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: /Catchall/
        [AllowAnonymous]
        public ActionResult Index(string name = "Index")
        {
            var file = $@"Views\{name}.cshtml";
            var viewExists = Exists(file);

            var script = $@"{_environment.WebRootPath}\Scripts\{name}.js";
            var scriptExists = Exists(script);

            // check if view name requested is not found
            if (!viewExists)
                return NotFound();

            var reg1 = new Regex(@"^_");
            var reg2 = new Regex($"{name}");

            var scriptList = Directory.GetFiles($@"{_environment.WebRootPath}\Scripts\", "*.js")
                .Select(Path.GetFileName)
                .Where(n => n.Contains('_'))
                .Where(n => reg1.IsMatch(n) || reg2.IsMatch(n))
                .ToList();

            if (scriptExists)
                scriptList.Add($"{name}.js");

            // otherwise just return the view
            var viewDto = new { Title = name, Scripts = scriptList };
            return View(file, viewDto);
        }
    }
}
