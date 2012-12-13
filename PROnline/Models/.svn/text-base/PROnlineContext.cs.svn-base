using System.Data.Entity;
using PROnline.Models.Users;
using PROnline.Models.Notices;
using PROnline.Models.Assessments;
using PROnline.Models.Teams;
using PROnline.Models.HeartWishes;
using System.Web.Mvc;
using PROnline.Models.Resources;
using System.Data.Entity.ModelConfiguration.Conventions;
using PROnline.Models.ShortMessages;
using PROnline.Models.Donation;
using PROnline.Models.Files;
using PROnline.Models.Service;
using PROnline.Models.Activities;
using PROnline.Models.Trainings;
using PROnline.Models.Home;
using PROnline.Models.Letters;
using PROnline.Models.Guides;
using PROnline.Models.WorkStations;

namespace PROnline.Models
{ 
    public class PROnlineContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<PROnline.Models.PROnlineContext>());
        public DbSet<User> Users { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<NoticeType> NoticeType { get; set; }

        public DbSet<Notice> Notice { get; set; }

        public DbSet<AssessmentQuestion> AssessmentQuestion { get; set; }

        public DbSet<StudyAssessment> StudyAssessment { get; set; }

        public DbSet<Administrator> Administrator { get; set; }

        public DbSet<School> School { get; set; }

        public DbSet<Student> Student { get; set; }

        public DbSet<Parent> Parent { get; set; }

        public DbSet<Volunteer> Volunteer { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<TeamMember> TeamMember { get; set; }

        public DbSet<HeartWishType> HeartWishType { get; set; }

        public DbSet<HeartWish> HeartWish { get; set; }

        public DbSet<HeartWishReply> HeartWishReply { get; set; }

        public DbSet<ResourceType> ResourceType { get; set; }

        public DbSet<AssessmentResultOption> AssessmentResultOption { get; set; }

        public DbSet<AssessmentOption> AssessmentOption { get; set; }

        public DbSet<AssessmentResult> AssessmentResult { get; set; }

        public DbSet<ShortMessage> ShortMessage { get; set; }

        public DbSet<Resource> Resource { get; set; }

        public DbSet<UploadFile> UploadFiles { get; set; }

        public DbSet<ResourceDiscussion> ResourceDiscussion { get; set; }

        public DbSet<RDReply> RDReply { get; set; }

        public DbSet<DonationType> DonationType { get; set; }

        public DbSet<Pairs> Pairs { get; set; }

        public DbSet<Activity> Activity { get; set; }

        public DbSet<Favorite> Favorite { get; set; }

        public DbSet<FavoriteResource> FavoriteResource { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<UserType> UserType { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }


        public DbSet<Answer> Answer { get; set; }

        public DbSet<VerifyState> VerifyState { get; set; }

        public DbSet<SliderModel> Slider { get; set; }

        public DbSet<WorkStation> WorkStation { get; set; }

        public DbSet<ResourceFile> ResourceFile { get; set; }

        public DbSet<AboutUs> AboutUs { get; set; }

        public DbSet<TrainingFeedback> TrainingFeedback { get; set; }

        public DbSet<ActivityContribute> ActivityContribute { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceType>().HasOptional(rt => rt.Parent)
                .WithMany(rt => rt.Children)
                .HasForeignKey(rt => rt.ParentId);



            modelBuilder.Entity<Volunteer>()
               .HasRequired(e => e.User)
               .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
              .HasRequired(e => e.User)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Donator>()
              .HasRequired(e => e.User)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrator>()
              .HasRequired(e => e.User)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Supervisor>()
               .HasRequired(e => e.User)
               .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Parent>()
               .HasRequired(e => e.User)
               .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TeamLeader>()
               .HasRequired(e => e.Volunteer)
               .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TeamMember>()
               .HasRequired(e => e.Volunteer)
               .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ShortMessage>()
              .HasRequired(e => e.FromUser)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ShortMessage>()
              .HasRequired(e => e.ToUser)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentFeedback>()
              .HasRequired(e => e.Student)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<VolunteerFeedback>()
             .HasRequired(e => e.volunteer)
             .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<AppointmentMember>()
              .HasRequired(e => e.User)
              .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Letter>()
                            .HasRequired(e => e.Sender)
                            .WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Letter>()
                                        .HasRequired(e => e.Receiver)
                                        .WithMany().WillCascadeOnDelete(false);

        }

        public DbSet<SchoolAdministrator> SchoolAdministrator { get; set; }

        public DbSet<UserPTest> UserPTest { get; set; }

        public DbSet<GoodAtField> GoodAtField { get; set; }

        public DbSet<Supervisor> Supervisor { get; set; }

        public DbSet<Training> Training { get; set; }

        public DbSet<TrainingVolunteer> TrainingVolunteer { get; set; }

        public DbSet<TrainingSupervisor> TrainingSupervisor { get; set; }

        public DbSet<PairAppointment> PairAppointment { get; set; }

        public DbSet<AppointmentMember> AppointmentMember { get; set; }

        public DbSet<TeamLeader> TeamLeader { get; set; }

        public DbSet<DonationItem> DonationItem { get; set; }

        public DbSet<Servers> Servers { get; set; }

        public DbSet<Letter> Letter { get; set; }

        public DbSet<GuideType> GuideType { get; set; }

        public DbSet<DonationVideo> DonationVideo { get; set; }

        public DbSet<Donator> Donator { get; set; }

        public DbSet<StudentFeedback> StudentFeedback { get; set; }

        public DbSet<VolunteerFeedback> VolunteerFeedback { get; set; }

        public DbSet<ShortMessageTemplate> ShortMessageTemplate { get; set; }

        public DbSet<DonationStudent> DonationStudent { get; set; }

        public DbSet<ActivityMember> ActivityMember { get; set; }
    }
}
