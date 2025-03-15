using Microsoft.EntityFrameworkCore;
using StudyService.Models;

namespace StudyService.Data
{
    public class ResponseRepository
    {
        private readonly StudyServiceDbContext _context;

        public ResponseRepository(StudyServiceDbContext context)
        {
            _context = context;
        }

        public List<Response> GetResponsesByAdId(int adId) => _context.Responses.Where(r => r.AdId == adId).ToList();

        public void AddResponse(Response response)
        {
            if (response.CreatedAt.Kind != DateTimeKind.Utc)
                response.CreatedAt = DateTime.SpecifyKind(response.CreatedAt, DateTimeKind.Utc);
            _context.Responses.Add(response);
            _context.SaveChanges();
        }

        public void DeleteResponsesByAdId(int adId)
        {
            var responses = _context.Responses.Where(r => r.AdId == adId);
            _context.Responses.RemoveRange(responses);
            _context.SaveChanges();
        }
    }
}