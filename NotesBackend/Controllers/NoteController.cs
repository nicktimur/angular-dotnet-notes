using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.Dtos;
using NotesBackend.Models;
using System.Security.Claims;

namespace NotesBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NoteController(ApplicationDbContext db) : ControllerBase
{
    private int GetUserId() =>
        int.Parse(User.FindFirstValue("uid")!);

    [HttpGet]
    public async Task<ActionResult<List<NoteDto>>> GetNotes()
    {
        var userId = GetUserId();
        var notes = await db.Notes
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();

        return Ok(notes);
    }

    [HttpPost]
    public async Task<ActionResult> CreateNote(NoteCreateDto dto)
    {
        var userId = GetUserId();

        var note = new Note
        {
            Title = dto.Title,
            Content = dto.Content,
            UserId = userId
        };

        db.Notes.Add(note);
        await db.SaveChangesAsync();

        return Ok(new { message = "Not başarıyla eklendi." });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNote(int id)
    {
        var userId = GetUserId();

        var note = await db.Notes
            .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

        if (note == null)
            return NotFound("Not bulunamadı.");

        db.Notes.Remove(note);
        await db.SaveChangesAsync();

        return Ok(new { message = "Not silindi." });
    }
}
