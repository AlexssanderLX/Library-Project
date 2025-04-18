using Biblioteca.Entities.Exceptions;

namespace Biblioteca.Services.Listas
{
    class UserLists
    {
        public List<Usuario> listU = new List<Usuario>();

        public void AddElementosU(Usuario usuario)//Adiciona elementos na lista
        {
            listU.Add(usuario);
        }
        public void RemoveElementosU(Usuario usuario)//Remove elementos da lista
        {
            listU.Remove(usuario);
        }
        
        public Usuario BuscarUsuarioPorNumeroIdentificacaopor(int numerodeidentificação)
        {
            if(numerodeidentificação == 0)
            {
                throw new ExcessõesPrograma("Usário inválido");
            }
            return listU.Find(u => u.NumeroIdentificacao == numerodeidentificação)
                ?? throw new ExcessõesPrograma("usuário não existe");
        }
    }
}
