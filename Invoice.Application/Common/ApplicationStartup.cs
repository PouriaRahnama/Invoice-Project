namespace DoctorAppointment.Application.Common
{
    public static class ApplicationStartup
    {
        public static void ApplicationConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region serilog
            #endregion

            #region DI ( Registeration Services )

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Idp Registration

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var jwtSettings = configuration.GetSection("JwtSettings");

            // Add JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings["Issuer"],
                     ValidAudience = jwtSettings["Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnChallenge = async context =>
                     {
                         context.HandleResponse();

                         context.Response.StatusCode = StatusCodes.Status403Forbidden;
                         context.Response.ContentType = "application/json";
                         var result = OkApiResult<string>.Fail("the Token is not valid.", 403);
                         await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
                     },
                     OnForbidden = async context =>
                     {
                         context.Response.StatusCode = StatusCodes.Status403Forbidden;
                         context.Response.ContentType = "application/json";

                         var result = OkApiResult<string>.Fail("forbidden", 403);
                         await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
                     }
                 };
             });



            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });

            #endregion


        }
    }
}