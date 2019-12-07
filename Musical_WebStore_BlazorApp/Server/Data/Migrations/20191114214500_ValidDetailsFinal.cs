using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_WebStore_BlazorApp.Server.Data.Migrations
{
    public partial class ValidDetailsFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLimited",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLimited", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Description", "Image", "Price", "Quantity", "Title", "TypeName" },
                values: new object[,]
                {
                    { -11, "One of the most legendary amps for the fans of heavier tone.", "images/Amplifiers/1.jpg", 200, 4, "Peavey 6505", "Amplifier" },
                    { -8, "Very light distortion that caters to Rock, but brings high quality tone.", "images/Pedals/8.jpg", 600, 7, "Boss RC-3 Loop Station Pedal", "Pedal" },
                    { -7, "Another classic stompbox that brings impressive performance and abundant flexibility.", "images/Pedals/7.jpg", 300, 9, "Dunlop The Original Crybaby", "Pedal" },
                    { -6, "A faithful reproduction of the classic Tube Screamer sound.", "images/Pedals/6.jpg", 100, 8, "MXR Dyna Comp Effects Pedal", "Pedal" },
                    { -5, "More refined affordable dist box that brings great performance and limited versatility.", "images/Pedals/5.jpg", 1000, 7, "Boss CH-1 Stereo", "Pedal" },
                    { -4, "Unique features and great tones with an outstanding pedigree.", "images/Pedals/4.jpg", 300, 7, "Electro-Harmonix SOULFOOD", "Pedal" },
                    { -3, "Fully analog distortion that brings an impressive range of tones and gain.", "images/Pedals/3.jpg", 200, 1, "Ibanez TS808 Tube Screamer", "Pedal" },
                    { -2, "British tube distortion with outstanding dynamic range.", "images/Pedals/2.jpg", 200, 6, "Boss DD-7 Digital", "Pedal" },
                    { -1, "Focused pedal that excels at distortion, overdrive and classic fuzz sounds.", "images/Pedals/1.jpg", 100, 8, "MXR Phase 90", "Pedal" },
                    { -30, "FG800 shows what made Yamaha's FG series so legendary to begin with.", "images/Guitars/10.jpg", 700, 5, "Yamaha APX600", "Guitar" },
                    { -29, "A proud representative from the Takamine family.", "images/Guitars/9.jpg", 300, 8, "The Loar LH-204 Brownstone", "Guitar" },
                    { -28, "Affordable slimline Yamaha with upgraded performance.", "images/Guitars/8.jpg", 800, 6, "Seagull S6 Original", "Guitar" },
                    { -27, "A taste of vintage America with this stylish Chinese guitar.", "images/Guitars/7.jpg", 800, 2, "Yamaha A Series A3M", "Guitar" },
                    { -26, "Beautiful style, quality and playability with this Seagull.", "images/Guitars/6.jpg", 800, 7, "Martin 16 Series D-16GT", "Guitar" },
                    { -25, "A mid-range performance-focused acoustic with a high-end feel.", "images/Guitars/5.jpg", 500, 2, "Blueridge BR-160", "Guitar" },
                    { -24, "Classic style with this solid-wood Martin.", "images/Guitars/4.jpg", 700, 4, "Taylor 314ce", "Guitar" },
                    { -23, "Great looking dreadnaught body guitar from Bristol.", "images/Guitars/3.jpg", 800, 5, "Takamine EF360S-TT", "Guitar" },
                    { -22, "A gorgeous premium all-solid-wood Taylor.", "images/Guitars/2.jpg", 300, 7, "Fender CD60", "Guitar" },
                    { -21, "Vintage tone and style to spare with this high-end Takamine.", "images/Guitars/1.jpg", 300, 4, "Yamaha S1209", "Guitar" },
                    { -20, "A solid dual-channel choice for street performers.", "images/Amplifiers/10.jpg", 100, 4, "Roland Cube Street", "Amplifier" },
                    { -19, "Huge tone and power from this high-end acoustic amp.", "images/Amplifiers/9.jpg", 400, 4, "AER Compact 60", "Amplifier" },
                    { -18, "A classic Fender combo amp with great power.", "images/Amplifiers/8.jpg", 900, 6, "Fender Champion 100", "Amplifier" },
                    { -17, "Flexible combo providing legendary Boss tone at an attractive price.", "images/Amplifiers/7.jpg", 1000, 4, "Boss Katana KTN-100", "Amplifier" },
                    { -16, "A stand-out combo that crushes stage performances.", "images/Amplifiers/6.jpg", 900, 10, "Orange Crush Pro CR60C", "Amplifier" },
                    { -15, "Superb flexibility and power from this all-tube combo.", "images/Amplifiers/5.jpg", 400, 1, "Blackstar HT Stage 60 MKII", "Amplifier" },
                    { -14, "A modern reimagining of the iconic AC15.", "images/Amplifiers/4.jpg", 700, 10, "Vox Custom AC15C2", "Amplifier" },
                    { -13, "A legendary tone monster designed by Eddie Van Halen.", "images/Amplifiers/3.jpg", 600, 2, "EVH 5150III 50W", "Amplifier" },
                    { -12, "A high-end hand-wired tube amp with great style.", "images/Amplifiers/2.jpg", 300, 6, "Vox AC4HW1", "Amplifier" },
                    { -9, "Quintessential distortion pedal used both by legendary guitar players and the masses.", "images/Pedals/9.jpg", 900, 8, "	Electro-Harmonix LPB-1", "Pedal" },
                    { -10, "One of the best and only Klon Centaur clones on the market.", "images/Pedals/10.jpg", 100, 2, "Boss FV-500H", "Pedal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserLimited_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "UserLimited",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserLimited_UserId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "UserLimited");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

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
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Instruments",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
