using TodoApi.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Pomelo.EntityFrameworkCore.MySql.Internal;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PraktycodeContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection")
    ,
    new MySqlServerVersion(new Version(8, 0, 41)) ,MySqlOptions=>MySqlOptions.EnableRetryOnFailure()
));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "A simple ASP.NET Core Web API"
    });
});
//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app=builder.Build();



// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty; 
    });
// }




app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.MapGet("/", () =>"work").WithName("stamm");

//שליפה הכל 
app.MapGet("/items", async (PraktycodeContext c) =>
{
    var items = await c.Items.ToListAsync();
    return Results.Ok(items);
})
.WithName("GetAllItems");


// הוספת משימה
app.MapPost("/items",async ([FromBody] Item item,PraktycodeContext c)=>{
Item it = new Item
    {
        Name = item.Name,
        IsComplete = false
    };
    c.Items.Add(it);
    await c.SaveChangesAsync();
    return Results.Ok(it);
}).WithName("addTask");
//מחיקת משימה

app.MapDelete("/items/{id}",async(int Id,PraktycodeContext c)=>{
    Item del=await c.Items.FirstOrDefaultAsync(w=>w.Id==Id);
    if(del!=null)
    c.Items.Remove(del);
    return Results.Ok(c.SaveChanges());

}).WithName("deletetodo");
//עדכון משימה
app.MapPut("items/{id}",async(int id,[FromBody] Item item,PraktycodeContext c)=>{
    Item del=await c.Items.FirstOrDefaultAsync(w=>w.Id==id);
    if(del!=null){
        del.IsComplete=item.IsComplete;
    }
    await c.SaveChangesAsync();
    return  Results.Ok(item);
     
});
app.MapControllers();

app.Run();
