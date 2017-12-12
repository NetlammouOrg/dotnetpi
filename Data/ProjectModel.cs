namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.Entity;

    public partial class ProjectModel : DbContext
    {
        public ProjectModel()
            : base("name=NetlammouConnection")
        {
        }

        public virtual DbSet<action> action { get; set; }
        public virtual DbSet<article> article { get; set; }
        public virtual DbSet<basket> basket { get; set; }
        public virtual DbSet<comment> comment { get; set; }
        public virtual DbSet<feedback> feedback { get; set; }
        public virtual DbSet<localisation> localisation { get; set; }
        public virtual DbSet<message> message { get; set; }
        public virtual DbSet<ngos> ngos { get; set; }
        public virtual DbSet<notification> notification { get; set; }
        public virtual DbSet<participate> participate { get; set; }
        public virtual DbSet<preference> preference { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<rate> rate { get; set; }
        public virtual DbSet<t_todo> t_todo { get; set; }
        public virtual DbSet<topic> topic { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<volenteer> volenteer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<action>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<action>()
                .HasMany(e => e.localisation)
                .WithOptional(e => e.action)
                .HasForeignKey(e => e.action_id);

            modelBuilder.Entity<action>()
                .HasMany(e => e.rate)
                .WithRequired(e => e.action)
                .HasForeignKey(e => e.idAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<action>()
                .HasMany(e => e.participate)
                .WithRequired(e => e.action)
                .HasForeignKey(e => e.idAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<action>()
                .HasMany(e => e.feedback)
                .WithMany(e => e.action)
                .Map(m => m.ToTable("action_feedback").MapLeftKey("actions_id").MapRightKey("feedbacks_id"));

            modelBuilder.Entity<article>()
                .Property(e => e.body)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.other)
                .IsUnicode(false);

            modelBuilder.Entity<comment>()
                .Property(e => e._object)
                .IsUnicode(false);

            modelBuilder.Entity<feedback>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<feedback>()
                .HasMany(e => e.volenteer)
                .WithMany(e => e.feedback)
                .Map(m => m.ToTable("volenteer_feedback").MapLeftKey("feedbacks_id").MapRightKey("volenteers_id"));

            modelBuilder.Entity<localisation>()
                .Property(e => e.altitude)
                .IsUnicode(false);

            modelBuilder.Entity<localisation>()
                .Property(e => e.longitude)
                .IsUnicode(false);

            modelBuilder.Entity<localisation>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<localisation>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<localisation>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<message>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<message>()
                .Property(e => e._object)
                .IsUnicode(false);

            modelBuilder.Entity<ngos>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ngos>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<preference>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.picture)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.Categorie)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.basket)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.idProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_todo>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<topic>()
                .Property(e => e._object)
                .IsUnicode(false);

            modelBuilder.Entity<topic>()
                .HasMany(e => e.comment)
                .WithOptional(e => e.topic)
                .HasForeignKey(e => e.topic_id);

            modelBuilder.Entity<user>()
                .Property(e => e.DTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.adress)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.mailAdress)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.passWodrd)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.action)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.ngos_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.article)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.basket)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idVolenteer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.localisation)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.message)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasOptional(e => e.ngos)
                .WithRequired(e => e.user);

            modelBuilder.Entity<user>()
                .HasMany(e => e.participate)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idVolenteer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.product)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.ngo_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.rate)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idVolenteer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.topic)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasOptional(e => e.volenteer)
                .WithRequired(e => e.user);

            modelBuilder.Entity<user>()
                .HasMany(e => e.feedback)
                .WithMany(e => e.user)
                .Map(m => m.ToTable("user_feedback").MapLeftKey("volenteers_id").MapRightKey("feedbacks_id"));

            modelBuilder.Entity<user>()
                .HasMany(e => e.notification)
                .WithMany(e => e.user)
                .Map(m => m.ToTable("user_notification").MapLeftKey("volenteers_id").MapRightKey("notifications_id"));

            modelBuilder.Entity<user>()
                .HasMany(e => e.preference)
                .WithMany(e => e.user)
                .Map(m => m.ToTable("user_preference").MapLeftKey("volenteers_id").MapRightKey("preferences_id"));

            modelBuilder.Entity<user>()
                .HasMany(e => e.user1)
                .WithMany(e => e.user2)
                .Map(m => m.ToTable("user_user").MapLeftKey("volenteers_id").MapRightKey("ngos_id"));

            modelBuilder.Entity<volenteer>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<volenteer>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<volenteer>()
                .HasMany(e => e.ngos)
                .WithMany(e => e.volenteer)
                .Map(m => m.ToTable("volenteer_ngos").MapLeftKey("volenteers_id"));

            modelBuilder.Entity<volenteer>()
                .HasMany(e => e.notification)
                .WithMany(e => e.volenteer)
                .Map(m => m.ToTable("volenteer_notification").MapLeftKey("volenteers_id").MapRightKey("notifications_id"));

            modelBuilder.Entity<volenteer>()
                .HasMany(e => e.preference)
                .WithMany(e => e.volenteer)
                .Map(m => m.ToTable("volenteer_preference").MapLeftKey("volenteers_id").MapRightKey("preferences_id"));
        }
    }
}
