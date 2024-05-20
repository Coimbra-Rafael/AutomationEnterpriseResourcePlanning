using AutomationEnterpriseResourcePlanning.Api.Data;
using AutomationEnterpriseResourcePlanning.Api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGet("/CustomersGetAll", async (ApplicationDbContext context) =>
{
    return await context.People.ToListAsync();
})
.WithOpenApi();

app.MapGet("/CustomerGetById/{id}", async (ApplicationDbContext context, Guid id) => 
{
    var customer = await context.People.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();

    if (customer == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(customer);
})
.WithOpenApi();

app.MapPost("/CustomerAdd", async (ApplicationDbContext context, Customers customer) =>
{
    if (customer.CpfCnpj.Length > 14)
        throw new Exception("Erro cnpj ou cpf invalido");
    await context.People.AddAsync(customer);
    await context.SaveChangesAsync();
    return Results.Created("GetCustomer", customer.Id);
})
.WithOpenApi()
.WithName("Cadastro de Clientes");

app.MapPut("/CustomerPut/{id}", async (ApplicationDbContext context, Guid id, Customers customer) => 
{
    if (id != customer.Id)
    {
        return Results.BadRequest();
    }

    context.Entry(customer).State = EntityState.Modified;

    try
    {
        await context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!CustomerExists(context, id))
        {
            return Results.NotFound();
        }
        else
        {
            throw;
        }
    }

    return Results.NoContent();
})
.WithOpenApi();

app.MapDelete("/CustomerDelete/{id}", async (ApplicationDbContext context, Guid id) => 
{
    var customer = await context.People.FindAsync(id);
    if (customer == null)
    {
        return Results.NotFound();
    }

    context.People.Remove(customer);
    await context.SaveChangesAsync();

    return Results.NoContent();
})
.WithOpenApi();

bool CustomerExists(ApplicationDbContext context ,Guid id)
{
    return context.People.Any(e => e.Id == id);
}

app.Run();