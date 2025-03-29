using ProgettoSettimana7.Data;
using ProgettoSettimana7.DTOs.Ticket;
using ProgettoSettimana7.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimana7.Services
{
    public class TicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistService> _logger;

        public TicketService(ApplicationDbContext context, ILogger<ArtistService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> TrySaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateTicketAsync(Ticket newTicket)
        {
            try
            {
                _context.Tickets.Add(newTicket);

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Ticket>?> GetAllTicketsAsync()
        {
            try
            {
                var ticketsList = await _context
                    .Tickets.Include(e => e.Event)
                    .Include(u => u.ApplicationUser)
                    .ToListAsync();

                return ticketsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<Ticket>?> GetAllMyTicketsAsync(string userId)
        {
            try
            {
                var myTicketsList = await _context
                    .Tickets.Include(e => e.Event)
                    .Include(u => u.ApplicationUser)
                    .Where(t => t.ApplicationUser.Id == userId)
                    .ToListAsync();

                return myTicketsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                var result = await _context
                    .Tickets.Include(e => e.Event)
                    .Include(u => u.ApplicationUser)
                    .FirstOrDefaultAsync(t => t.Ticketid == ticketId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> EditTicketByIdAsync(int ticketId, EdiTicketRequestDto editTicket)
        {
            try
            {
                var result = await GetTicketByIdAsync(ticketId);

                if (result == null)
                {
                    return false;
                }

                result.PurchaseDate = editTicket.PurchaseDate;
                result.EventId = editTicket.EventId;

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteTicketByIdAsync(int ticketId)
        {
            try
            {
                var ticket = await GetTicketByIdAsync(ticketId);

                if (ticket == null)
                {
                    return false;
                }

                _context.Tickets.Remove(ticket);

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
