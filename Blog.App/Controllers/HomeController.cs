using Blog.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //throw new Exception("Erro");
            return View();
        }


        [Route("Error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();
            if (id == 500) //erro servidor
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente mais tarde";
                modelErro.Titulo = "Ocorreu um erro";
                modelErro.ErroCode = id;
            }
            else if (id == 404)// pag nao existe
            {
                modelErro.Mensagem = "A página não existe!";
                modelErro.Titulo = "Ops! Pagina não encontrada";
                modelErro.ErroCode = id;
            }
            else if (id == 403)//acesso negado
            {
                modelErro.Mensagem = "Vc nao tem permissão p/ fazer isto!";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }
            return View("Error", modelErro);
        }
    }
}
