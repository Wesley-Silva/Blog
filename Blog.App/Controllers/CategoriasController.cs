using AutoMapper;
using Blog.App.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.App.Controllers
{
    [Authorize]
    public class CategoriasController : BaseController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository,
                                    ICategoriaService categoriaService,
                                    INotificador notificador,
                                    IMapper mapper)
            : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-categorias")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterCategorias()));
        }


        [Route("dados-da-categoria/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaPorId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }


        [Route("nova-categoria")]
        public IActionResult Create()
        {
            return View();
        }


        [Route("nova-categoria")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
                return View(categoriaViewModel);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _categoriaService.Adicionar(categoria);

            if (!OperacaoValida())
                return View(categoriaViewModel);

            return RedirectToAction("Index");
        }


        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaPorId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }


        [HttpPost]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(categoriaViewModel);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _categoriaService.Atualizar(categoria);
            return RedirectToAction(nameof(Index));
        }


        [Route("excluir-categoria/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaPorId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }


        [HttpPost, ActionName("Delete")]
        [Route("excluir-categoria/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaPorId(id);

            if (categoriaViewModel == null)
                return NotFound();

            await _categoriaService.Remover(id);

            if (!OperacaoValida()) return View(categoriaViewModel);

            // tempData porque estou fazendo um redirect - essa msg vai ser exibida na index
            TempData["Sucesso"] = "Categoria excluida com sucesso!";

            return RedirectToAction("Index");
        }


        private async Task<CategoriaViewModel> ObterCategoriaPorId(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterCategoriaPorId(id));
        }
    }
}