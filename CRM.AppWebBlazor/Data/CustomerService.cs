//importacion del espacio de nombres para los DTOs relacionados con los clientes
using CRM.DTOs.CustomerDTOs;

namespace CRM.AppWebBlazor.Data
{
    public class CustomerService
    {
        readonly HttpClient _httpClientCRMAPI;

        //Constructor que recibe una instancia de ihttpclientfactory para crear 
        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }

        //metodo para buscar clientes utilizando una solicitud http post
        public async Task<SearchResultCustomerDTO> Search(SearchQueryCustomerDTO searchQueryCustomerDTO)
        {
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);
                if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();
                return result ?? new SearchResultCustomerDTO();
            }
                return new SearchResultCustomerDTO();// devolver un objeto vacio en caso de error o respuesta no exitosa
        }

        //metodo para obtener un cliente por su ID utilizando una solicitud http get
        public async Task<GetIdResultCustomerDTO> GetById(int id)
        {
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();
                return result ?? new GetIdResultCustomerDTO();
            }
            return new GetIdResultCustomerDTO(); // devolver un objeto vacio en caso de rror o respuesta no exitosa
        }

        //metodo para crear un nuevo cliente utilizando una solicitud http post
        public async Task<int> Create(CreateCustomerDTO createCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer", createCustomerDTO);
            if (response.IsSuccessStatusCode) 
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }

        //metodo para editar un cliente existente utilizando una solicitud http put
        public async Task<int> Edit(EditCustomerDTO editCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer", editCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) ==false)
                    result = 0;
            }
            return result;
        }

        //metodo para eliminar un cliente por su id utilizando una solicitud http delete
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClientCRMAPI.DeleteAsync("/customer/" + id);
            if (response.IsSuccessStatusCode ) 
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if(int.TryParse(responseBody, out result)==false) 
                    result = 0;
            }
            return result;
        }
    }
}
