using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Repositories;
using RepositoryLayer.UnitOfWork;
using ServiceLayer.Interfaces;
using ServiceLayer.Mapping;
using ServiceLayer.Services;
using Twilio.Clients;

namespace SWP391_PawFund.AppStarts
{
    public static class DependencyInjectionContainers
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true; ;
                options.LowercaseQueryStrings = true;
            });
            services.AddDbContext<PawFundContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 2)); // Adjust the version to match your MySQL version

                options.UseMySql(connectionString, serverVersion);
            });
            services.AddSingleton<ITwilioRestClient>(new TwilioRestClient("ACCOUNT_SID", "AUTH_TOKEN"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUsersService, UsersServices>();
            services.AddScoped<IDonateService, DonateService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IShelterService, ShelterService>();
            services.AddScoped<IStatusPetService, StatusPetService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdoptionRegistrationFormService, AdoptionRegistrationFormService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ICertificationService, CertificationService>();
			services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IFileUploadService, FileUploadService>();



        }
    }
}
