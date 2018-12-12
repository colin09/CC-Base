﻿// <auto-generated />
using System;
using C.B.MySql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace C.B.Mysql.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20181212033317_initDb")]
    partial class initDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("C.B.MySql.Data.EventInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(32);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("EventId");

                    b.Property<int>("IsDeleted");

                    b.Property<int>("IsHot");

                    b.Property<int>("IsShow");

                    b.Property<int>("SortNo");

                    b.Property<string>("ThumbPath")
                        .HasMaxLength(64);

                    b.Property<int>("ThumbPicId");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("EventInfo");
                });

            modelBuilder.Entity("C.B.MySql.Data.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Icon")
                        .HasMaxLength(128);

                    b.Property<int>("IsDeleted");

                    b.Property<int>("IsShow");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.Property<int>("ParentId");

                    b.Property<int>("SortNo");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("EventType");
                });

            modelBuilder.Entity("C.B.MySql.Data.ExpertInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(32);

                    b.Property<string>("Content")
                        .HasMaxLength(512);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("IsDeleted");

                    b.Property<int>("IsShow");

                    b.Property<int>("PidFileId");

                    b.Property<int>("SortNo");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("ExpertInfo");
                });

            modelBuilder.Entity("C.B.MySql.Data.FileInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("IsDeleted");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("fileMd5")
                        .HasMaxLength(64);

                    b.Property<string>("fileName")
                        .HasMaxLength(64);

                    b.Property<string>("fileType")
                        .HasMaxLength(32);

                    b.Property<string>("filepath")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("FileInfo");
                });

            modelBuilder.Entity("C.B.MySql.Data.HisEventInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(2048);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("IsDeleted");

                    b.Property<int>("Link")
                        .HasMaxLength(128);

                    b.Property<int>("PicId");

                    b.Property<int>("SortNo");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateTime");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("HisEventInfo");
                });

            modelBuilder.Entity("C.B.MySql.Data.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(512);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("IsDeleted");

                    b.Property<int>("IsHot");

                    b.Property<int>("IsShow");

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.Property<string>("Region")
                        .HasMaxLength(64);

                    b.Property<int>("SortNo");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("replyContent")
                        .HasMaxLength(512);

                    b.Property<string>("replyName")
                        .HasMaxLength(64);

                    b.Property<DateTime>("replyTime");

                    b.HasKey("Id");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("C.B.MySql.Data.NewsInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(32);

                    b.Property<string>("Content")
                        .HasMaxLength(2048);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("FileId");

                    b.Property<int>("IsDeleted");

                    b.Property<int>("IsRoll");

                    b.Property<int>("IsShow");

                    b.Property<int>("IsTop");

                    b.Property<int>("NewsType");

                    b.Property<string>("PubOrg")
                        .HasMaxLength(64);

                    b.Property<DateTime>("PubTime");

                    b.Property<int>("SortNo");

                    b.Property<int>("ThumbId");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("NewsInfo");
                });

            modelBuilder.Entity("C.B.MySql.Data.Notice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(32);

                    b.Property<string>("Content")
                        .HasMaxLength(2048);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("IsDeleted");

                    b.Property<int>("IsRoll");

                    b.Property<int>("IsShow");

                    b.Property<int>("IsTop");

                    b.Property<string>("PubOrg")
                        .HasMaxLength(64);

                    b.Property<DateTime>("PubTime");

                    b.Property<int>("SortNo");

                    b.Property<string>("Title")
                        .HasMaxLength(64);

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Notice");
                });

            modelBuilder.Entity("C.B.MySql.Data.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthType");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Department")
                        .HasMaxLength(32);

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Password")
                        .HasMaxLength(32);

                    b.Property<int>("State");

                    b.Property<string>("TrueName")
                        .HasMaxLength(32);

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("UserName")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
