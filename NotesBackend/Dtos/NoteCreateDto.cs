﻿namespace NotesBackend.Dtos;

public class NoteCreateDto
{
    public string Title { get; set; } = "";
    public string? Content { get; set; }
}
