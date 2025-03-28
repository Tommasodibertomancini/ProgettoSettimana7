using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimana7.DTOs.Artist;
using ProgettoSettimana7.DTOs.Event;
using ProgettoSettimana7.Models;
using ProgettoSettimana7.Services;

namespace ProgettoSettimana7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateArtist([FromBody] CreateArtistRequestDto newArtist)
        {
            try
            {
                var artist = new Artist()
                {
                    FirstName = newArtist.FirstName,
                    LastName = newArtist.LastName,
                    Genre = newArtist.Genre,
                    Biography = newArtist.Biography,
                };

                var result = await _artistService.CreateArtistAsync(artist);

                return result
                    ? Ok(new CreateArtistResponse() { Message = "Artist successfully created!" })
                    : BadRequest(new CreateArtistResponse() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtist()
        {
            try
            {
                var result = await _artistService.GetAllArtistsAsync();

                if (result == null)
                {
                    return Ok(
                        new AllArtist() { Message = "No artist found!", Artists = null }
                    );
                }

                List<ArtistDto> artistsList = result
                    .Select(r => new ArtistDto()
                    {
                        ArtistId = r.ArtistId,
                        FirstName = r.FirstName,
                        LastName = r.LastName,
                        Genre = r.Genre,
                        Biography = r.Biography,
                        Events = r
                            .Events?.Select(e => new BaseEventDto()
                            {
                                Eventid = e.Eventid,
                                Title = e.Title,
                                Date = e.Date,
                                Place = e.Place,
                            })
                            .ToList(),
                    })
                    .ToList();

                return Ok(
                    new AllArtist()
                    {
                        Message = "Artists found!",
                        Artists = artistsList,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{artistId}")]
        public async Task<IActionResult> GetArtist(string artistId)
        {
            try
            {
                var artist = await _artistService.GetArtistByIdAsync(artistId);

                if (artist == null)
                {
                    return BadRequest(
                        new GetArtistResponse() { Message = "Something went wrong!", Artist = null }
                    );
                }

                var artistFound = new ArtistDto()
                {
                    ArtistId = artist.ArtistId,
                    FirstName = artist.FirstName,
                    LastName = artist.LastName,
                    Genre = artist.Genre,
                    Biography = artist.Biography,
                    Events = artist
                        .Events?.Select(e => new BaseEventDto()
                        {
                            Eventid = e.Eventid,
                            Title = e.Title,
                            Date = e.Date,
                            Place = e.Place,
                        })
                        .ToList(),
                };

                return Ok(
                    new GetArtistResponse() { Message = "Artist found!", Artist = artistFound }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditArtist(
            [FromQuery] string artistId,
            [FromBody] EditArtistRequestDto editArtist
        )
        {
            try
            {
                var result = await _artistService.EditArtistByIdAsync(artistId, editArtist);

                return result
                    ? Ok(new EditArtistResponse() { Message = "Artist successfully modified!" })
                    : BadRequest(new EditArtistResponse() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _artistService.DeleteArtistByIdAsync(id);

            return result
                ? Ok(new DeleteArtistResponse() { Message = "Artist successfully deleted!" })
                : BadRequest(new DeleteArtistResponse() { Message = "Something went wrong!" });
        }
    }
}
