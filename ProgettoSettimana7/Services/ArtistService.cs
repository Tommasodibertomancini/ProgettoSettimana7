using Microsoft.EntityFrameworkCore;
using ProgettoSettimana7.Data;
using ProgettoSettimana7.DTOs.Artist;
using ProgettoSettimana7.Models;

namespace ProgettoSettimana7.Services
{
    public class ArtistService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistService> _logger;

        public ArtistService(ApplicationDbContext context, ILogger<ArtistService> logger)
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

        public async Task<bool> CreateArtistAsync(Artist artist)
        {
            try
            {
                _context.Artists.Add(artist);

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
        public async Task<List<Artist>?> GetAllArtistsAsync()
        {
            try
            {
                var artistsList = await _context.Artists.Include(a => a.Events).ToListAsync();

                return artistsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }


        public async Task<Artist?> GetArtistByIdAsync(string artistId)
        {
            try
            {
                var artist = await _context
                    .Artists.Include(a => a.Events)
                    .FirstOrDefaultAsync(a => a.ArtistId.ToString() == artistId);

                return artist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> EditArtistByIdAsync(
            string artistId,
            EditArtistRequestDto editArtist
        )
        {
            try
            {
                var artist = await GetArtistByIdAsync(artistId);

                if (artist == null)
                {
                    return false;
                }

                artist.FirstName = editArtist.FirstName;
                artist.LastName = editArtist.LastName;
                artist.Genre = editArtist.Genre;
                artist.Biography = editArtist.Biography;

                return await TrySaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteArtistByIdAsync(int artistId)
        {
            try
            {
                var artist = await GetArtistByIdAsync(artistId.ToString());

                if (artist == null)
                {
                    return false;
                }

                _context.Artists.Remove(artist);

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
