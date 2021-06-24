IF OBJECT_ID('InsertPerson', 'P') IS NOT NULL
    BEGIN
        DROP PROC InsertPerson;
    END
    GO

    CREATE PROCEDURE [dbo].[InsertPerson]
        @firstName NVARCHAR(150)
        , @lastName NVARCHAR(150)
    AS
        INSERT INTO People (
            FirstName
            , LastName
            , Created
            , LastModified
        ) VALUES (
            @firstName
            , @lastName
            , GETUTCDATE()
            , GETUTCDATE()
        );

        SELECT  Id
                , CONCAT(FirstName, ' ', LastName) AS 'Name'
                , FirstName
                , LastName
                , Created
                , LastModified
        FROM    People
        WHERE   Id = SCOPE_IDENTITY();