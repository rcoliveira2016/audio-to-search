using AudioToSearch.Aplication.Consultas.Dtos.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToSearch.Aplication.Consultas.Dtos.Requests;

public sealed class ConsultarDadosRequest: IRequest<ConsultarDadosResponse>
{
    public required string Termo { get; set; }
}
