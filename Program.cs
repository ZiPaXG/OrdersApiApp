using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service.ClientService;
using OrdersApiApp.Service.OrderService;

var builder = WebApplication.CreateBuilder(args);
// добавление зависимостей
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoClient, DbdDaoClient>();
builder.Services.AddTransient<IDaoOrder, DbdDaoOrder>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/client/all", async (HttpContext context, IDaoClient dao) =>
{
    return await dao.GetAllClients();
});

app.MapPost("/client/add", async (HttpContext context, IDaoClient dao, Client client) =>
{
    return await dao.AddClient(client);
});

app.MapGet("/client/delete", async (HttpContext context, IDaoClient dao, int id) =>
{
    return await dao.DeleteClient(id);
});

app.MapGet("/client/get", async (HttpContext context, IDaoClient dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/client/update", async (HttpContext context, IDaoClient dao, Client client) =>
{
    return await dao.UpdateClient(client);
});

app.MapGet("/order/all", async (HttpContext context, IDaoOrder dao) =>
{
    return await dao.GetAllOrders();
});

app.MapPost("/order/add", async (HttpContext context, IDaoOrder dao, Order order) =>
{
    return await dao.AddOrder(order);
});

app.Run();
