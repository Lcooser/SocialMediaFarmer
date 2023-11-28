using SocialMediaFarmer.Models;
using System.Collections.Generic;

namespace SocialMediaFarmer.Services
{
    public class UsuarioService
    {
        private readonly Dictionary<int, Usuario> _usuarios = new Dictionary<int, Usuario>();

        public void IncluirUsuario(Usuario usuario)
        {
            // Lógica para incluir um usuário no dicionário
            if (!_usuarios.ContainsKey(usuario.Id))
            {
                _usuarios.Add(usuario.Id, usuario);
            }
        }

        public List<Usuario> ObterListaUsuarios()
        {
            return new List<Usuario>(_usuarios.Values);
        }
    }
}
