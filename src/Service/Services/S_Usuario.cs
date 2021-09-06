using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrossCutting.Exceptions;
using Domain.Entities;
using Infrastructure.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Services
{
  public class S_Usuario : IS_Usuario
  {
    private readonly IMapper _mapper;
    private readonly IR_Usuario _usuarioRepositorio;

    // public UserService(IMapper mapper, IUserRepository userRepository)
    public S_Usuario(IMapper mapper, IR_Usuario usuarioRepositorio)
    {
      _mapper = mapper;
      _usuarioRepositorio = usuarioRepositorio;
    }
    public async Task<bool> Delete(int id)
    {
      return await _usuarioRepositorio.Delete(id);
    }
    public async Task<D_Usuario> Get(int id)
    {
      var entity = await _usuarioRepositorio.Get(id);
      return _mapper.Map<D_Usuario>(entity);
    }

    public async Task<IEnumerable<D_Usuario>> Get()
    {
      // var listEntity = await _userRepository.Get();
      var listEntity = await _usuarioRepositorio.Get();
      return _mapper.Map<IEnumerable<D_Usuario>>(listEntity);

    }

    public async Task<D_Usuario> GetByUsername(string username)
    {
      var entity = await _usuarioRepositorio.GetByUsername(username);
      return _mapper.Map<D_Usuario>(entity);
    }

    public async Task<D_Usuario> Insert(D_Usuario user)
    {
      // var userExists = await _userRepository.GetByUsername(user.Username);
      var userExists = await _usuarioRepositorio.GetByUsername(user.Usuario);
      if (userExists != null)
      {
        throw new DomainException("Já existe um usuário cadastrado para esse username.");
      }

      var userEntity = _mapper.Map<E_Usuario>(user);

      var userCreated = await _usuarioRepositorio.Insert(userEntity);

      return _mapper.Map<D_Usuario>(userCreated);
    }

    public async Task<int> Update(D_Usuario user)
    {
      var userExists = await _usuarioRepositorio.GetByUsername(user.Usuario);

      if (userExists == null)
      {
        throw new DomainException("Não existe nenhum usuário com o username informado!");
      }

      var userEntity = _mapper.Map<E_Usuario>(user);
      userEntity.Validate();

      var ret = await _usuarioRepositorio.Update(userEntity);
      return ret;
    }
  }
}
