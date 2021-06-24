/*
	Execute Stored Procedures
*/

--DELETE
--FROM    People;
--DBCC CHECKIDENT ('People', RESEED, 0);

SELECT	*
FROM	People;

/*  Create
---------------------*/
EXECUTE [dbo].[InsertPerson] 
   'Leia'
   , 'Organa'
GO

/*  Read
---------------------*/

--  Find all.
EXECUTE [dbo].[FindPeople] 
	NULL
GO

-- Find by Id.
EXECUTE [dbo].[FindPeople] 
	1

/*  Update
---------------------*/

EXECUTE [dbo].[UpdatePerson] 
	1
   , 'Luke'
   , 'Skywalker'
GO

/*  Delete
---------------------*/
EXECUTE [dbo].[DeletePerson] 
	1
GO