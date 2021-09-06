using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Services
{
  public class S_LimitesIdade : IS_LimitesIdade
  {
    private readonly IMapper _mapper;
    private readonly IR_LimitesIdade _limitesIdadeRepository;

    public S_LimitesIdade(IMapper mapper, IR_LimitesIdade limitesIdadeRepository)
    {
      _mapper = mapper;
      _limitesIdadeRepository = limitesIdadeRepository;
    }

    public async Task<IEnumerable<D_LimitesIdade>> GetByAffiliated(D_LimitesIdade dto)
    {
      var lista = await _limitesIdadeRepository.GetByAffiliated(dto.Conveniada);
      return _mapper.Map<IEnumerable<D_LimitesIdade>>(lista);
    }

    public async Task<D_LimitesIdade> GetByAge(D_LimitesIdade dto)
    {
      var entidade = await _limitesIdadeRepository.GetByAge(dto.Conveniada, dto.idade_cliente);
      return _mapper.Map<D_LimitesIdade>(entidade);
    }

    public async Task<D_LimitesIdade> GetMinMaxAgeByAffiliated(D_LimitesIdade dto)
    {
      var entidade = await _limitesIdadeRepository.GetMinMaxAgeByAffiliated(dto.Conveniada);
      return _mapper.Map<D_LimitesIdade>(entidade);
    }
  }
}
