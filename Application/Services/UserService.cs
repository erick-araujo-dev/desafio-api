﻿using SorteOnlineDesafio.Application.Commom;
using SorteOnlineDesafio.Application.Commom.Exceptions;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Application.Models;
using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Repository;

namespace SorteOnlineDesafio.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UserService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }   

        public UserModel CreateUser(string name, string email, string password) 
        {
            ValidateEmail(email);

            Usuario createEntitie = new Usuario
            {
                Nome = name,
                Email = email,
                Senha = password,
                DataCriacao = DateTime.Now,
            };

            var entitie = _usuarioRepository.AddAndReturnEntity(createEntitie);

            UserModel model = new UserModel
            {
                UserId = entitie.UsuarioId,
                Name = entitie.Nome,
                Email = entitie.Email
            };

            return model;
        }

        private void ValidateEmail(string email)
        {
            var emailValid = Util.ValidateEmail(email);

            if (!emailValid) 
            {
                throw new BusinessException("Formato de e-mail inválido.");
            }

            var emailAlreadyUsed = _usuarioRepository.Find(u => u.Email == email).FirstOrDefault();

            if (emailAlreadyUsed != null) 
            {
                throw new BusinessException("E-mail já cadastrado.");
            }
        }
    }
}