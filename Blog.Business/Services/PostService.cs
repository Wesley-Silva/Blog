using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Blog.Business.Services
{
    public class PostService : BaseService, IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository,
                              INotificador notificador) : base(notificador)
        {
            _postRepository = postRepository;
        }

        public async Task Adicionar(Post produto)
        {
            produto.CriadoEm = DateTime.Now;
            produto.AtualizadoEm = DateTime.Now;
            if (!ExecutarValidacao(new PostValidation(), produto)) return;

            await _postRepository.Adicionar(produto);
        }

        public async Task Atualizar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            await _postRepository.Atualizar(post);
        }

        public async Task Remover(Guid id)
        {
            await _postRepository.Remover(id);
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }
    }
}
