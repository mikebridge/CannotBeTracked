using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Demo;

namespace Demo.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    partial class DemoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Demo.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("DateCreated");

                    b.HasKey("Id");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Demo.MessageTag", b =>
                {
                    b.Property<Guid>("MessageId");

                    b.Property<string>("Tag");

                    b.Property<DateTime>("DateCreated");

                    b.HasKey("MessageId", "Tag");

                    b.ToTable("MessageTag");
                });

            modelBuilder.Entity("Demo.MessageTag", b =>
                {
                    b.HasOne("Demo.Message", "Message")
                        .WithMany("Tags")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
