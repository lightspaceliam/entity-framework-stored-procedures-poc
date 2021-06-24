using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class PeopleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT  *
                    FROM    People
                )
                BEGIN
                    DELETE
                    FROM    People;

                    DBCC CHECKIDENT ('People', RESEED, 0);
                END
                GO

            INSERT INTO People (FirstName, LastName, Created, LastModified) VALUES
                ('Luke','Skywalker', GETUTCDATE(), GETUTCDATE()),
                ('Han','Solo', GETUTCDATE(), GETUTCDATE());
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT  *
                    FROM    People
                )
                BEGIN
                    DELETE
                    FROM    People;

                    DBCC CHECKIDENT ('People', RESEED, 0);
                END
                GO
            ");
        }
    }
}
