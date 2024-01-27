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
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
