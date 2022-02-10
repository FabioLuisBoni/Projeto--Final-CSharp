using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDeslocamento.Domain.Entities;
using AppDeslocamento.Domain.Interfaces;
using MediatR;

namespace AppDeslocamento.Application.CarrosCommands
{
    public class CadastrarCarroCommand : IRequest<Carro>
    {
        public string Placa { get; set; }

        public string Descricao { get; set; }
    }
    public class CadastrarCarroCommandHandler :
        IRequestHandler<CadastrarCarroCommand, Carro>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarCarroCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Carro> Handle(
            CadastrarCarroCommand request,
            CancellationToken cancellationToken)
        {
            var carroInserir = new Carro(
                 request.Placa,
                 request.Descricao);

            var repositoryCarro =
                _unitOfWork.GetRepository<Carro>();

            repositoryCarro.Add(carroInserir);

            await _unitOfWork.CommitAsync();

            return carroInserir;
        }
    }
}