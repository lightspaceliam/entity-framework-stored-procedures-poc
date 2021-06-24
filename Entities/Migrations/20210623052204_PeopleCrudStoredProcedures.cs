using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class PeopleCrudStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create.
            migrationBuilder.Sql(@"
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
            ");
            // Read.
            migrationBuilder.Sql(@"
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
            ");
            // Update.
            migrationBuilder.Sql(@"
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
            ");
            // Delete.
            migrationBuilder.Sql(@"
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
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID('InsertPerson', 'P') IS NOT NULL
                BEGIN
                    DROP PROC InsertPerson;
                END
                GO

                IF OBJECT_ID('FindPeople', 'P') IS NOT NULL
                BEGIN
                    DROP PROC FindPeople;
                END
                GO

                IF OBJECT_ID('UpdatePerson', 'P') IS NOT NULL
                BEGIN
                    DROP PROC UpdatePerson;
                END
                GO

                IF OBJECT_ID('DeletePerson', 'P') IS NOT NULL
                BEGIN
                    DROP PROC DeletePerson;
                END
                GO
            ");
        }
    }
}
