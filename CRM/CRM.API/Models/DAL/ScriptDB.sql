--crear la base de datos

CREATE DATABASE CRMDB
GO

--utilizar la base de datos
USE CRMDB
GO

--crear la tabla cosntumers

CREATE TABLE Customers
(
id INT IDENTITY (1,1) PRIMARY KEY,
Name VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Adress VARCHAR(255)
)