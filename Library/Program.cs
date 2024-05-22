using Serilog;
using Microsoft.EntityFrameworkCore;

namespace Library
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddSerilog(
				config =>
					config
						.MinimumLevel.Information()
						.WriteTo.Console()
						.WriteTo.File("log.txt"));

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<LibraryContext>(options =>
			{
				options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
				options.UseLazyLoadingProxies();
			}, ServiceLifetime.Singleton);

			builder.Services.AddSingleton<IBookService, BookService>();
			builder.Services.AddSingleton<IReaderService, ReaderService>();
			builder.Services.AddSingleton<IBorrowService, BorrowService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

			app.UseAuthorization();

			app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}
