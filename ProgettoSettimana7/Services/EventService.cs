using Microsoft.EntityFrameworkCore;
using ProgettoSettimana7.Data;
using ProgettoSettimana7.Models;
using ProgettoSettimana7.DTOs.Event;

namespace ProgettoSettimana7.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistService> _logger;

        public EventService(ApplicationDbContext context, ILogger<ArtistService> logger)
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

        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            try
            {
                var result = _context.Events.Add(newEvent);

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Event>?> GetAllEventsAsync()
        {
            try
            {
                var eventsList = await _context.Events.Include(e => e.Artist).ToListAsync();

                return eventsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Event?> GetEventByIdAsync(string eventId)
        {
            try
            {
                var result = await _context
                    .Events.Include(e => e.Artist)
                    .FirstOrDefaultAsync(e => e.Eventid.ToString() == eventId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> EditEventAsync(string eventId, EditEventRequestDto editEvent)
        {
            try
            {
                var eventFound = await GetEventByIdAsync(eventId);

                if (eventFound == null)
                {
                    return false;
                }

                eventFound.Title = editEvent.Title;
                eventFound.Place = editEvent.Place;
                eventFound.Date = editEvent.Date;
                eventFound.ArtistId = editEvent.ArtistId;

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEventByIdAsync(int eventId)
        {
            try
            {
                var result = await _context.Events.FindAsync(eventId);

                if (result == null)
                {
                    return false;
                }

                _context.Events.Remove(result);

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
