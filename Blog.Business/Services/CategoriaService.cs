using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository,
                                INotificador notificador)
            :base(notificador)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria))
                return;

            if(_categoriaRepository.Buscar(c => c.Nome == categoria.Nome).Result.Any())
            {
                Notificar("Já existe uma categoria com este nome informado");
                return;
            }

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Guid id)
        {
            await _categoriaRepository.Remover(id);
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
        }
    }
}
