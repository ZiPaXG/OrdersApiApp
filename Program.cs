using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service.ClientService;
using OrdersApiApp.Service.OrderProductService;
using OrdersApiApp.Service.OrderService;

var builder = WebApplication.CreateBuilder(args);
// добавление зависимостей
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoClient, DbdDaoClient>();
builder.Services.AddTransient<IDaoOrder, DbdDaoOrder>();
builder.Services.AddTransient<IDaoOrderProduct, DbdDaoOrderProduct>();

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

app.MapGet("/order/get", async (HttpContext context, IDaoOrder dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/order/update", async (HttpContext context, IDaoOrder dao, Order order) =>
{
    return await dao.UpdateOrder(order);
});

app.MapGet("/order/delete", async (HttpContext context, IDaoOrder dao, int id) =>
{
    return await dao.DeleteOrder(id);
});

app.MapGet("/orderProduct/all", async (HttpContext context, IDaoOrderProduct dao) =>
{
    return await dao.GetAllOrderProduct();
});

app.MapPost("/orderProduct/add", async (HttpContext context, IDaoOrderProduct dao, OrderProduct orderProduct) =>
{
    return await dao.AddOrderProduct(orderProduct);
});

app.MapGet("/orderProduct/get", async (HttpContext context, IDaoOrderProduct dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/orderProduct/update", async (HttpContext context, IDaoOrderProduct dao, OrderProduct orderProduct) =>
{
    return await dao.UpdateOrderProduct(orderProduct);
});

app.MapGet("/orderProduct/delete", async (HttpContext context, IDaoOrderProduct dao, int id) =>
{
    return await dao.DeleteOrderProduct(id);
});

app.Run();
