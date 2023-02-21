using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.Models.Validations;
using System.Threading.Tasks;

namespace Blog.Business.Services
{
    public class ComentarioService : BaseService, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        public ComentarioService(IComentarioRepository comentarioRepository, 
                                INotificador notificador)
            :base(notificador)
        {
            _comentarioRepository = comentarioRepository;
        }
        public async Task Adicionar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario))
                return;
            await _comentarioRepository.Adicionar(comentario);
        }

        public void Dispose()
        {
            _comentarioRepository?.Dispose();
        }
    }
}
