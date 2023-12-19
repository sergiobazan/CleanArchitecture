using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { } 
}