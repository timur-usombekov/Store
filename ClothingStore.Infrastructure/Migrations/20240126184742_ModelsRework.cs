using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModelsRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Clothes",
                newName: "TotalStock");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "ClothingVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

			migrationBuilder.Sql(@"
                CREATE TRIGGER UpdateTotalStock
                    ON ClothingVariants
                    AFTER INSERT, UPDATE, DELETE
                    AS
                    BEGIN
                        UPDATE Clothes
                        SET TotalStock = (
                            SELECT COALESCE(SUM(Stock),0)
                            FROM ClothingVariants
                            WHERE Clothes.Id = ClothingVariants.ClothingId
                        )
                        WHERE Clothes.Id IN (
                            SELECT DISTINCT ClothingId FROM INSERTED
                            UNION
                            SELECT DISTINCT ClothingId FROM DELETED
                        );
                    END;
                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "ClothingVariants");

            migrationBuilder.RenameColumn(
                name: "TotalStock",
                table: "Clothes",
                newName: "Stock");

			migrationBuilder.Sql(@"DROP TRIGGER UpdateTotalStock");
		}
    }
}
