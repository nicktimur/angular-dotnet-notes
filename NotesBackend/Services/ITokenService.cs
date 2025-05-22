using NotesBackend.Models;

namespace NotesBackend.Services;

public interface ITokenService
{
    string CreateToken(User user);
}
