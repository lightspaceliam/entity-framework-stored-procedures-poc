IF OBJECT_ID('DeletePerson', 'P') IS NOT NULL
    BEGIN
        DROP PROC DeletePerson;
    END
    GO

    CREATE PROCEDURE [dbo].[DeletePerson]
        @id INT
    AS
        DELETE
        FROM    People
        WHERE   Id = @id;