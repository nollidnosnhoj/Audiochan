﻿// <auto-generated />
using System;
using Audiochan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Audiochan.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AudioTag", b =>
                {
                    b.Property<Guid>("AudiosId")
                        .HasColumnType("uuid")
                        .HasColumnName("audios_id");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint")
                        .HasColumnName("tags_id");

                    b.HasKey("AudiosId", "TagsId")
                        .HasName("pk_audio_tags");

                    b.HasIndex("TagsId")
                        .HasDatabaseName("ix_audio_tags_tags_id");

                    b.ToTable("audio_tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("Duration")
                        .HasColumnType("numeric")
                        .HasColumnName("duration");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("Picture")
                        .HasColumnType("text")
                        .HasColumnName("picture");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer")
                        .HasColumnName("visibility");

                    b.HasKey("Id")
                        .HasName("pk_audios");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_audios_created");

                    b.HasIndex("Title")
                        .HasDatabaseName("ix_audios_title");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_audios_user_id");

                    b.ToTable("audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoriteAudio", b =>
                {
                    b.Property<Guid>("AudioId")
                        .HasColumnType("uuid")
                        .HasColumnName("audio_id");

                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("FavoriteDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("favorite_date");

                    b.Property<DateTime?>("UnfavoriteDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("unfavorite_date");

                    b.HasKey("AudioId", "UserId")
                        .HasName("pk_favorite_audios");

                    b.HasIndex("FavoriteDate")
                        .HasDatabaseName("ix_favorite_audios_favorite_date");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_favorite_audios_user_id");

                    b.ToTable("favorite_audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoritePlaylist", b =>
                {
                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uuid")
                        .HasColumnName("playlist_id");

                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("FavoriteDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("favorite_date");

                    b.Property<DateTime?>("UnfavoriteDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("unfavorite_date");

                    b.HasKey("PlaylistId", "UserId")
                        .HasName("pk_favorite_playlists");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_favorite_playlists_user_id");

                    b.ToTable("favorite_playlists");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FollowedUser", b =>
                {
                    b.Property<string>("ObserverId")
                        .HasColumnType("text")
                        .HasColumnName("observer_id");

                    b.Property<string>("TargetId")
                        .HasColumnType("text")
                        .HasColumnName("target_id");

                    b.Property<DateTime>("FollowedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("followed_date");

                    b.Property<DateTime?>("UnfollowedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("unfollowed_date");

                    b.HasKey("ObserverId", "TargetId")
                        .HasName("pk_followed_users");

                    b.HasIndex("FollowedDate")
                        .HasDatabaseName("ix_followed_users_followed_date");

                    b.HasIndex("TargetId")
                        .HasDatabaseName("ix_followed_users_target_id");

                    b.ToTable("followed_users");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("Picture")
                        .HasColumnType("text")
                        .HasColumnName("picture");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer")
                        .HasColumnName("visibility");

                    b.HasKey("Id")
                        .HasName("pk_playlists");

                    b.HasIndex("Created")
                        .HasDatabaseName("ix_playlists_created");

                    b.HasIndex("Title")
                        .HasDatabaseName("ix_playlists_title");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_playlists_user_id");

                    b.ToTable("playlists");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.PlaylistAudio", b =>
                {
                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uuid")
                        .HasColumnName("playlist_id");

                    b.Property<Guid>("AudioId")
                        .HasColumnType("uuid")
                        .HasColumnName("audio_id");

                    b.Property<DateTime>("Added")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("added");

                    b.HasKey("PlaylistId", "AudioId")
                        .HasName("pk_playlist_audios");

                    b.HasIndex("AudioId")
                        .HasDatabaseName("ix_playlist_audios_audio_id");

                    b.ToTable("playlist_audios");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("role_name_index");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tags_name");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("About")
                        .HasColumnType("text")
                        .HasColumnName("about");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("display_name");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<DateTime>("Joined")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("joined");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("PictureBlobName")
                        .HasColumnType("text")
                        .HasColumnName("picture_blob_name");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.Property<string>("Website")
                        .HasColumnType("text")
                        .HasColumnName("website");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("user_name_index");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_claims_role_id");

                    b.ToTable("role_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_claims_user_id");

                    b.ToTable("user_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_logins_user_id");

                    b.ToTable("user_logins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_roles_role_id");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_user_tokens");

                    b.ToTable("user_tokens");
                });

            modelBuilder.Entity("PlaylistTag", b =>
                {
                    b.Property<Guid>("PlaylistsId")
                        .HasColumnType("uuid")
                        .HasColumnName("playlists_id");

                    b.Property<long>("TagsId")
                        .HasColumnType("bigint")
                        .HasColumnName("tags_id");

                    b.HasKey("PlaylistsId", "TagsId")
                        .HasName("pk_playlist_tags");

                    b.HasIndex("TagsId")
                        .HasDatabaseName("ix_playlist_tags_tags_id");

                    b.ToTable("playlist_tags");
                });

            modelBuilder.Entity("AudioTag", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Audio", null)
                        .WithMany()
                        .HasForeignKey("AudiosId")
                        .HasConstraintName("fk_audio_tags_audios_audios_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .HasConstraintName("fk_audio_tags_tags_tags_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("Audios")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_audios_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoriteAudio", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Audio", "Audio")
                        .WithMany("Favorited")
                        .HasForeignKey("AudioId")
                        .HasConstraintName("fk_favorite_audios_audios_audio_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("FavoriteAudios")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_favorite_audios_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FavoritePlaylist", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Playlist", "Playlist")
                        .WithMany("Favorited")
                        .HasForeignKey("PlaylistId")
                        .HasConstraintName("fk_favorite_playlists_playlists_playlist_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("FavoritePlaylists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_favorite_playlists_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.FollowedUser", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", "Observer")
                        .WithMany("Followings")
                        .HasForeignKey("ObserverId")
                        .HasConstraintName("fk_followed_users_users_observer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", "Target")
                        .WithMany("Followers")
                        .HasForeignKey("TargetId")
                        .HasConstraintName("fk_followed_users_users_target_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Observer");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Playlist", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", "User")
                        .WithMany("Playlists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_playlists_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.PlaylistAudio", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Audio", "Audio")
                        .WithMany()
                        .HasForeignKey("AudioId")
                        .HasConstraintName("fk_playlist_audios_audios_audio_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.Playlist", "Playlist")
                        .WithMany("Audios")
                        .HasForeignKey("PlaylistId")
                        .HasConstraintName("fk_playlist_audios_playlists_playlist_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audio");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.OwnsMany("Audiochan.Core.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasColumnName("id")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("created");

                            b1.Property<DateTime>("Expiry")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("expiry");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("token");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("user_id");

                            b1.HasKey("Id")
                                .HasName("pk_refresh_tokens");

                            b1.HasIndex("UserId")
                                .HasDatabaseName("ix_refresh_tokens_user_id");

                            b1.ToTable("refresh_tokens");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_refresh_tokens_users_user_id");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_role_claims_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_claims_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_logins_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_user_roles_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_roles_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_tokens_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaylistTag", b =>
                {
                    b.HasOne("Audiochan.Core.Entities.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistsId")
                        .HasConstraintName("fk_playlist_tags_playlists_playlists_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Audiochan.Core.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .HasConstraintName("fk_playlist_tags_tags_tags_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Audio", b =>
                {
                    b.Navigation("Favorited");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.Playlist", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("Favorited");
                });

            modelBuilder.Entity("Audiochan.Core.Entities.User", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("FavoriteAudios");

                    b.Navigation("FavoritePlaylists");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
