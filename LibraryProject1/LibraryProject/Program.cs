using LibraryData.Models;
using LibraryData.Service.Entity;
using LibraryData.Service.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDataContext>
    (option => option.UseSqlServer
    (builder.Configuration.GetConnectionString("Connect")));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IMemberService, MemberService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
