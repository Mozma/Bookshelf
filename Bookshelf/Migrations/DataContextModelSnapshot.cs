﻿// <auto-generated />
using System;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookshelf.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0-rc.2.21480.5");

            modelBuilder.Entity("Bookshelf.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AuthorImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorImageId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Bookshelf.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ISBN")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PagesNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Subtitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("StatusId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Bookshelf.Models.BookBind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookBinds");
                });

            modelBuilder.Entity("Bookshelf.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Base64Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Bookshelf.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Bookshelf.Models.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("Bookshelf.Models.ShelfBind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShelfId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ShelfId");

                    b.ToTable("ShelfBinds");
                });

            modelBuilder.Entity("Bookshelf.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Bookshelf.Models.Author", b =>
                {
                    b.HasOne("Bookshelf.Models.Image", "AuthorImage")
                        .WithMany("Authors")
                        .HasForeignKey("AuthorImageId");

                    b.Navigation("AuthorImage");
                });

            modelBuilder.Entity("Bookshelf.Models.Book", b =>
                {
                    b.HasOne("Bookshelf.Models.Image", "Image")
                        .WithMany("Books")
                        .HasForeignKey("ImageId");

                    b.HasOne("Bookshelf.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId");

                    b.HasOne("Bookshelf.Models.Status", "Status")
                        .WithMany("Books")
                        .HasForeignKey("StatusId");

                    b.Navigation("Image");

                    b.Navigation("Publisher");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Bookshelf.Models.BookBind", b =>
                {
                    b.HasOne("Bookshelf.Models.Author", "Author")
                        .WithMany("BookBinds")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookshelf.Models.Book", "Book")
                        .WithMany("BookBinds")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Bookshelf.Models.ShelfBind", b =>
                {
                    b.HasOne("Bookshelf.Models.Book", "Book")
                        .WithMany("ShelfBinds")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bookshelf.Models.Shelf", "Shelf")
                        .WithMany("ShelfBinds")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("Bookshelf.Models.Author", b =>
                {
                    b.Navigation("BookBinds");
                });

            modelBuilder.Entity("Bookshelf.Models.Book", b =>
                {
                    b.Navigation("BookBinds");

                    b.Navigation("ShelfBinds");
                });

            modelBuilder.Entity("Bookshelf.Models.Image", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Books");
                });

            modelBuilder.Entity("Bookshelf.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Bookshelf.Models.Shelf", b =>
                {
                    b.Navigation("ShelfBinds");
                });

            modelBuilder.Entity("Bookshelf.Models.Status", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
