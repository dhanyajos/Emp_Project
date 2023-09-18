


USE [Employee]
GO

CREATE TABLE [dbo].[Employees](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](20) ,
	[LastName] [nvarchar](10) ,
	[PhoneNo] [nvarchar](20) ,
	[Email] [nvarchar](20) ,
	[Age] [int] ,
	[Position] [nvarchar](20) ,
	[Department] [nvarchar](20)  ,
    [Qualification] [NVARCHAR](20) ,
    [Gender] [nvarchar](5),
    [Salary]      [Int]          ,         
	Primary key (EmpId));

GO
SET IDENTITY_INSERT [dbo].[Employees] ON



INSERT [dbo].[Employees] ([EmpId], [FirstName], [LastName],[PhoneNo],[Email],[Age], [Position], [Department],[Qualification],[Gender],[Salary]) 
VALUES (1,'Sam','Raj','9207188764','samraj@gmail.com',26,'Software developer', 'IBU','MCA','Male',25000)
