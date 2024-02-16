﻿using CRM.API.Models.DAL;
using CRM.API.Models.EN;
using CRM.DTOs.CustomerDTOs;

namespace CRM.API.Endpoints
{
    public static class CustomerEndpoint
    {
        //metodo para configurar los endpoints relacionados con los clientes
        public static void AddCustomerEndpoints(this WebApplication app)
        {
            //cofigurar los endpoints realcionados con los clientes
            app.MapPost("/customer/search", async (SearchQueryCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crear un objeto customer a partir de los datos proporcionados
                var customer = new Customer
                {
                    Name = customerDTO.Name_Like != null ? customerDTO.Name_Like : string.Empty,
                    LastName = customerDTO.LastName_Like != null ? customerDTO.LastName_Like : string.Empty
                };

                //iniciar una lista de clientes y una variable para contar las filas
                var customers = new List<Customer>();
                int countRow = 0;

                //verifica si se debe enviar la cantidad de filas
                if (customerDTO.SendRowCount ==2)
                {
                    //realiza una busqueda de clientes y contrar las filas
                    customers = await customerDAL.Search(customer, skip: customerDTO.Skip, take: customerDTO.Take);
                    if (customers.Count > 0)
                        countRow = await customerDAL.CountSearch(customer);
                }
                else
                {
                    //realizar una busqueda de clietnes sin contar las filas
                    customers = await customerDAL.Search(customer, skip: customerDTO.Skip, take: customerDTO.Take);
                }

                //Crear un objeto searchresultcustomerdto para almacenar los resultados
                var customerResult = new SearchResultCustomerDTO
                {
                    Data = new List<SearchResultCustomerDTO.CustomerDTO>(),
                    CountRow = countRow
                };

                //mapea los resultados a objetos customerdto y agregalos al resultado
                customers.ForEach(s => {
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

            //configurar un endpoint de tipo get para obtener un cliente por id
            app.MapGet("/customer/{id}", async (int id, CustomerDAL customerDAL) =>
            {
                //obtener un cliente por id
                var customer = await customerDAL.GetById(id);

                //crear un objeto getidresultcustomerdto para almacenar el resultado
                var customerResult = new GetIdResultCustomerDTO
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    LastName = customer.LastName,
                    Address = customer.Address
                };

                //verificar si se encontro el cliente y devolver la respuesta correspondiente
                if (customerResult.Id > 0)
                    return Results.Ok(customerResult);
                else
                    return Results.NotFound(customerResult);
            });

            //configurar un endpoint de tipo post para crar un nuevo cliente
            app.MapPost("/customer", async (CreateCustomerDTO customerDTO, CustomerDAL customerDAL) =>
            {
                //crea un objeto customer a partir de los datos proporcionados
                var customer = new Customer
                {
                    Name = customerDTO.Name,
                    LastName= customerDTO.LastName,
                    Address= customerDTO.Address
                };

                //intentar crear el cliente y devolver el resultado correspondiente
                int result = await customerDAL.Create(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            //configurar un endpoint de tipo put para editar un cleinte existente
            app.MapPut("/customer", async(EditCustomerDTO customerDTO, CustomerDAL customerDAL)=>
            {
                //crear un objeto customer a partir de los datos proporcionados
                var customer = new Customer
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address
                };

                //intentar edtiar el cliente y devolver el resultado correspondiente
                int result = await customerDAL.Edit(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            // configurar un endpoint de tipo delete para eliminar un cliente por id
            app.MapDelete("/customer/{id}", async (int id, CustomerDAL customerDAL)=> 
            {
                //intentar eliminar el cliente y devolver el resultado correspondiente
                int result = await customerDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
        }
    }
}
