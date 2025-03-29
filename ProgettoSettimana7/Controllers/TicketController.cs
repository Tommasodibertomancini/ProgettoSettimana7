using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimana7.DTOs.Ticket;
using ProgettoSettimana7.Models;
using ProgettoSettimana7.Services;
using ProgettoSettimana7.DTOs.Event;
using ProgettoSettimana7.DTOs.Account;

namespace ProgettoSettimana7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequestDto createTicket)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user.Value;

                var newTicket = new Ticket()
                {
                    PurchaseDate = createTicket.PurchaseDate,
                    EventId = createTicket.EventId,
                    UserId = userId,
                };

                var result = await _ticketService.CreateTicketAsync(newTicket);

                return result
                    ? Ok(new CreateTicketResponse() { Message = "Ticket successfully created!" })
                    : BadRequest(new CreateTicketResponse() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            try
            {
                var result = await _ticketService.GetAllTicketsAsync();

                if (result == null)
                {
                    return BadRequest(
                        new AllTickets()
                        {
                            Message = "Something went wrong!",
                            Tickets = null,
                        }
                    );
                }

                List<TicketDto> ticketsList = result
                    .Select(t => new TicketDto()
                    {
                        Ticketid = t.Ticketid,
                        EventId = t.EventId,
                        PurchaseDate = t.PurchaseDate,
                        UserId = t.UserId,
                        Event = new BaseEventDto()
                        {
                            Eventid = t.Event.Eventid,
                            Title = t.Event.Title,
                            Place = t.Event.Place,
                            Date = t.Event.Date,
                        },
                        ApplicationUser = new ApplicationUserDto()
                        {
                            Id = t.ApplicationUser.Id,
                            FirstName = t.ApplicationUser.FirstName,
                            LastName = t.ApplicationUser.LastName,
                            Email = t.ApplicationUser.Email,
                        },
                    })
                    .ToList();

                var count = ticketsList.Count;

                var message = count == 1 ? $"{count} ticket found!" : $"{count} tickets found!";

                return Ok(new AllTickets() { Message = message, Tickets = ticketsList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{ticketId:int}")]
        [Authorize]
        public async Task<IActionResult> GetTicket(int ticketId)
        {
            try
            {
                var result = await _ticketService.GetTicketByIdAsync(ticketId);

                if (result == null)
                {
                    return BadRequest(
                        new GetTicketResponse() { Message = "Something went wrong!", Ticket = null }
                    );
                }

                var ticketFound = new TicketDto()
                {
                    Ticketid = result.Ticketid,
                    PurchaseDate = result.PurchaseDate,
                    EventId = result.EventId,
                    UserId = result.UserId,
                    Event = new BaseEventDto()
                    {
                        Eventid = result.Event.Eventid,
                        Title = result.Event.Title,
                        Date = result.Event.Date,
                        Place = result.Event.Place,
                    },
                    ApplicationUser = new ApplicationUserDto()
                    {
                        Id = result.ApplicationUser.Id,
                        FirstName = result.ApplicationUser.FirstName,
                        LastName = result.ApplicationUser.LastName,
                        Email = result.ApplicationUser.Email,
                    },
                };

                return Ok(
                    new GetTicketResponse() { Message = "Ticket found!", Ticket = ticketFound }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetYourTickets")]
        [Authorize]
        public async Task<IActionResult> GetYourTickets()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user.Value;

                var ticketsList = await _ticketService.GetAllMyTicketsAsync(userId);

                if (ticketsList == null)
                {
                    return BadRequest(
                        new AllTickets()
                        {
                            Message = "Something went wrong!",
                            Tickets = null,
                        }
                    );
                }

                List<TicketDto> myTicketsList = ticketsList
                    .Select(t => new TicketDto()
                    {
                        Ticketid = t.Ticketid,
                        EventId = t.EventId,
                        PurchaseDate = t.DateBought,
                        UserId = t.UserId,
                        Event = new BaseEventDto()
                        {
                            Eventid = t.Event.Eventid,
                            Title = t.Event.Title,
                            Place = t.Event.Place,
                            Date = t.Event.Date,
                        },
                        ApplicationUser = new ApplicationUserDto()
                        {
                            Id = t.ApplicationUser.Id,
                            FirstName = t.ApplicationUser.FirstName,
                            LastName = t.ApplicationUser.LastName,
                            Email = t.ApplicationUser.Email,
                        },
                    })
                    .ToList();

                var count = myTicketsList.Count;

                var message = count == 1 ? $"{count} ticket found!" : $"{count} tickets found!";

                return Ok(
                    new AllTickets() { Message = message, Tickets = myTicketsList }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{ticketId:int}")]
        [Authorize]
        public async Task<IActionResult> EditTicket(
            int ticketId,
            [FromBody] EdiTicketRequestDto editTicket
        )
        {
            try
            {
                var result = await _ticketService.EditTicketByIdAsync(ticketId, editTicket);

                return result
                    ? Ok(new EditTicketResponse() { Message = "Ticket successfully modified!" })
                    : BadRequest(new EditTicketResponse() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{ticketId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int ticketId)
        {
            var result = await _ticketService.DeleteTicketByIdAsync(ticketId);

            return result
                ? Ok(new DeleteTicketResponse() { Message = "Ticket successfully deleted" })
                : BadRequest(new DeleteTicketResponse() { Message = "Something went wrong!" });
        }
    }
}
