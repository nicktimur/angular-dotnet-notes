﻿namespace NotesBackend.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }

    public List<Note> Notes { get; set; } = new();
}
