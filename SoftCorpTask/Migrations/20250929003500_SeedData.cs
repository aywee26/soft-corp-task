using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftCorpTask.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "b4bae8d0-b41c-44c4-beff-30e42bb6f1a6", "Work Group 1" },
                    { "5e0f13b2-061e-4465-8fa4-9050177fbf7d", "Work Group 2" },
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "PasswordSalt", "FullName", "UserRole", "WorkGroupId" },
                values: new object[,]
                {
                    { "ba67654d-2e9a-4fda-81d0-506b8655cd86", "admin", /* Password = "AdminPassword" */ "EGLzjFy5JqRuuuQ0DAMAqt94b9Jov2My4xGnK4KH9kR4XXNdkvhJxTYaVxWtaOpC2C18h6FfEZj7bUjbkM1kDw==", "QuIUous0l3lkpjYGDz6TEwuv49YnBH22qiMYoLAHGlv64m+5VsmJJbIgD/UAbelb1jaTQOkSqz22Eu5FQVZlqA==", "admin", 1, null },
                    { "4cea42a0-ed0d-48bb-9366-a5d180b4ef8e", "hr1", /* Password = "HR1Password" */ "EV9AH0geyZdV2L0EOIpKeotu9yvKWphApXVMuzeD5dZSSlx1NejkfGg7oBAxYoDsXTmWfSwiDyIdAtrPX5Gl3w==", "QlT3HgfMsC0SDIL3mkdErZRy5QIRIFAagKzfYOCLDDAflmdTaf4+u8zLMFxEL4eshtrypUB7oNmm5Qc9I+BL9A==", "Human Resources 1", 2, "b4bae8d0-b41c-44c4-beff-30e42bb6f1a6" },
                    { "9b78a56c-a3b8-4971-a7e8-7a3d97c5a7c1", "hr2", /* Password = "HR2Password" */ "OxeaFwXhZVkwNabV0EVF+LVvxSGNOXAyPTYnWFRd09CtVUZu4sTYSUMWU2gyR6p6NzDrMLSvRz2spAjSWbFldA==", "bbj5GSydqeuaF0Vw40IXfJJXw4uHb/aG4nyF/VdNtfmZmX3r7ZptoIrRJLn17MMCh+B9kz57QTv5N2ntdfaomg==", "Human Resources 2", 2, "5e0f13b2-061e-4465-8fa4-9050177fbf7d" },
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "UpdatedAt", "WorkType", "CandidateDataId", "WorkGroupId" },
                values: new object[,]
                {
                    { "6c1fb518-b3e7-47c6-9583-66ab9c754bf1", new DateTimeOffset(2025, 09, 20, 13, 40, 0, TimeSpan.Zero), 1, "af425fe2-97b7-4fed-b1f1-999b5ee5721c", "b4bae8d0-b41c-44c4-beff-30e42bb6f1a6" },
                    { "daed28e8-0f48-4293-9a1f-080c4c6b3861", new DateTimeOffset(2025, 09, 20, 13, 40, 0, TimeSpan.Zero), 1, "af425fe2-97b7-4fed-b1f1-999b5ee5721c", "5e0f13b2-061e-4465-8fa4-9050177fbf7d" }
                });
            
            migrationBuilder.InsertData(
                table: "CandidateDatas",
                columns: new[] { "Id", "FirstName", "LastName", "PatronymicName", "Email", "PhoneNumber", "Country", "BirthDate", "CandidateId" },
                values: new object[,]
                {
                    { "40118920-06e4-4433-874f-acd30284ad88", "TestFirstName", "TestLastName", "TestPatronymicName", "user@mail.com", "88005553535", "Russia", new DateTimeOffset(1990, 01, 20, 00, 00, 0, TimeSpan.Zero), "6c1fb518-b3e7-47c6-9583-66ab9c754bf1" },
                    { "62d6f1e6-ec93-47b2-bf3c-f8adbb235543", "AnotherTestFirstName", "AnotherTestLastName", "AnotherTestPatronymicName", "user2@mail.com", "+79000000000", "Russia", new DateTimeOffset(1995, 02, 28, 00, 00, 0, TimeSpan.Zero), "daed28e8-0f48-4293-9a1f-080c4c6b3861" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
