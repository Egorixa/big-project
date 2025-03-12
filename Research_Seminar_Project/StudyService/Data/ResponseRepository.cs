namespace StudyService.Data
{
    public class ResponseRepository
    {
        private static List<Models.Response> _responses = new List<Models.Response>();

        public List<Models.Response> GetResponsesByAdId(int adId) => _responses.Where(r => r.AdId == adId).ToList();

        public void AddResponse(Models.Response response)
        {
            response.Id = _responses.Count + 1;
            response.CreatedAt = DateTime.Now; // Устанавливаем текущую дату
            _responses.Add(response);
        }
    }
}
