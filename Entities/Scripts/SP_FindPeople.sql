IF OBJECT_ID('FindPeople', 'P') IS NOT NULL
    BEGIN
        DROP PROC FindPeople;
    END
    GO

    CREATE PROCEDURE [dbo].[FindPeople]
        @id INT NULL
    AS
        IF @id IS NULL
            SELECT  Id
                    , CONCAT(FirstName, ' ', LastName) AS 'Name'
                    , FirstName
                    , LastName
                    , Created
                    , LastModified
            FROM    People
            ORDER BY FirstName ASC
                    , LastName ASC;
        ELSE
            SELECT  Id
                    , CONCAT(FirstName, ' ', LastName) AS 'Name'
                    , FirstName
                    , LastName
                    , Created
                    , LastModified
            FROM    People
            WHERE   Id = @id;