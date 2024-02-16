using CRM.API.Models.DAL;
using CRM.API.Models.EN;
using CRM.DTOs.CustomerDTOs;

namespace CRM.API.Endpoints
{
    public static class CustomerEndpoint
    {
        public static void AddCustomerEndpoint(this WebApplication app)
        {
            //configurar un endpoint de tipo post
            app.MapPost("/customer/search", async (SearchQueryCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto customer
                var customer = new Customer
                {
                    Name = customerDTO.Name_Like != null ? customerDTO.Name_Like : string.Empty,
                    LastName = customerDTO.LastName_Like != null ? customerDTO.LastName_Like : string.Empty,
                };

                //iniciar ina lista de clientes a partir de los datos
                var customers = new List<Customer>();
                int countRow = 0;
                //verificar una lista de clientes
                if (customerDTO.SendRowCount == 2)
                {

                    customers = await customerDAL.Search(customer, skip: customerDTO.skip, take: customerDTO.Take);
                    if (customerDTO.SendRowCount > 0)
                        countRow = await customerDAL.CountSearch(customer);
                }
                else
                {
                    //realizar una busqueda de clientes sin contar las filas
                    customers = await customerDAL.Search(customer, skip: customerDTO.skip, take: customerDTO.Take);
                }

                //crear un objeto searchtresultcustomerdto

                var customerResult = new SearchResultCustomerDTO
                {
                    Data = new List<SearchResultCustomerDTO.CustomerDTO>(),
                    CountRow = countRow,
                };
                //mapear los resultados
                customers.ForEach(s =>
                {
                    customerResult.Data.Add(new SearchResultCustomerDTO.CustomerDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LastName = s.LastName,
                        Address = s.Address
                    });
                });
                //devolver los resultados
                return customerResult;
            });

            //configurar un endpoint de tipo get para obtener un cliente por ID
            app.MapGet("/customer/{id}", async (int id, CustomerDAL customerDAL) =>
            {
                // obtener un cliente por ID
                var customer = await customerDAL.GetById(id);

                //crear un objeto getresultcustomer
                var customerResult = new GetIdResultCustomerDTO
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    LastName = customer.LastName,
                    Address = customer.Address
                };
                if (customerResult.Id > 0)
                    return Results.Ok(customerResult);

                else
                    return Results.NotFound(customerResult);
            });


            app.MapPost("/customer", async (CreateCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto customer
                var customer = new Customer
                {
                    Name = customerDTO.Name,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address
                };
                //intentar crear el cliente y devolver el resultado correspondiente 
                int result = await customerDAL.Create(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });



            //configurar un endpoint de tipo post para crear un nuevo cliente
            app.MapPut("/customer", async (EditCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto customer a partir de los datos proporcionados
                var customer = new Customer
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address
                };
                int result = await customerDAL.Edit(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
            //configurar un endpoint de tipo delete
            app.MapDelete("/customer/{id}", async (int id, CustomerDAL customerDAL) =>
            {
                //intentar eliminar al cliente y devolver el resultado
                int result = await customerDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });



        }

    }
}
