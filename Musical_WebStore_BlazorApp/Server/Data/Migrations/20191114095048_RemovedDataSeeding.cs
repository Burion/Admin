using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_WebStore_BlazorApp.Server.Data.Migrations
{
    public partial class RemovedDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -30);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -29);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -28);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -27);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -26);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -25);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -24);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -23);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -22);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -21);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -40);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -39);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -38);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -37);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -36);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -35);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -34);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -33);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -32);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -31);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -20);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -19);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -18);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -17);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -16);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -14);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -13);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -12);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -11);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Description", "Image", "Price", "Quantity", "Title", "TypeName" },
                values: new object[,]
                {
                    { -21, "test desc1", "test.jpg", 1, 1, "amplifierTest1", "Amplifier" },
                    { -18, "test desc8", "test.jpg", 8, 8, "pedalTest8", "Pedal" },
                    { -17, "test desc7", "test.jpg", 7, 7, "pedalTest7", "Pedal" },
                    { -16, "test desc6", "test.jpg", 6, 6, "pedalTest6", "Pedal" },
                    { -15, "test desc5", "test.jpg", 5, 5, "pedalTest5", "Pedal" },
                    { -14, "test desc4", "test.jpg", 4, 4, "pedalTest4", "Pedal" },
                    { -13, "test desc3", "test.jpg", 3, 3, "pedalTest3", "Pedal" },
                    { -12, "test desc2", "test.jpg", 2, 2, "pedalTest2", "Pedal" },
                    { -11, "test desc1", "test.jpg", 1, 1, "pedalTest1", "Pedal" },
                    { -40, "test desc10", "test.jpg", 10, 10, "guitarTest10", "Guitar" },
                    { -39, "test desc9", "test.jpg", 9, 9, "guitarTest9", "Guitar" },
                    { -38, "test desc8", "test.jpg", 8, 8, "guitarTest8", "Guitar" },
                    { -37, "test desc7", "test.jpg", 7, 7, "guitarTest7", "Guitar" },
                    { -36, "test desc6", "test.jpg", 6, 6, "guitarTest6", "Guitar" },
                    { -35, "test desc5", "test.jpg", 5, 5, "guitarTest5", "Guitar" },
                    { -34, "test desc4", "test.jpg", 4, 4, "guitarTest4", "Guitar" },
                    { -33, "test desc3", "test.jpg", 3, 3, "guitarTest3", "Guitar" },
                    { -32, "test desc2", "test.jpg", 2, 2, "guitarTest2", "Guitar" },
                    { -31, "test desc1", "test.jpg", 1, 1, "guitarTest1", "Guitar" },
                    { -30, "test desc10", "test.jpg", 10, 10, "amplifierTest10", "Amplifier" },
                    { -29, "test desc9", "test.jpg", 9, 9, "amplifierTest9", "Amplifier" },
                    { -28, "test desc8", "test.jpg", 8, 8, "amplifierTest8", "Amplifier" },
                    { -27, "test desc7", "test.jpg", 7, 7, "amplifierTest7", "Amplifier" },
                    { -26, "test desc6", "test.jpg", 6, 6, "amplifierTest6", "Amplifier" },
                    { -25, "test desc5", "test.jpg", 5, 5, "amplifierTest5", "Amplifier" },
                    { -24, "test desc4", "test.jpg", 4, 4, "amplifierTest4", "Amplifier" },
                    { -23, "test desc3", "test.jpg", 3, 3, "amplifierTest3", "Amplifier" },
                    { -22, "test desc2", "test.jpg", 2, 2, "amplifierTest2", "Amplifier" },
                    { -19, "test desc9", "test.jpg", 9, 9, "pedalTest9", "Pedal" },
                    { -20, "test desc10", "test.jpg", 10, 10, "pedalTest10", "Pedal" }
                });
        }
    }
}
