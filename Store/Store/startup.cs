namespace Store_ApplicationLayer
{
    public class startup
    {
        public static void Main()
        {
            var builder = WebApplication.CreateBuilder();
            builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("127.0.0.1"), 7136));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options => options.AddPolicy(name: "E-commerce",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }
            ));
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("E-commerce");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
