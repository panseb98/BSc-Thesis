using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Migrations
{
    public partial class aaa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("USER");
            migrationBuilder.DropTable("USER_EXPERIENCE");
            migrationBuilder.DropTable("USER_SKILL"); 
                            migrationBuilder.DropTable("USER_PERSONAL_DATA");
            migrationBuilder.DropTable("USER_EDUCATION");




        }
    }
}
