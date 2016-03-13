CREATE TABLE [dbo].[CustomerInfo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Customer Name] NVARCHAR(MAX) NOT NULL, 
    [Address 1] NVARCHAR(MAX) NOT NULL, 
    [Address 2] NVARCHAR(MAX) NOT NULL, 
    [Address 3] NVARCHAR(MAX) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [Postal Code] NUMERIC NOT NULL, 
    [Phone Number 1] NUMERIC NOT NULL, 
    [Phone Number 2] NUMERIC NOT NULL, 
    [Fax Number] NUMERIC NOT NULL, 
    [Email Address] NVARCHAR(50) NOT NULL, 
    [Website] NVARCHAR(50) NOT NULL
)
