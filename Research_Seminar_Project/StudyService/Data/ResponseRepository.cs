using StudyService.Models;

namespace StudyService.Data
{
    public class ResponseRepository
    {
        private static List<Response> _responses = new List<Response>();

        public List<Response> GetResponsesByAdId(int adId) => _responses.Where(r => r.AdId == adId).ToList();
        public void AddResponse(Response response) => _responses.Add(response);

        public void DeleteResponsesByAdId(int adId)
        {
            _responses.RemoveAll(r => r.AdId == adId);
        }
    }
}