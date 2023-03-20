using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service;
using OrdersApiApp.Service.ClientService;
using OrdersApiApp.Service.OrderProductService;
using OrdersApiApp.Service.OrderService;
using OrdersApiApp.Service.ProductService;
using OrdersApiApp.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
// добавление зависимостей
builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoTemplate<Client>, DbDaoClient>();
builder.Services.AddTransient<IDaoTemplate<Order>, DbDaoOrder>();
builder.Services.AddTransient<IDaoTemplate<OrderProduct>, DbDaoOrderProduct>();
builder.Services.AddTransient<IDaoTemplate<Product>, DbDaoProduct>();
builder.Services.AddTransient<IDaoOrderProduct, DbDaoOrderProduct>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/{username}", async (HttpContext context, string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)), // время действия 10 минут
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var jwtRes = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = jwtRes,
        username = username
    };
    return response;
});

app.MapGet("/client/all", [Authorize] async (HttpContext context, IDaoTemplate<Client> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/client/add", [Authorize] async (HttpContext context, IDaoTemplate<Client> dao, Client client) =>
{
    return await dao.Add(client);
});

app.MapGet("/client/delete", [Authorize] async (HttpContext context, IDaoTemplate<Client> dao, int id) =>
{
    return await dao.Delete(id);
});

app.MapGet("/client/get", [Authorize] async (HttpContext context, IDaoTemplate<Client> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/client/update", [Authorize] async (HttpContext context, IDaoTemplate<Client> dao, Client client) =>
{
    return await dao.Update(client);
});

// Order

app.MapGet("/order/all", [Authorize] async (HttpContext context, IDaoTemplate<Order> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/order/add", [Authorize] async (HttpContext context, IDaoTemplate<Order> dao, Order order) =>
{
    return await dao.Add(order);
});

app.MapGet("/order/get", [Authorize] async (HttpContext context, IDaoTemplate<Order> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/order/update", [Authorize] async (HttpContext context, IDaoTemplate<Order> dao, Order order) =>
{
    return await dao.Update(order);
});

app.MapGet("/order/delete", [Authorize] async (HttpContext context, IDaoTemplate<Order> dao, int id) =>
{
    return await dao.Delete(id);
});

// OrderProduct

app.MapGet("/orderProduct/all", [Authorize] async (HttpContext context, IDaoTemplate<OrderProduct> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/orderProduct/add", [Authorize] async (HttpContext context, IDaoTemplate<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.Add(orderProduct);
});

app.MapGet("/orderProduct/get", [Authorize] async (HttpContext context, IDaoTemplate<OrderProduct> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/orderProduct/update", [Authorize] async (HttpContext context, IDaoTemplate<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.Update(orderProduct);
});

app.MapGet("/orderProduct/delete", [Authorize] async (HttpContext context, IDaoTemplate<OrderProduct> dao, int id) =>
{
    return await dao.Delete(id);
});

app.MapGet("/orderProduct/getProductsInOrder", [Authorize] async (HttpContext context, IDaoOrderProduct dao, int id) =>
{
    return await dao.GetProductsInOrderById(id);
});

// Чек

app.MapGet("/bill", [Authorize] async (HttpContext context, IDaoOrderProduct dao, int id) =>
{
    // указываем id заказа
    Bill bill = await dao.GetBill(id);
    return new {
        Products = bill.GetProducts(),
        TotalPrice = bill.GetTotalPrice()
    };
});

// Product

app.MapGet("/product/all", [Authorize] async (HttpContext context, IDaoTemplate<Product> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/product/add", [Authorize] async (HttpContext context, IDaoTemplate<Product> dao, Product product) =>
{
    return await dao.Add(product);
});

app.MapGet("/product/get", [Authorize] async (HttpContext context, IDaoTemplate<Product> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/product/update", [Authorize] async (HttpContext context, IDaoTemplate<Product> dao, Product product) =>
{
    return await dao.Update(product);
});

app.MapGet("/product/delete", [Authorize] async (HttpContext context, IDaoTemplate<Product> dao, int id) =>
{
    return await dao.Delete(id);
});


app.Run();
