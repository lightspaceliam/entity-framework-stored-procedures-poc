IF OBJECT_ID('UpdatePerson', 'P') IS NOT NULL
    BEGIN
        DROP PROC UpdatePerson;
    END
    GO

    CREATE PROCEDURE [dbo].[UpdatePerson]
        @id INT
        , @firstName NVARCHAR(150)
        , @lastName NVARCHAR(150)
    AS
        UPDATE  People
        SET     FirstName = @firstName
                , LastName = @lastName
                , LastModified = GETUTCDATE()
        WHERE   Id = @id

        SELECT  Id
                , CONCAT(FirstName, ' ', LastName) AS 'Name'
                , FirstName
                , LastName
                , Created
                , LastModified
        FROM    People
        WHERE   Id = @id;