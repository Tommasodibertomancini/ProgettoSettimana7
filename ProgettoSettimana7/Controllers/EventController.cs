using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimana7.Data;
using ProgettoSettimana7.DTOs.Event;
using ProgettoSettimana7.DTOs.Artist;
using ProgettoSettimana7.Models;
using ProgettoSettimana7.Services;

namespace ProgettoSettimana7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventRequestDto createEvent)
        {
            try
            {
                var newEvent = new Event()
                {
                    Title = createEvent.Title,
                    Place = createEvent.Place,
                    Date = createEvent.Date,
                    ArtistId = createEvent.ArtistId,
                };

                var result = await _eventService.CreateEventAsync(newEvent);

                return result
                    ? Ok(new CreateEventResponse() { Message = "Event successfully created!" })
                    : BadRequest(new CreateEventResponse() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _eventService.GetAllEventsAsync();

            if (result == null)
            {
                return Ok(
                    new AllEvent() { Message = "We don't find any event!", Events = null }
                );
            }

            List<EventDto> eventsList = result
                .Select(e => new EventDto()
                {
                    Eventid = e.Eventid,
                    Title = e.Title,
                    Place = e.Place,
                    Date = e.Date,
                    ArtistId = e.ArtistId,
                    Artist = new ArtistBaseDto()
                    {
                        ArtistId = e.Artist.ArtistId,
                        FirstName = e.Artist.FirstName,
                        LastName = e.Artist.LastName,
                        Genre = e.Artist.Genre,
                        Biography = e.Artist.Biography,
                    },
                })
                .ToList();

            return Ok(
                new AllEvent() { Message = "Events found!", Events = eventsList }
            );
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEvent(string eventId)
        {
            try
            {
                var result = await _eventService.GetEventByIdAsync(eventId);

                if (result == null)
                {
                    return BadRequest(
                        new GetEventResponse() { Message = "Something went wrong!", Event = null }
                    );
                }

                var eventFound = new EventDto()
                {
                    Eventid = result.Eventid,
                    Title = result.Title,
                    Place = result.Place,
                    Date = result.Date,
                    ArtistId = result.Artist.ArtistId,
                    Artist = new ArtistBaseDto()
                    {
                        ArtistId = result.Artist.ArtistId,
                        FirstName = result.Artist.FirstName,
                        LastName = result.Artist.LastName,
                        Genre = result.Artist.Genre,
                        Biography = result.Artist.Biography,
                    },
                };

                return Ok(new GetEventResponse() { Message = "Event found!", Event = eventFound });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> EditEvent(
            string eventId,
            [FromBody] EditEventRequestDto editEvent
        )
        {
            try
            {
                var editResult = await _eventService.EditEventAsync(eventId, editEvent);

                return editResult
                    ? Ok(new EditEventResponse() { Message = "Event successfully modified!" })
                    : BadRequest(new EditEventResponse() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{eventId:int}")]
        public async Task<IActionResult> Delete(int eventId)
        {
            var result = await _eventService.DeleteEventByIdAsync(eventId);

            return result
                ? Ok(new DeleteEventResponse() { Message = "Event successfully deleted!" })
                : BadRequest(new DeleteEventResponse() { Message = "Something went wrong!" });
        }
    }
}
