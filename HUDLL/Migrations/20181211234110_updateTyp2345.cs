using Microsoft.EntityFrameworkCore.Migrations;

namespace HUDLL.Migrations
{
    public partial class updateTyp2345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEventType_EventTypes_EventTypeId",
                table: "PostEventType");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEventType_Posts_PostId",
                table: "PostEventType");

            migrationBuilder.DropForeignKey(
                name: "FK_PostFigure_Figures_FigureId",
                table: "PostFigure");

            migrationBuilder.DropForeignKey(
                name: "FK_PostFigure_Posts_PostId",
                table: "PostFigure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostFigure",
                table: "PostFigure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostEventType",
                table: "PostEventType");

            migrationBuilder.RenameTable(
                name: "PostFigure",
                newName: "PostFigures");

            migrationBuilder.RenameTable(
                name: "PostEventType",
                newName: "PostEventTypes");

            migrationBuilder.RenameIndex(
                name: "IX_PostFigure_FigureId",
                table: "PostFigures",
                newName: "IX_PostFigures_FigureId");

            migrationBuilder.RenameIndex(
                name: "IX_PostEventType_EventTypeId",
                table: "PostEventTypes",
                newName: "IX_PostEventTypes_EventTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostFigures",
                table: "PostFigures",
                columns: new[] { "PostId", "FigureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostEventTypes",
                table: "PostEventTypes",
                columns: new[] { "PostId", "EventTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostEventTypes_EventTypes_EventTypeId",
                table: "PostEventTypes",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "EventTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEventTypes_Posts_PostId",
                table: "PostEventTypes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostFigures_Figures_FigureId",
                table: "PostFigures",
                column: "FigureId",
                principalTable: "Figures",
                principalColumn: "FigureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostFigures_Posts_PostId",
                table: "PostFigures",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEventTypes_EventTypes_EventTypeId",
                table: "PostEventTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEventTypes_Posts_PostId",
                table: "PostEventTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostFigures_Figures_FigureId",
                table: "PostFigures");

            migrationBuilder.DropForeignKey(
                name: "FK_PostFigures_Posts_PostId",
                table: "PostFigures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostFigures",
                table: "PostFigures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostEventTypes",
                table: "PostEventTypes");

            migrationBuilder.RenameTable(
                name: "PostFigures",
                newName: "PostFigure");

            migrationBuilder.RenameTable(
                name: "PostEventTypes",
                newName: "PostEventType");

            migrationBuilder.RenameIndex(
                name: "IX_PostFigures_FigureId",
                table: "PostFigure",
                newName: "IX_PostFigure_FigureId");

            migrationBuilder.RenameIndex(
                name: "IX_PostEventTypes_EventTypeId",
                table: "PostEventType",
                newName: "IX_PostEventType_EventTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostFigure",
                table: "PostFigure",
                columns: new[] { "PostId", "FigureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostEventType",
                table: "PostEventType",
                columns: new[] { "PostId", "EventTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostEventType_EventTypes_EventTypeId",
                table: "PostEventType",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "EventTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEventType_Posts_PostId",
                table: "PostEventType",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostFigure_Figures_FigureId",
                table: "PostFigure",
                column: "FigureId",
                principalTable: "Figures",
                principalColumn: "FigureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostFigure_Posts_PostId",
                table: "PostFigure",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
