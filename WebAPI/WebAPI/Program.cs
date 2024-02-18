using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Domain;
using WebAPI.Middleware;
using WebAPI;
using WebAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Don't have naming policy, return raw Property
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new LocalTimeZoneConverter());
    }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Khai báo các Dependency Injection
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeValidate, EmployeeValidate>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentValidate, DepartmentValidate>();

builder.Services.AddScoped<IConfigRepository, ConfigRepository>();
builder.Services.AddScoped<IConfigService, ConfigService>();

builder.Services.AddScoped<IDateConfigurationService, DateConfigurationService>();
builder.Services.AddScoped<IDateConfigurationRepository, DateConfigurationRepository>();

builder.Services.AddScoped<ILeaveDaysRequestRepository, LeaveDaysRequestRepository>();
builder.Services.AddScoped<ILeaveDaysRequestService, LeaveDaysRequestService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();  
builder.Services.AddScoped<IUserService, UserService>();    

builder.Services.AddScoped<IParkMemberService, ParkMemberService>();    
builder.Services.AddScoped<IParkMemberRepository, ParkMemberRepository>();  

builder.Services.AddScoped<IParkSlotService, ParkSlotService>();    
builder.Services.AddScoped<IParkSlotRepository, ParkSlotRepository>();  

builder.Services.AddScoped<IParkingHistoryRepository, ParkingHistoryRepository>();
builder.Services.AddScoped<IParkingHistoryService, ParkingHistoryService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();    


// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Gán connectionString
AccessDatabase.connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Grant permission to FE fetch API
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
})
    );



builder.Services.AddControllers().ConfigureApiBehaviorOptions(
    options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Where(pair => pair.Value.Errors != null && pair.Value.Errors.Any()).Select(pair => new
            {
                ErrorKey = pair.Key,
                ErrorMessage = string.Join(", ", pair.Value.Errors.Select(error => error.ErrorMessage))
            }).ToArray();
            Console.WriteLine(context.ModelState.Values);
            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                UserMessage = "Lỗi từ người dùng",
                DevMessage = "Lỗi dữ liệu đầu vào từ người dùng",
                TraceId = "",
                MoreInfo = "",
                Errors = errors,
            }.ToString() ?? "");
        };
    }
    );
// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {   
        ValidateIssuer = true,
        ValidateAudience = true,
        //ValidateLifetime = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});

// Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.UseCors("corsapp");
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
