using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Middleware;
using NTEcommerce.WebAPI.Model.Identity;
using NTEcommerce.WebAPI.Repository.Implementation;
using NTEcommerce.WebAPI.Repository.Interface;
using NTEcommerce.WebAPI.Services.Implement;
using NTEcommerce.WebAPI.Services.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add cors
builder.Services.AddCors(options => {
    options.AddPolicy("FreePolicy", 
    policy => {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("Pagination");
    });
});

//Add auto mapper
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Add dbContext
builder.Services.AddDbContext<EcommerceDbContext>(options => options
/*.UseLazyLoadingProxies()*/
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
                .AddEntityFrameworkStores<EcommerceDbContext>()
                .AddDefaultTokenProviders();
//Add services
builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<IProductServices, ProductServices>();
//Add unit of work
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//Add exception middleware
builder.Services.AddTransient<ExceptionHandleMiddleware>();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NTEcommerceAPI",
        Version = "v1",
        Description = "Authentication & Authorizstion in Asp .Net Core 6.0 with JWT & Swagger"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter `Bearer` [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\""

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }
                        ,new string[]{}
                    }
                });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine( app.Environment.ContentRootPath, "Resource", "ProductImages")),
    RequestPath = "/Product"
});

app.UseMiddleware<ExceptionHandleMiddleware>();

app.UseHttpsRedirection();

app.UseCors("FreePolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
