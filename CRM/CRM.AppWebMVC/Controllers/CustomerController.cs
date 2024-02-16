using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.DTOs.CustomerDTOs;

namespace CRM.AppWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClientCRMAPI;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }
        //metodo para mostrar la lista de clientes
        public async Task<IActionResult> Index(SearchQueryCustomerDTO searchQueryCustomerDTO, int CountRow = 0)
        {
            if (searchQueryCustomerDTO.SendRowCount == 0)
                searchQueryCustomerDTO.SendRowCount = 2;
            if (searchQueryCustomerDTO.Take == 0)
                searchQueryCustomerDTO.Take = 10;

            var result = new SearchResultCustomerDTO();

            //realizar una solicitud http post para buscar clientes en el servicio web
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();

            result = result != null ? result : new SearchResultCustomerDTO();

            if (result.CountRow == 0 && searchQueryCustomerDTO.SendRowCount == 1)
                result.CountRow = CountRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryCustomerDTO.SendRowCount = 0;
            ViewBag.searchQuery = searchQueryCustomerDTO;

            return View(result);
        }

        // metodo para mostrar los detalles de un cliente

        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultCustomerDTO();

            // realizar una solicitud http get para obtener los detalles del cliente por id
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(result ?? new GetIdResultCustomerDTO());
        }

        //metodo para mostrar el formulario de creacion de un cliente
        public ActionResult create()
        {
            return View();

        }

        // metodo para procesar la creacion de un cliente
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> create(CreateCustomerDTO createCustomerDTO)
        {
            try
            {
                // realizar una solicitud http post para crear un nuevo cliente
                var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/", createCustomerDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "error al intentar guardar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //metodo pra mostrar el formulario de edicion de un cliente
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(new EditCustomerDTO(result ?? new GetIdResultCustomerDTO()));
        }
        //metodo para procesar la edicion de un cliente
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, EditCustomerDTO editCustomerDTO)
        {
            try
            {
                //realizar una solictud http put para editar el cliente
                var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer/", editCustomerDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "error al intentar editar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //metodo para mostrar la pagina de confirmacion de eliminacion de un cliente
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(result ?? new GetIdResultCustomerDTO());
        }

        //metodo para procesar la eliminacion de un cliente

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id, GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            try
            {
                //realizar una solicitud http delete para eliminar el cliente por id
                var response = await _httpClientCRMAPI.DeleteAsync("/customer/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));

                }

                ViewBag.Error = "error al intentar borrar el registro";
                return View(getIdResultCustomerDTO);
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(getIdResultCustomerDTO);
            }

        }



    }
}