using CRM.DTOs.CustomerDTOs;
namespace CRM.AppWebBlazor.Data
{
    public class CustomerServices
    {
        readonly HttpClient _httpClientCRMAPI;

        public CustomerServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }
        public async Task<SearchResultCustomerDTO> Search(SearchQueryCustomerDTO searchQueryCustomerDTO)
        {
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);
            if(response.IsSuccessStatusCode) 
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();
                return result ?? new SearchResultCustomerDTO();
            }
            return new SearchResultCustomerDTO();
        }
        public async Task<GetIdResultCustomerDTO> getById(int id)
        {
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();
                return result ?? new GetIdResultCustomerDTO();
            }
            return new GetIdResultCustomerDTO();
        }
        public async Task<int> Create(CreateCustomerDTO createCustomerDTO)
        {
            int result = 0;
            var response = await _
        }
    }
}
