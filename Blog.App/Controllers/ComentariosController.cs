using AutoMapper;
using Blog.App.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.App.Controllers
{
    [Authorize]
    public class ComentariosController : BaseController
    {
        private readonly IComentarioService _comentarioService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ComentariosController(IComentarioService comentarioService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IHttpContextAccessor accessor
                                    ) : base(notificador)
        {
            _mapper = mapper;
            _comentarioService = comentarioService;
            _accessor = accessor;
        }
        

        [HttpPost]
        public JsonResult Create(ComentarioViewModel comentarioViewModel)
        {
            var resultado = "OK";
            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
            }

            comentarioViewModel.PostId = comentarioViewModel.Id;
            comentarioViewModel.Criado = DateTime.Now;

            comentarioViewModel.UsuarioId = Guid.Parse("754d1637-a003-4413-ac2d-d867f6bbed48");
            comentarioViewModel.Id = Guid.NewGuid();
            _comentarioService.Adicionar(_mapper.Map<Comentario>(comentarioViewModel));

            if (!OperacaoValida())
            {
                resultado = "OK";
            }
            return Json(new { Resultado = resultado });
        }
    }
}