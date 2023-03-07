using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service;
using OrdersApiApp.Service.ClientService;
using OrdersApiApp.Service.OrderProductService;
using OrdersApiApp.Service.OrderService;
using OrdersApiApp.Service.ProductService;

var builder = WebApplication.CreateBuilder(args);
// добавление зависимостей
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IDaoTemplate<Client>, DbdDaoClient>();
builder.Services.AddTransient<IDaoTemplate<Order>, DbdDaoOrder>();
builder.Services.AddTransient<IDaoTemplate<OrderProduct>, DbdDaoOrderProduct>();
builder.Services.AddTransient<IDaoTemplate<Product>, DbdDaoProduct>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/client/all", async (HttpContext context, IDaoTemplate<Client> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/client/add", async (HttpContext context, IDaoTemplate<Client> dao, Client client) =>
{
    return await dao.Add(client);
});

app.MapGet("/client/delete", async (HttpContext context, IDaoTemplate<Client> dao, int id) =>
{
    return await dao.Delete(id);
});

app.MapGet("/client/get", async (HttpContext context, IDaoTemplate<Client> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/client/update", async (HttpContext context, IDaoTemplate<Client> dao, Client client) =>
{
    return await dao.Update(client);
});

// Order

app.MapGet("/order/all", async (HttpContext context, IDaoTemplate<Order> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/order/add", async (HttpContext context, IDaoTemplate<Order> dao, Order order) =>
{
    return await dao.Add(order);
});

app.MapGet("/order/get", async (HttpContext context, IDaoTemplate<Order> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/order/update", async (HttpContext context, IDaoTemplate<Order> dao, Order order) =>
{
    return await dao.Update(order);
});

app.MapGet("/order/delete", async (HttpContext context, IDaoTemplate<Order> dao, int id) =>
{
    return await dao.Delete(id);
});

// OrderProduct

app.MapGet("/orderProduct/all", async (HttpContext context, IDaoTemplate<OrderProduct> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/orderProduct/add", async (HttpContext context, IDaoTemplate<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.Add(orderProduct);
});

app.MapGet("/orderProduct/get", async (HttpContext context, IDaoTemplate<OrderProduct> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/orderProduct/update", async (HttpContext context, IDaoTemplate<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.Update(orderProduct);
});

app.MapGet("/orderProduct/delete", async (HttpContext context, IDaoTemplate<OrderProduct> dao, int id) =>
{
    return await dao.Delete(id);
});

// Product

app.MapGet("/product/all", async (HttpContext context, IDaoTemplate<OrderProduct> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/product/add", async (HttpContext context, IDaoTemplate<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.Add(orderProduct);
});

app.MapGet("/product/get", async (HttpContext context, IDaoTemplate<OrderProduct> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/product/update", async (HttpContext context, IDaoTemplate<OrderProduct> dao, OrderProduct orderProduct) =>
{
    return await dao.Update(orderProduct);
});

app.MapGet("/product/delete", async (HttpContext context, IDaoTemplate<OrderProduct> dao, int id) =>
{
    return await dao.Delete(id);
});


app.Run();
