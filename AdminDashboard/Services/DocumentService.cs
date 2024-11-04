namespace AdminDashboard.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(IConfiguration configuration,ILogger<DocumentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<bool> DeleteFileAsync(string pictureUrl, string folderName)
        {
            using var _httpClient = new HttpClient();
            var response = await _httpClient.PostAsync($"{_configuration["BaseURL"]}api/Document/delete?PictureUrl={pictureUrl}&folderName={folderName}",null);

            response.EnsureSuccessStatusCode();

            var responseData=await response.Content.ReadAsStringAsync();

            return bool.TryParse(responseData, out var result) && result;


        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            try
            {
                using var _httpClient = new HttpClient();

                using var form = new MultipartFormDataContent();

                using var fileContent = new StreamContent(file.OpenReadStream());


                form.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PostAsync($"{_configuration["BaseURL"]}api/Document/upload?folderName={folderName}", form);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "";
                
            }
            
        }



    }
}
