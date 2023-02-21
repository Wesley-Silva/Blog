using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.App.Models;
using Blog.App.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.App.Controllers
{
    [Authorize]
    public class PostsController : BaseController
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public PostsController(IPostRepository postRepository,
                                  ICategoriaRepository categoriaRepository,
                                  IComentarioRepository comentarioRepository,
                                  IMapper mapper,
                                  IPostService postService,
                                  INotificador notificador,
                                  IHttpContextAccessor accessor) : base(notificador)
        {
            _postRepository = postRepository;
            _categoriaRepository = categoriaRepository;
            _comentarioRepository = comentarioRepository;
            _mapper = mapper;
            _postService = postService;
            _accessor = accessor;
        }

        [AllowAnonymous]        
        public async Task<IActionResult> Index()
        {
            //var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            //ViewBag.UsuarioLogado = _accessor.HttpContext.User.Identity.Name;             

            ViewBag.Top5 = ObterPostMaisComentados();

           return View(_mapper.Map<IEnumerable<PostViewModel>>(await _postRepository.ObterPosts()));
        }

        [AllowAnonymous]
        [Route("detalhes-do-post/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)        
        
        {
            var postViewModel = await ObterPost(id);
            if (postViewModel == null)
            {
                return NotFound();
            }
            return View(postViewModel);
        }          
        
        [Route("novo-post")]        
        public async Task<IActionResult> Create()
        {            
            var categoriaViewModel = await ListarCategorias(new PostViewModel());
            return View(categoriaViewModel);
        }

        [Route("novo-post")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)            
                return View(postViewModel);
            

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(postViewModel.ImagemUpload, imgPrefixo))
            {
                return View(postViewModel);
            }

            postViewModel.Imagem = imgPrefixo + postViewModel.ImagemUpload.FileName;
            postViewModel.Email = _accessor.HttpContext.User.Identity.Name;
            await _postService.Adicionar(_mapper.Map<Post>(postViewModel));

            if (!OperacaoValida())
            {
                return View(postViewModel);
            }
            return RedirectToAction("Index");
        }
            
        [HttpPost]
        public JsonResult RecuperarPost(Guid id)
        {
            var postViewModel = ObterPostPorId(id);
            return Json(new { Resultado = postViewModel });
        }

        private async Task<PostViewModel> ObterPostPorId(Guid id)
        {
            var post = _mapper.Map<PostViewModel>(await _postRepository.ObterPorId(id));
            return post;
        }

        [HttpGet]
        public async Task<IActionResult> PostarComentario(Guid id)
        {
            var comentario = await ObterPost(id);

            if (comentario == null)
            {
                return NotFound();
            }
            return PartialView("_PostarComentario", new PostViewModel { Comentario = comentario.Comentario });
        }

        //[HttpGet]
        //public IActionResult ListaPostMaisComentados()
        //{
        //    var posts = ObterPostMaisComentados();

        //    return PartialView("_ListaPostMaisComentados", posts);
        //}

        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await ObterPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        private async Task<PostViewModel> ObterPost(Guid id)
        {
            var post = _mapper.Map<PostViewModel>(await _postRepository.ObterPorId(id));
            post.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());                        
            post.Comentarios = _mapper.Map<IEnumerable<ComentarioViewModel>>(await _comentarioRepository.ObterComentarioPorId(id));
            ViewBag.Qtde = post.Comentarios.Count();
            return post;
        }

        private IEnumerable<PostViewModel> ObterPostMaisComentados()
        {            
            var post = _mapper.Map<IEnumerable<PostViewModel>>(_postRepository.ObterPostMaisComentados());
            
            return post;
        }

        private async Task<PostViewModel> ListarCategorias(PostViewModel post)
        {
            post.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return post;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }
            return true;
        }


        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}