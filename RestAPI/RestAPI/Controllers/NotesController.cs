using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.DTOs;
using RestAPI.DTOs.Notes;
using RestAPI.Models;
using RestAPI.Services;
using System.Security.Claims;

namespace RestAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpPost("add-note")]
        public async Task<IActionResult> AddNote([FromBody] CreateNoteDto dto)
        {
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var result = await _notesService.AddNoteAsync(userId, dto.Content);
            return Ok(result);
        }
        [HttpPost("analyze-multiple")]
        public async Task<IActionResult> AnalyzeMultipleNotes([FromBody] AnalyzeMultipleNotesDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var results = new List<AnalyzeNoteResult>();

            foreach (var id in dto.Ids)
            {
                var result = await _notesService.AnalyzeNotesAsync(id, userId);
                results.Add(result);
            }

            return Ok(results);
        }

        [HttpGet("my-notes")]
        public async Task<IActionResult> GetAllUserNotes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Nieprawidłowy token");

            var result = await _notesService.GetAllUserNotesAsync(userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
