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
builder.Services.AddTransient<IDaoTemplate<Client>, DbDaoClient>();
builder.Services.AddTransient<IDaoTemplate<Order>, DbDaoOrder>();
builder.Services.AddTransient<IDaoTemplate<OrderProduct>, DbDaoOrderProduct>();
builder.Services.AddTransient<IDaoTemplate<Product>, DbDaoProduct>();
builder.Services.AddTransient<IDaoOrderProduct, DbDaoOrderProduct>();

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

app.MapGet("/orderProduct/getProductsInOrder", async (HttpContext context, IDaoOrderProduct dao, int id) =>
{
    return await dao.GetProductsInOrderById(id);
});

// Чек

app.MapGet("/bill", async (HttpContext context, IDaoOrderProduct dao, int id) =>
{
    float totalPrice = 0;
    List<Product> products = new List<Product>();
    List<OrderProduct> orderProducts = await dao.GetByOrderId(id);
    // Собираем продукты заказа и суммируем цену товаров
    foreach(OrderProduct orderProduct in orderProducts)
    {
        products.Add(orderProduct.Product);
        totalPrice += orderProduct.Product.Price * orderProduct.CountProduct;
    }
    var bill = new { Products = products, TotalPrice = totalPrice };
    return bill;
});

// Product

app.MapGet("/product/all", async (HttpContext context, IDaoTemplate<Product> dao) =>
{
    return await dao.GetAll();
});

app.MapPost("/product/add", async (HttpContext context, IDaoTemplate<Product> dao, Product product) =>
{
    return await dao.Add(product);
});

app.MapGet("/product/get", async (HttpContext context, IDaoTemplate<Product> dao, int id) =>
{
    return await dao.GetById(id);
});

app.MapPost("/product/update", async (HttpContext context, IDaoTemplate<Product> dao, Product product) =>
{
    return await dao.Update(product);
});

app.MapGet("/product/delete", async (HttpContext context, IDaoTemplate<Product> dao, int id) =>
{
    return await dao.Delete(id);
});


app.Run();
